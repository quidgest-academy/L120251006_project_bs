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

		private static readonly NavigationLocation ACTION_F_USER_CANCEL = new("USER_PROFILE06358", "F_user_Cancel", "Userp") { vueRouteName = "form-F_USER", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_USER_SHOW = new("USER_PROFILE06358", "F_user_Show", "Userp") { vueRouteName = "form-F_USER", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_USER_NEW = new("USER_PROFILE06358", "F_user_New", "Userp") { vueRouteName = "form-F_USER", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_USER_EDIT = new("USER_PROFILE06358", "F_user_Edit", "Userp") { vueRouteName = "form-F_USER", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_USER_DUPLICATE = new("USER_PROFILE06358", "F_user_Duplicate", "Userp") { vueRouteName = "form-F_USER", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_USER_DELETE = new("USER_PROFILE06358", "F_user_Delete", "Userp") { vueRouteName = "form-F_USER", mode = "DELETE" };

		#endregion

		#region F_user private

		private void FormHistoryLimits_F_user()
		{

		}

		#endregion

		#region F_user_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_USER]/

		[HttpPost]
		public ActionResult F_user_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_user_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Show_GET",
				AreaName = "userp",
				Location = ACTION_F_USER_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_user();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_USER]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_user_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_USER]/
		[HttpPost]
		public ActionResult F_user_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_user_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_user_New_GET",
				AreaName = "userp",
				FormName = "F_USER",
				Location = ACTION_F_USER_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_user();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_USER]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Userp/F_user_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_USER]/
		[HttpPost]
		public ActionResult F_user_New([FromBody]F_user_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_user_New",
				ViewName = "F_user",
				AreaName = "userp",
				Location = ACTION_F_USER_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_USER]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_USER]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_USER]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_user_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_USER]/
		[HttpPost]
		public ActionResult F_user_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_user_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Edit_GET",
				AreaName = "userp",
				FormName = "F_USER",
				Location = ACTION_F_USER_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_user();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_USER]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Userp/F_user_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_USER]/
		[HttpPost]
		public ActionResult F_user_Edit([FromBody]F_user_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Edit",
				ViewName = "F_user",
				AreaName = "userp",
				Location = ACTION_F_USER_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_USER]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_USER]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_USER]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_user_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_USER]/
		[HttpPost]
		public ActionResult F_user_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_user_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Delete_GET",
				AreaName = "userp",
				FormName = "F_USER",
				Location = ACTION_F_USER_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_user();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_USER]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Userp/F_user_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_USER]/
		[HttpPost]
		public ActionResult F_user_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_user_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_user_Delete",
				ViewName = "F_user",
				AreaName = "userp",
				Location = ACTION_F_USER_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_USER]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_user_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_USER");
		}

		#endregion

		#region F_user_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_USER]/

		[HttpPost]
		public ActionResult F_user_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_user_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Duplicate_GET",
				AreaName = "userp",
				FormName = "F_USER",
				Location = ACTION_F_USER_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_USER]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Userp/F_user_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_USER]/
		[HttpPost]
		public ActionResult F_user_Duplicate([FromBody]F_user_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_user_Duplicate",
				ViewName = "F_user",
				AreaName = "userp",
				Location = ACTION_F_USER_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_USER]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_USER]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_USER]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_user_Cancel

		//
		// GET: /Userp/F_user_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_USER]/
		public ActionResult F_user_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Userp(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("userp");

// USE /[MANUAL MOV BEFORE_CANCEL F_USER]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_USER]/

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


		public class F_user_PswValNomeModel : RequestLookupModel
		{
			public F_user_ViewModel Model { get; set; }
		}

		//
		// GET: /Userp/F_user_PswValNome
		// POST: /Userp/F_user_PswValNome
		[ActionName("F_user_PswValNome")]
		public ActionResult F_user_PswValNome([FromBody] F_user_PswValNomeModel requestModel)
		{
			var queryParams = requestModel.QueryParams;

			int perPage = CSGenio.framework.Configuration.NrRegDBedit;
			string rowsPerPageOptionsString = "";

			// If there was a recent operation on this table then force the primary persistence server to be called and ignore the read only feature
			if (string.IsNullOrEmpty(Navigation.GetStrValue("ForcePrimaryRead_psw")))
				UserContext.Current.SetPersistenceReadOnly(true);
			else
			{
				Navigation.DestroyEntry("ForcePrimaryRead_psw");
				UserContext.Current.SetPersistenceReadOnly(false);
			}

			var requestValues = new NameValueCollection();
			if (queryParams != null)
			{
				// Add to request values
				foreach (var kv in queryParams)
					requestValues.Add(kv.Key, kv.Value);
			}

			IsStateReadonly = true;

			Models.Userp parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_user_PswValNome_ViewModel model = new(UserContext.Current, parentCtx);

			// Table configuration load options
			CSGenio.framework.TableConfiguration.TableConfigurationLoadOptions tableConfigOptions = new CSGenio.framework.TableConfiguration.TableConfigurationLoadOptions();

			// Determine which table configuration to use and load it
			CSGenio.framework.TableConfiguration.TableConfiguration tableConfig = TableUiSettings.Load(
				UserContext.Current.PersistentSupport,
				model.Uuid,
				UserContext.Current.User,
				tableConfigOptions
			).DetermineTableConfig(
				requestModel?.TableConfiguration,
				requestModel?.UserTableConfigName,
				(bool)requestModel?.LoadDefaultView,
				tableConfigOptions
			);

			// Determine rows per page
			tableConfig.RowsPerPage = CSGenio.framework.TableConfiguration.TableConfigurationHelpers.DetermineRowsPerPage(tableConfig.RowsPerPage, perPage, rowsPerPageOptionsString);

			// Determine which columns have totalizers
			tableConfig.TotalizerColumns = requestModel.TotalizerColumns;

			// For tables with multiple selection enabled, determine currently selected rows
			tableConfig.SelectedRows = requestModel.SelectedRows;

			// Add form field filters to the table configuration
			tableConfig.FieldFilters = requestModel.RelatedFilterValues;

			model.setModes(Request.Query["m"].ToString());
			model.Load(tableConfig, requestValues, Request.IsAjaxRequest());

			return JsonOK(model);
		}

		// POST: /Userp/F_user_SaveEdit
		[HttpPost]
		public ActionResult F_user_SaveEdit([FromBody] F_user_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_user_SaveEdit",
				ViewName = "F_user",
				AreaName = "userp",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_USER]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_USER]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_userDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_user_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_user([FromBody] F_userDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
