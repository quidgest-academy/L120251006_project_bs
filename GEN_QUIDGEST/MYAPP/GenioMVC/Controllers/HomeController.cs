using JsonPropertyName = System.Text.Json.Serialization.JsonPropertyNameAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text;
using System.Text.Unicode;

using CSGenio.business;
using CSGenio.framework;
using CSGenio.persistence;
using GenioMVC.Models;
using GenioMVC.Models.Navigation;
using GenioMVC.ViewModels;
using GenioServer.security;
using Quidgest.Persistence.GenericQuery;

namespace GenioMVC.Controllers
{
	[Authorize]
	public class HomeController(UserContextService userContext) : ControllerBase(userContext)
	{
		private static readonly NavigationLocation ACTION_LSTUSR_EDIT = new("LISTA_DE_UTILIZADORE37232", "ChangeListProperties", "Home");

		public readonly string EPH_Action_Available_Key = "EPH_Action_Available";
		public readonly string EPH_Action_Form_Key = "EPH_Action_Form";

// USE /[MANUAL MOV HOME_CONTROLLER_INDEX]/
		[HttpGet]
		[AllowAnonymous]
		public ActionResult IndexAuthenticated()
		{
			var model = new Home_ViewModel(UserContext.Current);
			var isGuestUser = UserContext.Current.User.IsGuest();

			// Load the ViewModel of the Home Page
			model.HomePage_model = new ViewModels.Home.HomePage_ViewModel(UserContext.Current, isGuestUser);
			model.HomePage_model.Load();

			if (!isGuestUser)
			{
				if (UserContext.Current.User.NeedsToChangePassword())
					return RedirectToAction("Profile", "Home");
				if (UserContext.Current.User.NeedsToSetup2FA())
					return RedirectToAction("Change2FA", "Home");
			}

			return JsonOK(model);
		}

		[HttpGet]
		public JsonResult GetHomePages()
		{
			var isGuestUser = UserContext.Current.User.IsGuest();

			var model = new ViewModels.Home.HomePage_ViewModel(UserContext.Current, isGuestUser);
			model.Load();

			return JsonOK(model);
		}

		[HttpPost]
		public ActionResult Bookmarks()
		{
			if (UserContext.Current.User.IsGuest() || UserContext.Current.User.Public)
				return new EmptyResult();

			var cacheKey = string.Format("bookmarks.{0}.{1}", UserContext.Current.User.Name, UserContext.Current.User.Codpsw);
			var model = QCache.Instance.User.Get(cacheKey) as ViewModels.Bookmarks.Bookmarks_ViewModel;
			if (model == null)
			{
				model = new ViewModels.Bookmarks.Bookmarks_ViewModel();
				model.LoadMenus(UserContext.Current);
				QCache.Instance.User.Put(cacheKey, model, TimeSpan.FromMinutes(15));
			}

			return JsonOK(model);
		}

		public class RequestAddBookmarkModel
		{
			public string Module { get; set; }
			public string MenuId { get; set; }
		}

		[HttpPost]
		public JsonResult AddBookmark([FromBody]RequestAddBookmarkModel requestModel)
		{
			string module = requestModel.Module;
			string menuId = requestModel.MenuId;

			var sp = UserContext.Current.PersistentSupport;
			var user = UserContext.Current.User;
			try
			{
				var sqlCheck = new SelectQuery() { noLock = true }
					.Select(SqlFunctions.Count("1"), "count")
					.From(CSGenio.business.Area.AreaUSRCFG)
					.Where(CriteriaSet.And()
						.Equal(CSGenioAusrcfg.FldTipo, "FV")
						.Equal(CSGenioAusrcfg.FldCodpsw, user.Codpsw)
						.Equal(CSGenioAusrcfg.FldModulo, module)
						.Equal(CSGenioAusrcfg.FldId, menuId));

				var values = sp.executeReaderOneColumn(sqlCheck);
				int count = (int)values[0];

				if (count != 0)
					return Json(new { Success = true });

				sp.openTransaction();

				var fav = new CSGenio.business.CSGenioAusrcfg(user, module)
				{
					ValTipo = "FV",
					ValModulo = module,
					ValId = menuId,
					ValCodpsw = user.Codpsw
				};

				fav.insert(sp);

				var cacheKey = string.Format("bookmarks.{0}.{1}", UserContext.Current.User.Name, UserContext.Current.User.Codpsw);
				QCache.Instance.User.Invalidate(cacheKey);

				sp.closeTransaction();
			}
			catch (Exception e)
			{
				sp.rollbackTransaction();
				sp.closeConnection();
				Log.Error("Error on AddBookmark. Message: " + e.Message ?? string.Empty);
				return Json(new { Success = false, Message = Resources.Resources.PEDIMOS_DESCULPA__OC63848 });
			}

			try
			{
				var model = new ViewModels.Bookmarks.Bookmarks_ViewModel();
				model.LoadMenus(UserContext.Current);

				var cacheKey = string.Format("bookmarks.{0}.{1}", UserContext.Current.User.Name, UserContext.Current.User.Codpsw);
				QCache.Instance.User.Put(cacheKey, model, TimeSpan.FromMinutes(15));

				return JsonOK(model);
			}
			catch
			{
				return Json(new { Success = false, Message = Resources.Resources.PEDIMOS_DESCULPA__OC63848 });
			}
		}

