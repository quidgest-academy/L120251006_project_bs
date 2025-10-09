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
using GenioMVC.ViewModels.Movie;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER MOVIE]/

namespace GenioMVC.Controllers
{
	public partial class MovieController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_MOVADM_CANCEL = new("MOVIES48538", "F_movadm_Cancel", "Movie") { vueRouteName = "form-F_MOVADM", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_MOVADM_SHOW = new("MOVIES48538", "F_movadm_Show", "Movie") { vueRouteName = "form-F_MOVADM", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_MOVADM_NEW = new("MOVIES48538", "F_movadm_New", "Movie") { vueRouteName = "form-F_MOVADM", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_MOVADM_EDIT = new("MOVIES48538", "F_movadm_Edit", "Movie") { vueRouteName = "form-F_MOVADM", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_MOVADM_DUPLICATE = new("MOVIES48538", "F_movadm_Duplicate", "Movie") { vueRouteName = "form-F_MOVADM", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_MOVADM_DELETE = new("MOVIES48538", "F_movadm_Delete", "Movie") { vueRouteName = "form-F_MOVADM", mode = "DELETE" };

		#endregion

		#region F_movadm private

		private void FormHistoryLimits_F_movadm()
		{

		}

		#endregion

		#region F_movadm_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_MOVADM]/

		[HttpPost]
		public ActionResult F_movadm_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_movadm_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Show_GET",
				AreaName = "movie",
				Location = ACTION_F_MOVADM_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_movadm();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_MOVADM]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_movadm_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_movadm_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_New_GET",
				AreaName = "movie",
				FormName = "F_MOVADM",
				Location = ACTION_F_MOVADM_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_movadm();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_MOVADM]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Movie/F_movadm_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_New([FromBody]F_movadm_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_New",
				ViewName = "F_movadm",
				AreaName = "movie",
				Location = ACTION_F_MOVADM_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_MOVADM]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_MOVADM]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_MOVADM]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_movadm_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_movadm_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Edit_GET",
				AreaName = "movie",
				FormName = "F_MOVADM",
				Location = ACTION_F_MOVADM_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_movadm();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_MOVADM]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Movie/F_movadm_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_Edit([FromBody]F_movadm_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Edit",
				ViewName = "F_movadm",
				AreaName = "movie",
				Location = ACTION_F_MOVADM_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_MOVADM]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_MOVADM]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_MOVADM]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_movadm_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_movadm_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Delete_GET",
				AreaName = "movie",
				FormName = "F_MOVADM",
				Location = ACTION_F_MOVADM_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_movadm();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_MOVADM]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Movie/F_movadm_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_movadm_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Delete",
				ViewName = "F_movadm",
				AreaName = "movie",
				Location = ACTION_F_MOVADM_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_MOVADM]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_movadm_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_MOVADM");
		}

		#endregion

		#region F_movadm_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_MOVADM]/

		[HttpPost]
		public ActionResult F_movadm_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_movadm_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Duplicate_GET",
				AreaName = "movie",
				FormName = "F_MOVADM",
				Location = ACTION_F_MOVADM_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_MOVADM]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Movie/F_movadm_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_MOVADM]/
		[HttpPost]
		public ActionResult F_movadm_Duplicate([FromBody]F_movadm_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_movadm_Duplicate",
				ViewName = "F_movadm",
				AreaName = "movie",
				Location = ACTION_F_MOVADM_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_MOVADM]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_MOVADM]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_MOVADM]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_movadm_Cancel

		//
		// GET: /Movie/F_movadm_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_MOVADM]/
		public ActionResult F_movadm_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Movie(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("movie");

// USE /[MANUAL MOV BEFORE_CANCEL F_MOVADM]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_MOVADM]/

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

				Navigation.SetValue("ForcePrimaryRead_movie", "true", true);
			}

			Navigation.ClearValue("movie");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		// POST: /Movie/F_movadm_SaveEdit
		[HttpPost]
		public ActionResult F_movadm_SaveEdit([FromBody] F_movadm_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_movadm_SaveEdit",
				ViewName = "F_movadm",
				AreaName = "movie",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_MOVADM]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_MOVADM]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_movadmDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_movadm_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_movadm([FromBody] F_movadmDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
