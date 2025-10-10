using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Principal;

using CSGenio.business;
using CSGenio.core.di;
using CSGenio.core.persistence;
using CSGenio.framework;
using CSGenio.persistence;
using GenioMVC.Helpers;
using GenioMVC.Helpers.Menus;
using GenioMVC.Models;
using GenioMVC.Models.Exception;
using GenioMVC.Models.Navigation;
using GenioServer.security;

namespace GenioMVC.Controllers
{
	public class AccountController(IUserContextService userContextService) : ControllerBase(userContextService)
	{
		//
		// GET: /Account/LogOn
// USE /[MANUAL MOV CUSTOM_LOGON_GET]/
		[HttpGet]
		[AllowAnonymous]
		public ActionResult LogOn()
		{
			LogOnModel model = new();

			foreach(var ip in SecurityFactory.IdentityProviderList)
			{
				if (ip.HasRedirectLogin())
					model.AuthRedirectMethods.Add(new()
					{
						Id = ip.Id,
						Description = ip.Description,
						Redirect = ip.GetRedirectLoginUrl(
							AuthRedirectMethodModel.MapRedirectEndpoint(ip, Url, Request))
					});
			}

			return JsonOK(model);
		}

		/// <summary>
		/// Validates the provided model and adds any validation errors to the ModelState.
		/// </summary>
		/// <param name="model">The model to be validated.</param>
		/// <param name="userContext">The userContext.</param>
		public void ValidateModel(IValidatable model, UserContext userContext)
		{
			var validationResult = model.Validate(userContext);

			foreach (var (field, errorMessages) in validationResult.ModelErrors)
				foreach (var errorMessage in errorMessages)
					ModelState.AddModelError(field, errorMessage);
		}

		//
		// POST: /Account/LogOn
		[HttpPost]
		[AllowAnonymous]
// USE /[MANUAL MOV CUSTOM_LOGON_POST]/

		public ActionResult LogOn([FromBody]LogOnModel model, string returnUrl)
		{
			ValidateModel(model, UserContext.Current);
			if (!ModelState.IsValid)
				return JsonERROR();

			User? user = AuthenticateUser(model, Configuration.DefaultYear);
			if (user == null)
			{
				ModelState.AddModelError("Error", Resources.Resources.ENTRADA_INCORRETA__T45717);

				//Increment invalid login counter
				GenioDI.MetricsOtlp.IncrementCounter("login_counter", 1, new List<KeyValuePair<string, object>>()
				{
					new("User", model.UserName),
					new("Ip", HttpContext.GetIpAddress()),
					new("Failed", true)
				});

				return JsonERROR();
			}

			string loginError = ValidateLoginState(user);
			if (!string.IsNullOrEmpty(loginError))
			{
				ModelState.AddModelError("Error", loginError);

				// Increment invalid login counter
				GenioDI.MetricsOtlp.IncrementCounter("login_counter", 1, new List<KeyValuePair<string, object>>()
				{
					new("User", model.UserName),
					new("Ip", user.Location),
					new("Failed", true)
				});

				return JsonERROR();
			}

			//Increment success login counter
			GenioDI.MetricsOtlp.IncrementCounter("login_counter", 1, new List<KeyValuePair<string, object>>() {
				new("User", model.UserName),
				new("Ip", user.Location),
				new("Failed", false)
			});

			if (user.Auth2FA)
				return Json(new { Success = true, Auth2FA = true, User = user, Redirect = returnUrl });

			return finalizeAuthentication(user, returnUrl, false);
		}