		public class RequestRemoveBookmarkModel
		{
			public string BookmarkId { get; set; }
		}

		[HttpPost]
		public JsonResult RemoveBookmark([FromBody]RequestRemoveBookmarkModel requestModel)
		{
			string bookmarkId = requestModel.BookmarkId;
			var sp = UserContext.Current.PersistentSupport;
			var user = UserContext.Current.User;

			try
			{
				sp.openTransaction();

				var fav = CSGenio.business.CSGenioAusrcfg.search(sp, bookmarkId, user);

				fav.delete(sp);

				var cacheKey = string.Format("bookmarks.{0}.{1}", UserContext.Current.User.Name, UserContext.Current.User.Codpsw);
				QCache.Instance.User.Invalidate(cacheKey);

				sp.closeTransaction();
				return Json(new { Success = true, fav_id = fav.ValCodusrcfg });
			}
			catch (Exception e)
			{
				sp.rollbackTransaction();
				sp.closeConnection();
				Log.Error("Error on RemoveBookmark. Message: " + e.Message ?? string.Empty);
				return Json(new { Success = false, Message = Resources.Resources.PEDIMOS_DESCULPA__OC63848 });
			}
		}

		public JsonResult GetAvailableMenus()
		{
			var model = new Menu_ViewModel(UserContext.Current);
			return JsonOK(model);
		}

		private void RecreateUser()
		{
			QCache.Instance.User.Invalidate("user." + UserContext.Current.User.Name);
			UserContext.Current.User = null;
		}

		// GET: /Home/ProfileRedirect for Vue
		public ActionResult ProfileRedirect()
		{
			return RedirectToVueRoute("profile");
		}

		// GET: /Home/HomeRedirect for Vue
		public ActionResult HomeRedirect()
		{
			return RedirectToVueRoute("home");
		}

		// GET: /Home/Change2FA for Vue
		public ActionResult Change2FARedirect()
		{
			return RedirectToVueRoute("change2fa");
		}

		// GET: /Home/Profile
		[HttpGet]
		public ActionResult Profile()
		{
			var sp = UserContext.Current.PersistentSupport;
			var user = UserContext.Current.User;

			var profile = new ProfileModel();
			profile.Enable2FAOptions = Configuration.Security.Activate2FA != Auth2FAModes.None;

			try
			{
				sp.openConnection();
				var userValues = CSGenioApsw.search(sp, user.Codpsw, user, new string[] { CSGenioApsw.FldCodpsw.Field, CSGenioApsw.FldNome.Field });

				profile.ValCodpsw = userValues.ValCodpsw;
				profile.ValNome = userValues.ValNome;
			}
			catch
			{
				ModelState.AddModelError("Erro", Resources.Resources.PEDIMOS_DESCULPA__OC63848);
			}
			finally
			{
				sp.closeConnection();
			}

			var status = user.Status;
			if (status == 1)
				ModelState.AddModelError(Resources.Resources.PALAVRA_CHAVE_EXPIRA05120, Resources.Resources.PALAVRA_CHAVE_EXPIRA05120);

			// Check if configuracoes.xml have External Auth Providers configured
			foreach (var ip in SecurityFactory.IdentityProviderList)
			{
				if (ip.HasRedirectLogin())
					profile.AuthRedirectMethods.Add(new()
					{
						Id = ip.Id,
						Description = ip.Description,
						Redirect = ip.GetRedirectLoginUrl(
							AuthRedirectMethodModel.MapRedirectEndpoint(ip, Url, Request, "Register")
							)
					});
			}

			return JsonOK(profile);
		}

