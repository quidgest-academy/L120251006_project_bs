using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Dynamic;

using CSGenio.business;
using CSGenio.core.persistence;
using CSGenio.framework;
using CSGenio.persistence;
using CSGenio.reporting;
using GenioMVC.Helpers;
using GenioMVC.Models;
using GenioMVC.Models.Exception;
using GenioMVC.Models.Navigation;
using GenioMVC.Resources;
using GenioMVC.ViewModels;
using GenioMVC.ViewModels.Userp;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER USERP]/

namespace GenioMVC.Controllers
{
	public partial class UserpController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_PROFIL_CANCEL = new("USER_PROFILE06358", "F_profil_Cancel", "Userp") { vueRouteName = "form-F_PROFIL", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_PROFIL_SHOW = new("USER_PROFILE06358", "F_profil_Show", "Userp") { vueRouteName = "form-F_PROFIL", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_PROFIL_NEW = new("USER_PROFILE06358", "F_profil_New", "Userp") { vueRouteName = "form-F_PROFIL", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_PROFIL_EDIT = new("USER_PROFILE06358", "F_profil_Edit", "Userp") { vueRouteName = "form-F_PROFIL", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_PROFIL_DUPLICATE = new("USER_PROFILE06358", "F_profil_Duplicate", "Userp") { vueRouteName = "form-F_PROFIL", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_PROFIL_DELETE = new("USER_PROFILE06358", "F_profil_Delete", "Userp") { vueRouteName = "form-F_PROFIL", mode = "DELETE" };

		#endregion

		#region F_profil private

		private void FormHistoryLimits_F_profil()
		{

		}

		#endregion

		#region F_profil_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_PROFIL]/

		[HttpPost]
		public ActionResult F_profil_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_profil_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Show_GET",
				AreaName = "userp",
				Location = ACTION_F_PROFIL_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_profil();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_PROFIL]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_profil_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_profil_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_New_GET",
				AreaName = "userp",
				FormName = "F_PROFIL",
				Location = ACTION_F_PROFIL_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_profil();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_PROFIL]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Userp/F_profil_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_New([FromBody]F_profil_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_New",
				ViewName = "F_profil",
				AreaName = "userp",
				Location = ACTION_F_PROFIL_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_PROFIL]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_PROFIL]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_PROFIL]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_profil_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_profil_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Edit_GET",
				AreaName = "userp",
				FormName = "F_PROFIL",
				Location = ACTION_F_PROFIL_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_profil();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_PROFIL]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Userp/F_profil_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_Edit([FromBody]F_profil_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Edit",
				ViewName = "F_profil",
				AreaName = "userp",
				Location = ACTION_F_PROFIL_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_PROFIL]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_PROFIL]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_PROFIL]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_profil_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_profil_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Delete_GET",
				AreaName = "userp",
				FormName = "F_PROFIL",
				Location = ACTION_F_PROFIL_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_profil();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_PROFIL]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Userp/F_profil_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_profil_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Delete",
				ViewName = "F_profil",
				AreaName = "userp",
				Location = ACTION_F_PROFIL_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_PROFIL]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_profil_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_PROFIL");
		}

		#endregion

		#region F_profil_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_PROFIL]/

		[HttpPost]
		public ActionResult F_profil_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_profil_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Duplicate_GET",
				AreaName = "userp",
				FormName = "F_PROFIL",
				Location = ACTION_F_PROFIL_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_PROFIL]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Userp/F_profil_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_PROFIL]/
		[HttpPost]
		public ActionResult F_profil_Duplicate([FromBody]F_profil_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_profil_Duplicate",
				ViewName = "F_profil",
				AreaName = "userp",
				Location = ACTION_F_PROFIL_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_PROFIL]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_PROFIL]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_PROFIL]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_profil_Cancel

		//
		// GET: /Userp/F_profil_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_PROFIL]/
		public ActionResult F_profil_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Userp(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("userp");

// USE /[MANUAL MOV BEFORE_CANCEL F_PROFIL]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_PROFIL]/

				}
				catch (Exception e)
				{
					sp.rollbackTransaction();
					sp.closeConnection();

					var exceptionUserMessage = Resources.Resources.PEDIMOS_DESCULPA__OC63848;
					if (e is GenioException && (e as GenioException).UserMessage != null)
						exceptionUserMessage = Translations.Get((e as GenioException).UserMessage, UserContext.Current.User.Language);
					return JsonERROR(exceptionUserMessage);
				}

				Navigation.SetValue("ForcePrimaryRead_userp", "true", true);
			}

			Navigation.ClearValue("userp");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		// POST: /Userp/F_profil_SaveEdit
		[HttpPost]
		public ActionResult F_profil_SaveEdit([FromBody] F_profil_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_profil_SaveEdit",
				ViewName = "F_profil",
				AreaName = "userp",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_PROFIL]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_PROFIL]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_profilDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_profil_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_profil([FromBody] F_profilDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