		private string ValidateLoginState(User user)
		{
			if (user.Status == 2)
			{
				string errorMessage = Resources.Resources.ESTE_UTILIZADOR_ENCO01685;
				Log.Error($"{errorMessage}. User: {user.Name}");
				return errorMessage;
			}

			bool isConfigurationValid = DatabaseVersionReader.IsConfigurationUpToDate();
			if (!isConfigurationValid)
			{
				string errorMessage = Resources.Resources.E_NECESSARIO_PROCEDE36325;
				Log.Error($"{errorMessage}. Found: {Configuration.GetDbVersion(user.Year)}, Expected: {Configuration.VersionDbGen}");
				return errorMessage;
			}

			bool isValidDb = DatabaseVersionReader.IsDatabaseUpToDate(user);
			if (!isValidDb)
			{
				string errorMessage = Resources.Resources.E_NECESSARIO_ATUALIZ49371;
				Log.Error($"{errorMessage}. Found: {Configuration.GetDbVersion(user.Year)}, Expected: {Configuration.VersionDbGen}");
				return errorMessage;
			}

			return null;
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult Authentication2FA([FromBody] LogOnModel model, string returnUrl)
		{
			UserPassCredential cred = new()
			{
				Username = model.UserName,
				Password = model.Password
			};
			TOTPIdentityProvider totp = new();
			var ident = totp.Authenticate(cred);
			if (ident == null)
				return JsonERROR(Resources.Resources.DADOS_DE_LOGIN_INCOR44791);

			var principal = SecurityFactory.Authorize(ident);
			principal.SessionId = HttpContext.Session.Id;
			principal.Location = HttpContext.GetHostName();
			principal = UserFactory.ReadEphs(principal);
			return finalizeAuthentication(principal, returnUrl, true);
		}

		/// <summary>
		/// Sends the email for password recovery
		/// </summary>
		/// <remarks>TODO: Add Captcha to the RecoverPassword interface (Vue.js)</remarks>
		[HttpPost]
		[AllowAnonymous]
		public ActionResult RecoverPassword([FromBody]PasswordRecoverViewModel model)
		{
			try
			{
				ValidateModel(model, UserContext.Current);

				if (!ModelState.IsValid)
					return JsonERROR();

				var captchaData = model.CaptchaData;
				bool isValidCaptcha = QCaptcha.Validate(captchaData.UserEnteredCaptchaCode, captchaData.CaptchaId, HttpContext.Session);
				QCaptcha.SetCaptcha(captchaData.CaptchaId, null, HttpContext.Session);
				if (!isValidCaptcha)
				{
					ModelState.AddModelError("userEnteredCaptchaCode", Resources.Resources.INVALID_CAPTCHA29660);
					return JsonERROR(Resources.Resources.INVALID_CAPTCHA29660);
				}

				User user = SecurityFactory.Authorize(new()
				{
					Name = model.Email,
					AuthenticationType = "RecoverPassword",
					IsAuthenticated = false,
					IdProperty = GenioIdentityType.Email
				});
				user.Location = HttpContext.GetHostName();

				string emailBody = "";
				string appName = Configuration.Application.Name;
				// TODO: this should be obtained directly from user that already has its language filled by Usercontext
				string lang = RouteData.Values["culture"]?.ToString() ?? "";

				if (user != null)
				{
					ResourceUser rec = new(user.Name, user.Codpsw);
					var ticket = QResources.CreateTicketEncryptedBase64(user.Name, user.Location, rec);

					string userName = user.Name;
					string? urlToken = Url.Action("RecoverPasswordChange", "Account", new { ticket }, Request.Scheme);

					emailBody = UserRegistration.GetEmailForLanguage("PasswordChangeEmail", lang);
					emailBody = string.Format(emailBody, appName, userName, urlToken);
				}
				else
				{
					emailBody = UserRegistration.GetEmailForLanguage("InvalidEmailTemplate", lang);
					string? baseUrl = Url.Action("LogOn", "Account", null, Request.Scheme);
					emailBody = string.Format(emailBody, appName, baseUrl);
				}

				UserFactory userFactory = new(null, m_userContext.User);
				userFactory.SendPasswordRecoveryMail(model.Email, emailBody);
				model.IsEmailSent = true;
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				return JsonERROR(HandleException(e));
			}

			return JsonOK(model);
		}

		/// <summary>
		/// Receives a ticket, validates it and shows the view to change password
		/// </summary>
		[AllowAnonymous]
		public ActionResult RecoverPasswordChange(string ticket)
		{
			try
			{
				var ticketContent = QResources.DecryptTicketBase64(ticket);
				ResourceUser resource = ticketContent[2] as ResourceUser;

				//Check if ticket expired
				if (GenFunctions.DateDiffPart(resource.CreationDate, DateTime.UtcNow, "M") < 60)
				{
					//Store the id in session for later use
					HttpContext.Session.SetString("userId", resource.Name);
					return RedirectToVuePage("RecoverPasswordChange");
				}

				return RedirectToVuePage("ErrorTicketConfirm");
			}
			catch
			{
				return RedirectToVuePage("ErrorTicketConfirm");
			}
		}

		/// <summary>
		/// Persist the password change
		/// </summary>
		[HttpPost]
		[AllowAnonymous]
		public ActionResult RecoverPasswordChange([FromBody]PasswordRecoverChangeModel model)
		{
			ValidateModel(model, UserContext.Current);
			if (!ModelState.IsValid)
				return JsonERROR();

			try
			{
				User u = UserContext.Current.User;
				PersistentSupport sp = PersistentSupport.getPersistentSupport(u.Year, u.Name);
				var userFactory = new UserFactory(sp, u);
				//Get the user id from the session
				string userId = HttpContext.Session.GetString("userId");

				var user = userFactory.GetUser(userId);
				Password password = new(model.NewPassword, model.ConfirmPassword);

				string encOldPass = user.ValPassword;
				// checks if the new password is equal to the old one, if yes, return an error
				var isSamePassword = PasswordFactory.IsOK(password.New, encOldPass, user.ValSalt, user.ValPswtype);
				if (isSamePassword)
				{
					ModelState.AddModelError("error", Resources.Resources.A_NOVA_PALAVRA_PASSE58485);
					return JsonERROR();
				}

				//Change password
				userFactory.ChangePassword(user, model.NewPassword, model.ConfirmPassword);
				user.UserRecord = false;

				try
				{
					sp.openTransaction();

					user.User.Name = "PasswordRecovery";
					user.fillStampChange();
					user.update(sp);

					sp.closeTransaction();
				}
				catch
				{
					sp.rollbackTransaction();
					throw;
				}

				//Cleanup
				HttpContext.Session.Remove("userId");
				return JsonOK();
			}
			catch (InvalidPasswordException e)
			{
				Log.Error(e.Message);
				return JsonERROR(HandleException(e));
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				return JsonERROR(HandleException(e));
			}
		}

		[AllowAnonymous]
		public ActionResult WebAuthn2FAAssertionOptions()
		{
			WebAuthIdentityProvider credWebAuth = new WebAuthIdentityProvider(new WebAuthValues()
			{
				MDSAccessKey = ModelState.GetValueOrDefault("fido2:MDSAccessKey")?.AttemptedValue,
				MDSCacheDirPath = ModelState.GetValueOrDefault("fido2:MDSCacheDirPath")?.AttemptedValue,
				TimestampDriftTolerance = ModelState.GetValueOrDefault("fido2:TimestampDriftTolerance")?.AttemptedValue,
				Fido2Options = new WebAuthFido2Options() { Origin = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" }
			});

			User user = UserContext.Current.User;
			PersistentSupport sp = PersistentSupport.getPersistentSupport(user.Year, user.Name);
			var returnWebAuth = credWebAuth.AssertionOptionsPost(user.Codpsw, sp);

			if (returnWebAuth.Success)
			{
				//Temporarily store options, session/in-memory cache/redis/db
				HttpContext.Session.SetString("fido2.attestationOptions", returnWebAuth.Options);
				return Json(new { Success = true, options = returnWebAuth.Options });
			}

			return Json(new { Success = false, returnWebAuth.ErrorMessage });
		}

		public async Task<ActionResult> WebAuthn2FAMakeAssertion(string data, string returnUrl)
		{
			WebAuthIdentityProvider credWebAuth = new(new WebAuthValues()
			{
				MDSAccessKey = ModelState.GetValueOrDefault("fido2:MDSAccessKey")?.AttemptedValue,
				MDSCacheDirPath = ModelState.GetValueOrDefault("fido2:MDSCacheDirPath")?.AttemptedValue,
				TimestampDriftTolerance = ModelState.GetValueOrDefault("fido2:TimestampDriftTolerance")?.AttemptedValue,
				Fido2Options = new WebAuthFido2Options() { Origin = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" }
			});

			User user = UserContext.Current.User;

			PersistentSupport sp = PersistentSupport.getPersistentSupport(user.Year, user.Name);
			var returnWebAuth = await credWebAuth.MakeAssertion(data, HttpContext.Session.GetString("fido2.assertionOptions"), user.Codpsw, sp);

			if (returnWebAuth.Success)
				return finalizeAuthentication(user, returnUrl, true);
			return Json(new { returnWebAuth.Success, returnWebAuth.ErrorMessage });
		}

		private ActionResult IdentityProviderLoginGeneric(string providerId, Func<IIdentityProvider, Credential> createCredential)
		{
			try
			{
				var ip = SecurityFactory.IdentityProviderList.First(i => i.Id == providerId);
				var credential = createCredential(ip);
				User user = SecurityFactory.Authenticate(credential, providerId);
				user.SessionId = HttpContext.Session.Id;
				user.Location = HttpContext.GetHostName();
				user.Auth2FA = false;
				user.Status = 0;

				string loginError = ValidateLoginState(user);
				if (!string.IsNullOrEmpty(loginError))
					throw new BusinessException(loginError, "IdentityProviderLoginGeneric", loginError);

				finalizeAuthentication(user, "", false);
				return RedirectToVuePage("");
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}

			// TODO: When an authentication error occurs, return to Logon page and present the user with a perceptible error message.
			return RedirectToVuePage("Error", null, false);
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult OpenIdConnectLogin([FromRoute] string providerId, [FromForm] string code, [FromForm] string id_token)
		{
			return IdentityProviderLoginGeneric(providerId, (ip) => new TokenCredential()
			{
				Auth = code,
				Token = id_token,
				OriginUrl = AuthRedirectMethodModel.MapRedirectEndpoint(ip, Url, Request)
			});
		}

		private ActionResult IdentityProviderRegisterGeneric(string providerId, Func<IIdentityProvider, Credential> createCredential)
		{
			try
			{
				var ip = SecurityFactory.IdentityProviderList.First(i => i.Id == providerId);
				var credential = createCredential(ip);

				var identity = ip.Authenticate(credential);
				SecurityFactory.RegisterExternalId(UserContext.Current.User, identity);
				return RedirectToVuePage("Profile");
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
			}

			// TODO: When an authentication error occurs, return to Logon page and present the user with a perceptible error message.
			return RedirectToVuePage("");
		}

		[HttpGet]
		[ActionName("OpenIdConnectRegister")]
		public ActionResult OpenIdConnectRegister_Get([FromRoute] string providerId, [FromQuery] string code, [FromQuery] string id_token, [FromQuery] string state)
		{
			return IdentityProviderRegisterGeneric(providerId, (ip) => new TokenCredential()
			{
				Auth = code,
				Token = id_token,
				OriginUrl = AuthRedirectMethodModel.MapRedirectEndpoint(ip, Url, Request, "Register")
			});
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult OpenIdConnectRegister([FromRoute] string providerId, [FromForm] string code, [FromForm] string id_token, [FromForm] string state)
		{
			//reflects the request back to the server through a browser initiated Get
			//this is necessary because an external request will not bring the authentication cookies of the person registering because its considered cross-site
			//However if we do the registration part in a subsequent request then the cookies are sent and we can use them to get the user state.
			//This has the advantage of not needing any internal state being sent and maintained by the external provider.
			string endpoint = Url.Action("OpenIdConnectRegister", "Account", new { providerId, code, id_token, state });
			return ClientSideRedirect(endpoint);
		}

		/// <summary>
		/// After user have authenticated on Governement CMD identity provider will callback to our application to that funcion.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[AllowAnonymous]
		public ActionResult CMDLogin([FromRoute] string providerId)
		{
			//Government CMD sends the results in the Url hash (instead of the standard that sends them in the url query)
			//The server will not receive them in this method call, so we need to render a html page that captures them in javascript and sends them back as url query
			string endpoint = Url.Action("CMDLoginParams", "Account", new { providerId });
			return ClientSideRedirect(endpoint, captureHash: true);
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult CMDLoginParams([FromRoute] string providerId, string access_token, string token_type, string expires_in)
		{
			return IdentityProviderLoginGeneric(providerId, (ip) => new TokenCredential()
			{
				Token = access_token,
			});
		}

		[HttpGet]
		public ActionResult CMDRegisterParams([FromRoute] string providerId, string access_token, string token_type, string expires_in)
		{
			return IdentityProviderRegisterGeneric(providerId, (ip) => new TokenCredential()
			{
				Token = access_token
			});
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult CMDRegister([FromRoute] string providerId)
		{
			//reflects the request back to the server through a browser initiated Get
			//this is necessary because an external request will not bring the authentication cookies of the person registering because its considered cross-site
			//However if we do the registration part in a subsequent request then the cookies are sent and we can use them to get the user state.
			//This has the advantage of not needing any internal state being sent and maintained by the external provider.
			string endpoint = Url.Action("CMDRegisterParams", "Account", new { providerId });
			return ClientSideRedirect(endpoint, captureHash: true);
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult CASLogin([FromRoute] string providerId, string ticket)
		{
			return IdentityProviderLoginGeneric(providerId, (ip) => new TokenCredential()
			{
				Token = ticket,
				OriginUrl = AuthRedirectMethodModel.MapRedirectEndpoint(ip, Url, Request)
			});
		}

		private ActionResult finalizeAuthentication(User user, string returnUrl, bool Val2FA)
		{
			if (user != null)
			{
				user = UserFactory.ReadEphs(user);
				UserContext.Current.User = user;

				//UserContext.Current.SetFormsAuthenticationCookie();
				var claimsIdentity = new ClaimsIdentity(new List<System.Security.Claims.Claim>
				{
					new(ClaimTypes.Name, user.Name)
				}, LegacyFormsAuthenticationOptions.DefaultScheme);

				var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
				HttpContext.SignInAsync(claimsPrincipal).Wait();

				// log login (audit)
				CSGenio.framework.Audit.registLoginOut(user, Resources.Resources.ENTRADA31905, Resources.Resources.ENTRADA_ATRAVES_DA_P48446, HttpContext.GetHostName(), HttpContext.GetIpAddress());
				GlobalAppSessions.Instance.AddOrUpdate(HttpContext.Session.Id, user.Name, HttpContext.GetHostName());

				if (GenFunctions.emptyN(user.Status) == 0 && user.Status == 1 || (Configuration.Security.Mandatory2FA && !user.Auth2FA))
				{
					if (Val2FA)
						return Json(new { Success = true, Redirect = Url.Action("Profile", "Home"), Val2FA = true });
					return Json(new { Success = true, Redirect = Url.Action("Profile", "Home") });
				}
				else if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
					&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
				{
					if (Val2FA)
						return Json(new { Success = true, Redirect = returnUrl, Val2FA = true });
					return Json(new { Success = true, Redirect = returnUrl });
				}
				else
				{
					if (Val2FA)
						return Json(new { Success = true, Redirect = Url.Action("Index", "Home"), Val2FA = true });
					return Json(new { Success = true, Redirect = Url.Action("Index", "Home") });
				}
			}
			else
			{
				if (Val2FA)
					return Json(new { Success = false, Message = Resources.Resources.DADOS_DE_LOGIN_INCOR44791, Val2FA = false });
				return Json(new { Success = false, Message = Resources.Resources.DADOS_DE_LOGIN_INCOR44791 });
			}
		}

		//
		// GET: /Account/LogOff
		[HttpPost]
		public ActionResult LogOff()
		{
			DestroySession();
			return JsonOK();
		}

		[AllowAnonymous]
		public ActionResult RecoverPassword()
		{
			return JsonOK(new PasswordRecoverViewModel());
		}

		private User AuthenticateUser(BasicUserModel model, string year)
		{
			try
			{
				var principal = SecurityFactory.Authenticate(new UserPassCredential() { Username = model.UserName, Password = model.Password, Year = year });
				if (principal == null)
				{
					CSGenio.framework.Audit.registLoginOut(UserContext.Current.User,
						model.UserName,
						Resources.Resources.TENTATIVA38682,
						Resources.Resources.LOGIN_OU_PASSWORD_IN32183,
						HttpContext.GetHostName(),
						HttpContext.GetIpAddress());

					throw new BusinessException(Resources.Resources.LOGIN_OU_PASSWORD_IN32183, "InterfaceXml.pedidoEXW()", Resources.Resources.LOGIN_OU_PASSWORD_IN32183);
				}

				principal = UserFactory.ReadEphs(principal);
				principal.SessionId = HttpContext.Session.Id;
				principal.Location = HttpContext.GetHostName();
				return principal;
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				return null;
			}
		}

		[HttpGet]
		public ActionResult GetImage()
		{
			User usr = UserContext.Current.User;

			// html for user avatar image
			string dataImage = "";
			string ckey = "userInfo." + usr.Codpsw;

			UserInfo usrInfo = QCache.Instance.User.Get(ckey) as UserInfo;

			if (usrInfo == null)
			{
				usrInfo = UserProfileInfo.getUserImage(UserContext.Current.PersistentSupport, usr);

				if (!usrInfo.IsEmpty())
					QCache.Instance.User.Put(ckey, usrInfo, TimeSpan.FromMinutes(1));
			}

			if (usrInfo.Image != null && usrInfo.Image.Length > 0 )
			{
				dataImage = HtmlHelpers.ImageBase64(usrInfo.Image);
			}
			else
			{
				var avatar = System.IO.File.ReadAllBytes("./Content/img/user_avatar.png");
				dataImage = HtmlHelpers.ImageBase64(avatar);
			}

			return Json(new { Success = true, img = dataImage, fullname = usrInfo.Fullname, position = usrInfo.Position });
		}

		[HttpGet]
		public ActionResult UserAvatar()
		{
			User usr = UserContext.Current.User;

			// base64 image for user avatar image
			string dataImage = "";
			string ckey = "userInfo." + usr.Codpsw;

			UserInfo usrInfo = QCache.Instance.User.Get(ckey) as UserInfo;

			if (usrInfo == null)
			{
				usrInfo = UserProfileInfo.getUserImage(UserContext.Current.PersistentSupport, usr);

				if (!usrInfo.IsEmpty())
					QCache.Instance.User.Put(ckey, usrInfo, TimeSpan.FromMinutes(1));
			}

			if (usrInfo.Image != null && usrInfo.Image.Length > 0)
			{
				string img = HtmlHelpers.ImageBase64(usrInfo.Image);
				if (img != null)
					dataImage = img;
			}

			var usrAvatarMenu = UserAvatarMenu.GetMenus(UserContext.Current.PersistentSupport, UserContext.Current.User);
			var ePHUsrAvatarMenu = EPHUserAvatarMenu.GetMenus(UserContext.Current);
			var avatar = new { image = dataImage, fullname = usrInfo.Fullname, position = usrInfo.Position };

			var has2FAOptions = Configuration.Security.Activate2FA != Auth2FAModes.None;
			var hasOpenIdAuth = Configuration.Security.IdentityProviders.Exists(ip => ip.Type == "GenioServer.security.OpenIdConnectIdentityProvider");

			return Json(new { Success = true, Avatar = avatar, UserAvatarMenus = usrAvatarMenu, EPHUserAvatarMenus = ePHUsrAvatarMenu, Has2FAOptions = has2FAOptions, HasOpenIdAuth = hasOpenIdAuth });
		}

		// GET: /Account/GetIfUserLogged
		[AllowAnonymous]
		public ActionResult GetIfUserLogged()
		{
			var user = UserContext.Current.User;
			return Json(new { username = user.Name != "guest" ? user.Name : "" });
		}
	}
}