		//
		// POST: /Home/Profile/5
		[HttpPost]
		public ActionResult Profile([FromBody]ProfileModel model)
		{
			var years = UserContext.Current.User.Years;
			var user = UserContext.Current.User;
			List<string> yearsFailed = new List<string>();

			try
			{
				var validationResult = model.Validate(UserContext.Current);
				if (!validationResult.IsValid)
				{
					foreach (var (field, errorMessages) in validationResult.ModelErrors)
						foreach (var errorMessage in errorMessages)
							ModelState.AddModelError(field, errorMessage);
				}

				if (user.Codpsw != model.ValCodpsw)
				{
					string errorMessage = Resources.Resources.NAO_PODE_ALTERAR_A_P42871;
					ModelState.AddModelError(errorMessage, errorMessage);
				}

				// Change the password in each database the user has access to
				foreach (var year in years)
				{
					var sp = PersistentSupport.getPersistentSupport(year);

					try
					{
						sp.openConnection();
						var uf = new UserFactory(sp, user);
						var psw = uf.GetUser(user.Name);
						uf.ChangePassword(psw, model.NewPassword, model.ConfirmPassword, model.OldPassword);
						psw.update(sp);
					}
					catch (Exception ex)
					{
						Log.Error($"Profile - error changing password in system {year}. Error: {ex.Message}");
						yearsFailed.Add(year);
					}
					finally
					{
						sp.closeConnection();
					}
				}

				// Otherwise, recreate logged user.
				if (UserContext.Current.User.Status == 1)
					RecreateUser();
			}
			catch (Exception e)
			{
				HandleException(e);
				return JsonERROR(null, model);
			}
			finally
			{
				model.OldPassword = "";
				model.NewPassword = "";
				model.ConfirmPassword = "";
			}

			// If update failed for any databases
			if (yearsFailed.Count > 0)
				return JsonERROR(Resources.Resources.ERRO_AO_ATUALIZAR_A_25317 + ": " + string.Join(", ", [..yearsFailed]), yearsFailed);

			return JsonOK(new { Message = Resources.Resources.A_SUA_PASSWORD_FOI_A50177 });
		}

		public ActionResult Change2FA()
		{
			var model = new TwoFAViewModel();

			var userPsw = Models.Psw.Find(UserContext.Current.User.Codpsw, UserContext.Current);
			model.HasTotp = userPsw.ValPsw2fatp == Auth2FAModes.TOTP.ToString() ? 1 : 0;
			model.HasWebAuthN = userPsw.ValPsw2fatp == Auth2FAModes.WebAuth.ToString() ? 1 : 0;
			model.ShowTotp = false;

			// Give to user a message if is mandatory to create 2FA
			if (Configuration.Security.Mandatory2FA && !UserContext.Current.User.Auth2FA)
				ModelState.AddModelError("Erro", Resources.Resources.A_2ND_AUTHENTICATION36972);

			return JsonOK(model);
		}

		[HttpPost]
		public ActionResult Change2FA([FromBody]TwoFAViewModel model)
		{
			if (model.HasTotp == 1)
			{
				var secret = model.TotpDisplayCode;
				//Only save if the user has correctly inserted the 6 code, otherwise they may be locked out of the system
				if (new TOTPIdentityProvider().IsOk(secret, model.Totp6Code))
				{
					var sp = UserContext.Current.PersistentSupport;
					try
					{
						sp.openConnection();
						var userPsw = Models.Psw.Find(UserContext.Current.User.Codpsw, UserContext.Current);
						userPsw.ValPsw2fatp = Auth2FAModes.TOTP.ToString();
						userPsw.ValPsw2favl = secret;
						userPsw.Save(sp);
						sp.closeConnection();
					}
					catch (Exception ex)
					{
						sp.closeConnection();
						Log.Error(ex.Message);

						ModelState.AddModelError("user", Resources.Resources.PEDIMOS_DESCULPA__OC63848);
						CreateTOTPModel(ref model, secret);
						return JsonERROR(Resources.Resources.PEDIMOS_DESCULPA__OC63848, model);
					}

					RecreateUser();
				}
				else
				{
					ModelState.AddModelError(Resources.Resources.THE_CODE_YOU_ENTERED21835, Resources.Resources.THE_CODE_YOU_ENTERED21835);
					CreateTOTPModel(ref model, secret);
					return JsonERROR(Resources.Resources.THE_CODE_YOU_ENTERED21835, model);
				}
			}

			return JsonOK();
		}

		private string getUrlQrCodeTOTP (string secret)
		{
			return TOTPIdentityProvider.GetUrlQrCode(UserContext.Current.User.Name, secret);
		}

		private void CreateTOTPModel(ref TwoFAViewModel model, string secret)
		{
			model.HasTotp = 1;
			model.HasWebAuthN = 0;
			model.ShowTotp = true;

			var qrUrl = getUrlQrCodeTOTP(secret);
			model.TotpUrl = qrUrl;
			model.TotpDisplayCode = secret;
		}

		[HttpGet]
		public ActionResult CreateTOTP()
		{
			var model = new TwoFAViewModel();

			// Creation 2FA based on TOTP
			string secret = PasswordFactory.StringRandom(20, true);

			// Save the 2FA secret
			UserContext.Current.User.Code = secret;

			CreateTOTPModel(ref model, secret);

			return JsonOK(model);
		}

		[HttpGet]
		public ActionResult CreateWebAuthN()
		{
			var model = new TwoFAViewModel();

			model.HasTotp = 0;
			model.HasWebAuthN = 1;
			model.ShowWebAuthN = true;

			return JsonOK(model);
		}

		public ActionResult WebAuthn2FAMakeCredentialOptions()
		{
			WebAuthIdentityProvider credWebAuth = new WebAuthIdentityProvider(new WebAuthValues()
			{
				MDSAccessKey = ModelState.GetValueOrDefault("fido2:MDSAccessKey")?.AttemptedValue,
				MDSCacheDirPath = ModelState.GetValueOrDefault("fido2:MDSCacheDirPath")?.AttemptedValue,
				TimestampDriftTolerance = ModelState.GetValueOrDefault("fido2:TimestampDriftTolerance")?.AttemptedValue,

				Fido2Options = new WebAuthFido2Options() { Origin = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" }
			});

			var returnWebAuth = credWebAuth.MakeCredentialOptions(UserContext.Current.User.Name);

			if (returnWebAuth.Success)
			{
				// Temporarily store options, session/in-memory cache/redis/db
				HttpContext.Session.SetString("fido2.attestationOptions", returnWebAuth.Options);
				return Json(new { Success = true, options = returnWebAuth.Options });
			}
			else
				return Json(new { Success = false, ErrorMessage = returnWebAuth.ErrorMessage });
		}

		public async Task<ActionResult> WebAuthn2FAMakeCredentialOptions2(string data)
		{
			WebAuthIdentityProvider credWebAuth = new WebAuthIdentityProvider(new WebAuthValues()
			{
				MDSAccessKey = ModelState.GetValueOrDefault("fido2:MDSAccessKey")?.AttemptedValue,
				MDSCacheDirPath = ModelState.GetValueOrDefault("fido2:MDSCacheDirPath")?.AttemptedValue,
				TimestampDriftTolerance = ModelState.GetValueOrDefault("fido2:TimestampDriftTolerance")?.AttemptedValue,

				Fido2Options = new WebAuthFido2Options() { Origin = $"{Request.Scheme}://{Request.Host}{Request.PathBase}" }
			});

			User u = UserContext.Current.User;
			PersistentSupport sp = PersistentSupport.getPersistentSupport(u.Year, u.Name);
			var returnWebAuth = await credWebAuth.MakeCredential(data, HttpContext.Session.GetString("fido2.attestationOptions"), UserContext.Current.User.Codpsw, sp);

			if (returnWebAuth.Success)
				return Json(new { Success = returnWebAuth.Success, options = returnWebAuth.Options });
			return Json(new { Success = returnWebAuth.Success, ErrorMessage = returnWebAuth.ErrorMessage });
		}


		[AllowAnonymous]
		public ActionResult About()
		{
			return JsonOK(/* TODO: data ?? */);
		}

		[AllowAnonymous]
		public ActionResult NavigationalBar()
		{
			var availableMenus = Helpers.Menus.Menus.GetModuleMenus(UserContext.Current, UserContext.Current.User.CurrentModule, true);
			return JsonOK(new
			{
				Module = UserContext.Current.User.CurrentModule,
				MenuList = availableMenus
			});
		}

		/*
		// NOTE: This code not yet used for client-side debugging.
		[AllowAnonymous]
		public ActionResult QDebug()
		{
			// We only allow code debugging when event tracing is active.
			if (!Configuration.EventTracking)
				return RedirectToVueRoute("main");
			QDebug_ViewModel model = new(UserContext.Current);
			return JsonOK(model);
		}
		*/

		public record QSignRequestModel(string Tag, string Motivo, string Data, string Cargo, string Codtabela, string Coddocums, string Versao, string NomeTabela, string Campo, string Idsession,
			string Codpsw, string Name, string Year, string Module, string Language, string Class, string Rec, string Nome,
			int AssinarBd, int Assinado, int RegistarCc, string Serial, string Issuer, string Coddeslo);

		public enum FileDocType
		{
			/// <summary>
			/// Documento que referencia os dados de comunicação
			/// </summary>
			Reference,
			/// <summary>
			/// Documento assinado
			/// </summary>
			Sign
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult QSignManagement([FromQuery]QSignRequestModel requestModel)
		{
			// obter o conteudo do recurso
			string name = requestModel.Name;
			string codpsw = requestModel.Codpsw;
			string idsession = requestModel.Idsession;
			string year = requestModel.Year;
			string module = requestModel.Module;
			string language = requestModel.Language;

			string versao = requestModel.Versao;
			string coddocums = requestModel.Coddocums;
			string codtabela = requestModel.Codtabela;
			string nomeTabela = requestModel.NomeTabela;
			string campo = requestModel.Campo;
			string recSer = requestModel.Rec;

			// obter o utilizador da sessão
			User user = new User(name, idsession, year);
			user.CurrentModule = module;
			user.Language = language;
			user.Codpsw = codpsw;
			user.AddModuleRole(module, CSGenio.framework.Role.ADMINISTRATION);

			if (user == null)
				throw new Exception("Invalid session values");

			IFormFileCollection uploadFiles = Request.Form.Files;
			if (uploadFiles.Count > 0)
			{
				if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "loading.txt")
				{
					try
					{
						if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/loading" + idsession + ".txt"))
							System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/temp/loading" + idsession + ".txt", "");
					}
					catch { }
				}
				else if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "logincert.txt")
				{
					try
					{
						if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/logincert" + idsession + ".txt"))
							System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/temp/logincert" + idsession + ".txt", "");
					}
					catch { }
				}
				else if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "login.txt")
				{
					if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/login" + idsession + ".txt"))
					{
						IFormFile postedFile = uploadFiles[0];
						string filePath = AppDomain.CurrentDomain.BaseDirectory + "/temp/login" + idsession + ".txt";
						using (Stream fileStream = new FileStream(filePath, FileMode.Create))
							postedFile.CopyTo(fileStream);
					}
				}
				else if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "loginCancel.txt")
				{
					if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/loginCancel" + idsession + ".txt"))
					{
						IFormFile postedFile = uploadFiles[0];
						if (postedFile.Length > 0)
						{
							string filePath = AppDomain.CurrentDomain.BaseDirectory + "/temp/loginCancel" + idsession + ".txt";
							using (Stream fileStream = new FileStream(filePath, FileMode.Create))
								postedFile.CopyTo(fileStream);
						}
					}
				}
				else if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "loadingCert.txt")
				{
					try
					{
						if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/loadingCert" + idsession + ".txt"))
							System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/temp/loadingCert" + idsession + ".txt", "");
					}
					catch { }
				}
				else if (uploadFiles.Count == 1 && uploadFiles[0].FileName == "Cert.txt")
				{
					if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/temp/Cert" + idsession + ".txt"))
					{
						IFormFile postedFile = uploadFiles[0];
						if (postedFile.Length > 0)
						{
							string filePath = AppDomain.CurrentDomain.BaseDirectory + "/temp/Cert" + idsession + ".txt";
							using (Stream fileStream = new FileStream(filePath, FileMode.Create))
								postedFile.CopyTo(fileStream);
						}
					}
				}
				else
				{
					string tempFolder = FilePathUtils.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp");
					foreach (IFormFile postedFile in Request.Form.Files)
					{
						Guid guidOutput;
						FileDocType tipo = Guid.TryParse(postedFile.FileName.Split('.')[0], out guidOutput) ? FileDocType.Reference : FileDocType.Sign;
						if (tipo == FileDocType.Sign)
						{
							//Gravar documento localmente na pasta temp
							string fileName = postedFile.FileName;
							string fullFilePath = FilePathUtils.GetSafeFilePath(tempFolder, fileName); // TODO: Handle exceptions

							try
							{
								using (FileStream fs = System.IO.File.Create(fullFilePath))
								{
									postedFile.CopyTo(fs);
								}
							}
							catch { }

							bool hasAuxiliarClass = requestModel.Class != null;
							if (hasAuxiliarClass)
							{
								try
								{
									// obter o ticket da classe auxiliar
									string classe = requestModel.Class;

									// decifra o ticket, devolvendo um array com os objectos instanciados
									object[] objs = QResourcesSign.DecryptTicketBase64(classe);
									// na primeira posição do array está o IP
									string username = (string)objs[0];
									string ip = (string)objs[1];

									// valida o IP e o username
									if (!username.Equals(user.Name) && !ip.Equals(Request.HttpContext.Connection.RemoteIpAddress))
										throw new Exception("Invalid ticket");

									// na segunda posição do array está o objecto do recurso
									ResourceSign rec = objs[2] as ResourceSign;

									// cria-se um suporte persistente e invoca-se a função que devolve o conteúdo do recurso
									PersistentSupport sp = PersistentSupport.getPersistentSupport(user.Year, user.Name);

									try
									{
										byte[] file = System.IO.File.ReadAllBytes(fullFilePath);
										sp.openTransaction();
										rec.Sign(sp, user, file);
										sp.closeTransaction();
									}
									catch
									{
										sp.rollbackTransaction();
									}
									finally
									{
										sp.closeConnection();
										FilePathUtils.DeleteFileIfExists(tempFolder, fileName);
									}
								}
								catch { }
							}
							else
							{
								try
								{
									//comportamento padrão

									// Validate the ticket
									GetResourceFileFromTicket(recSer);

									//cria - se um suporte persistente e invoca - se a função que devolve o conteúdo do recurso
									PersistentSupport sp = PersistentSupport.getPersistentSupport(user.Year, user.Name);

									string nomedoc = "", auxnome = "";
									try
									{
										//Alterar a tabela docums
										sp.openConnection();
										CSGenioAdocums docums = CSGenioAdocums.search(sp, coddocums, user);
										sp.openTransaction();
										docums.duplicate(sp, CriteriaSet.And().Equal(CSGenioAdocums.FldCoddocums, coddocums));
										docums.ValZzstate = 0;
										docums.ValDatacria = DateTime.Now;
										docums.updateDirect(sp);
										coddocums = docums.ValCoddocums;

										//Registar documento associada à tabela passada como documento
										DbArea baseklass = (DbArea)CSGenio.business.Area.createArea(nomeTabela.Substring(3).ToLower(), user, user.CurrentModule);
										RequestedField field = new(baseklass.Alias + "." + baseklass.PrimaryKeyName, baseklass.Alias)
										{
											Value = codtabela,
											FieldType = FieldType.KEY_VARCHAR
										};
										baseklass.Fields.Add(baseklass.Alias + "." + baseklass.PrimaryKeyName, field);
										nomedoc = postedFile.FileName.Replace("Sign", "");
										auxnome = nomedoc.Substring(0, (nomedoc.Length - nomedoc.Split('.').Last().Length - 1));
										auxnome = auxnome.Substring(0, auxnome.Length - 36);
										campo = campo.Substring(0, 3).ToLower() == "val" ? campo.Substring(3).ToLower() : campo.ToLower();
										byte[] file = System.IO.File.ReadAllBytes(fullFilePath);
										baseklass.submitDocum(sp, campo, file, auxnome + "." + nomedoc.Split('.').Last() + "_" + coddocums, "SUBM", (int.Parse(versao) + 1).ToString());
										baseklass.updateDirect(sp);
										sp.closeTransaction();
									}
									catch
									{
										sp.rollbackTransaction();
									}
									finally
									{
										sp.closeConnection();
									}

									try
									{
										FilePathUtils.DeleteFileIfExists(tempFolder, nomedoc);
										FilePathUtils.DeleteFileIfExists(tempFolder, fileName);

										string nomeaux = nomedoc.Replace(auxnome, "").Replace(".pdf", "");

										FilePathUtils.DeleteFileIfExists(tempFolder, "nome" + nomeaux + ".txt");
									}
									catch { }
								}
								catch { }
							}
						}
						else if (tipo == FileDocType.Reference)
						{
							//vamos apagar todos os ficheiros que o txt referenciar
							string postedFileContent = new StreamReader(postedFile.OpenReadStream()).ReadToEnd();
							var files = postedFileContent.Split(';');
							foreach (string file in files)
							{
								FilePathUtils.DeleteFileIfExists(tempFolder, Path.GetFileName(file));
							}
						}
					}
				}
			}

			return new EmptyResult();
		}

		[AllowAnonymous]
		public ActionResult GetExternalFile([FromQuery]RequestDocumGetModel requestModel)
		{
			try
			{
				ResourceFile rec = GetResourceFileFromTicket(requestModel.Ticket);

				if (rec is null || string.IsNullOrEmpty(rec?.Name))
					// Invalid user or null record
					return PermissionError(Resources.Resources.O_REGISTO_PEDIDO_NAO63869);

				PersistentSupport sp = PersistentSupport.getPersistentSupport(UserContext.Current.User.Year);

				byte[] content = rec.GetContent(sp);
				string fileName = "\"" + rec.Name + "\"";
				string contentType = "application/octet-stream";
				return File(content, contentType, fileName);
			}
			catch (Exception ex)
			{
				CSGenio.framework.Log.Error("GetExternalFile Error: " + ex.Message);
				return JsonERROR();
			}
		}

		/// <summary>
		/// Created by [SF] at [2017.03.23]
		/// Fazer refresh à pagina
		/// </summary>
		/// <returns></returns>
		public ActionResult RefreshDbPDF()
		{
			string sessionId = HttpContext.Session.Id;
			if (System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", "loading" + sessionId + ".txt")))
			{
				System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", "loading" + sessionId + ".txt"));
				return JsonOK(new { success = true, loading = true });
			}
			if (System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", sessionId + ".txt")))
			{
				System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "temp", sessionId + ".txt"));
				return JsonOK(new { success = true, loading = false });
			}

			return JsonERROR();
		}

		/// <summary>
		/// Created by [HTA] at [2019.10.01]
		/// Devolve um link para ser usado com a aplicação da consola do Office Add-In. Usa o stream do pedido (request)
		/// </summary>
		/// <returns>O redirecionamneto para o link a ser usado na aplicação ou para a página de origem em caso de erro</returns>
		public ActionResult PrepareFileLink()
		{
			PersistentSupport userSP = UserContext.Current.PersistentSupport;
			string url = ""; // TODO: Update to use the exclusive addin portal
			string area = Request.Query["area"].First().ToLower();
			string areaPrimarykey = Request.Query["areakey"].First();
			string userAgent = Request.Headers.UserAgent;
			bool isWindows = false;
			if (userAgent.Contains("Windows"))
				isWindows = true;

			try
			{
				CSGenio.business.Area info = CSGenio.business.Area.createArea(area, UserContext.Current.User, UserContext.Current.User.CurrentModule);
				string tablename = info.TableName;
				string field = "";

				foreach (KeyValuePair<string,Field> fields in info.DBFields)
				{
					if (fields.Key.EndsWith("fk"))
					{
						field = fields.Key;
						break;
					}
				}

				SelectQuery query = new SelectQuery()
					.Select("docums", "document")
					.Select("docums", "docpath")
					.Select("docums", "nome")
					.Select("docums", "coddocums")
					.Select("docums", "datamuda")
					.From(tablename).Join("docums", "docums", TableJoinType.Inner).On(CriteriaSet.And().Equal(tablename, field, "docums", "documid"))
					.Where(CriteriaSet.And()
						.Equal(tablename, info.PrimaryKeyName, areaPrimarykey)
						.Equal(tablename, "zzstate", 0)
						.NotEqual("docums", "versao", "CHECKOUT"))
					.OrderBy("docums", "datacria", SortOrder.Descending)
					.OrderBy("docums", "chave", SortOrder.Ascending).Page(1);
				DataMatrix values = userSP.Execute(query);

				if (values.NumRows > 0)
				{
					Byte[] bytes = new byte[0];
					if (Configuration.Files2Disk)
					{
						System.IO.FileInfo fileinfo = new System.IO.FileInfo(Path.Combine(Configuration.PathDocuments, values.GetString(0, 1)));
						int size = (int)fileinfo.Length;
						bytes = new Byte[size];
						System.IO.FileStream fs = new System.IO.FileStream(Path.Combine(Configuration.PathDocuments, values.GetString(0, 1)), System.IO.FileMode.Open);
						fs.Read(bytes, 0, size);
						fs.Flush();
						fs.Close();
					}
					else
						bytes = values.GetBinary(0, 0);

					string fileName = values.GetString(0, 2);
					string documsPrimaryKey = values.GetString(0, 3);
					string timestamp = values.GetDate(0, 4).ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);

					string tempFile = Path.Combine(".", "temp", documsPrimaryKey + "-" + fileName);
					using (System.IO.FileStream tempFileStream = System.IO.File.OpenWrite(tempFile))
					{
						tempFileStream.Write(bytes, 0, bytes.Length);
					}
					string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
					tempFile = Path.Combine(baseUrl, "temp", documsPrimaryKey + "-" + fileName);

					ResourceFile resource = new(documsPrimaryKey + "-" + fileName, "");
					string ticket = QResources.CreateTicketEncryptedBase64(m_userContext.User.Name, m_userContext.User.Location, resource);
					Navigation.SetValue("filename", ticket);

					string protocol = "addin:";
					bool openTaskPane = false;
					if (!string.IsNullOrEmpty(Request.Query["openPane"].First()))
						openTaskPane = bool.Parse(Request.Query["openPane"].First());

					// Information format: url | File download url | File name | area name | area primary key | docums primary key | timestamp | open task pane (bool) | platform is Windows (bool)
					string link = protocol + url + "?linkfile=" + tempFile + "&filename=" + fileName + "&area=" + area + "&areakey=" + areaPrimarykey + "&documskey=" + documsPrimaryKey + "&timestamp=" + timestamp + "&taskpane=" + openTaskPane + "&win=" + isWindows;

					return Redirect(link);
				}

				return Redirect(Request.GetDisplayUrl());
			}
			catch (Exception)
			{
				if (userSP != null)
					userSP.closeConnection();
				return Redirect(Request.GetDisplayUrl());
			}
		}

		/// <summary>
		/// Redirects to the Permanent History Entry (PHE) menu.
		/// Create by [TMV] (2020.09.23)
		/// </summary>
		/// <returns></returns>
		public ActionResult GetEphFormAction([FromBody] RequestInitialEPHModule InitialEphModule)
		{
			User user = UserContext.Current.User;
			string routeName = string.Empty;
			// List of routes that are allowed as 'child' menus
			var allowedRoutes = new HashSet<string>();

			if (user.EphTofill != null)
			{
				var allowedActions = Helpers.Menus.Menus.GetAllowedRoutes(m_userContext.User, false);

				if (allowedActions.Count > 0)
				{
					routeName = allowedActions.LastOrDefault().action;

					foreach (var action in allowedActions)
						allowedRoutes.Add(action.action);
				}
			}

			return JsonOK(new { routeName, allowedRoutes });
		}

		/// <summary>
		/// Redirects to the Permanent History Entry (PHE) when don't pass in action filter.
		/// Create by FFS (2025.01.03)
		/// </summary>
		/// <returns></returns>
		public ActionResult GetEphFormActionByModule(string EphModule)
		{
			User user = UserContext.Current.User;
			string routeName = string.Empty;
			bool initialPHEEmpty = false;
			// List of routes that are allowed as 'child' menus
			var allowedRoutes = new HashSet<string>();

			if (user.EphTofill != null)
			{
				var allowedActions = Helpers.Menus.Menus.GetAllowedRoutes(m_userContext.User, false);
				routeName = allowedActions.LastOrDefault().action;

				foreach (var action in allowedActions)
				{
					allowedRoutes.Add(action.action);
					initialPHEEmpty = true;
				}
			}

			return JsonOK(new { routeName = routeName, allowedRoutes = allowedRoutes, InitialPHEEmpty = initialPHEEmpty, Module = EphModule });
		}

		#region Programmers area...




// USE /[MANUAL MOV HOME_CONTROLLER_MANUAL]/

		#endregion
	}
}
