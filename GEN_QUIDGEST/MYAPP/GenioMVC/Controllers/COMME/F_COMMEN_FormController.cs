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
using GenioMVC.ViewModels.Comme;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER COMME]/

namespace GenioMVC.Controllers
{
	public partial class CommeController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_COMMEN_CANCEL = new("COMMENTS30895", "F_commen_Cancel", "Comme") { vueRouteName = "form-F_COMMEN", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_COMMEN_SHOW = new("COMMENTS30895", "F_commen_Show", "Comme") { vueRouteName = "form-F_COMMEN", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_COMMEN_NEW = new("COMMENTS30895", "F_commen_New", "Comme") { vueRouteName = "form-F_COMMEN", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_COMMEN_EDIT = new("COMMENTS30895", "F_commen_Edit", "Comme") { vueRouteName = "form-F_COMMEN", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_COMMEN_DUPLICATE = new("COMMENTS30895", "F_commen_Duplicate", "Comme") { vueRouteName = "form-F_COMMEN", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_COMMEN_DELETE = new("COMMENTS30895", "F_commen_Delete", "Comme") { vueRouteName = "form-F_COMMEN", mode = "DELETE" };

		#endregion

		#region F_commen private

		private void FormHistoryLimits_F_commen()
		{

		}

		#endregion

		#region F_commen_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_COMMEN]/

		[HttpPost]
		public ActionResult F_commen_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_commen_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Show_GET",
				AreaName = "comme",
				Location = ACTION_F_COMMEN_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_commen();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_COMMEN]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_commen_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_commen_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_New_GET",
				AreaName = "comme",
				FormName = "F_COMMEN",
				Location = ACTION_F_COMMEN_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_commen();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_COMMEN]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Comme/F_commen_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_New([FromBody]F_commen_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_New",
				ViewName = "F_commen",
				AreaName = "comme",
				Location = ACTION_F_COMMEN_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_COMMEN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_COMMEN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_COMMEN]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_commen_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_commen_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Edit_GET",
				AreaName = "comme",
				FormName = "F_COMMEN",
				Location = ACTION_F_COMMEN_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_commen();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_COMMEN]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Comme/F_commen_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_Edit([FromBody]F_commen_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Edit",
				ViewName = "F_commen",
				AreaName = "comme",
				Location = ACTION_F_COMMEN_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_COMMEN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_COMMEN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_COMMEN]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_commen_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_commen_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Delete_GET",
				AreaName = "comme",
				FormName = "F_COMMEN",
				Location = ACTION_F_COMMEN_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_commen();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_COMMEN]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Comme/F_commen_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_commen_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Delete",
				ViewName = "F_commen",
				AreaName = "comme",
				Location = ACTION_F_COMMEN_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_COMMEN]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_commen_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_COMMEN");
		}

		#endregion

		#region F_commen_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_COMMEN]/

		[HttpPost]
		public ActionResult F_commen_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_commen_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Duplicate_GET",
				AreaName = "comme",
				FormName = "F_COMMEN",
				Location = ACTION_F_COMMEN_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_COMMEN]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Comme/F_commen_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_COMMEN]/
		[HttpPost]
		public ActionResult F_commen_Duplicate([FromBody]F_commen_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_commen_Duplicate",
				ViewName = "F_commen",
				AreaName = "comme",
				Location = ACTION_F_COMMEN_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_COMMEN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_COMMEN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_COMMEN]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_commen_Cancel

		//
		// GET: /Comme/F_commen_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_COMMEN]/
		public ActionResult F_commen_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Comme(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("comme");

// USE /[MANUAL MOV BEFORE_CANCEL F_COMMEN]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_COMMEN]/

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

				Navigation.SetValue("ForcePrimaryRead_comme", "true", true);
			}

			Navigation.ClearValue("comme");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		public class F_commen_UserpValNameModel : RequestLookupModel
		{
			public F_commen_ViewModel Model { get; set; }
		}

		//
		// GET: /Comme/F_commen_UserpValName
		// POST: /Comme/F_commen_UserpValName
		[ActionName("F_commen_UserpValName")]
		public ActionResult F_commen_UserpValName([FromBody] F_commen_UserpValNameModel requestModel)
		{
			var queryParams = requestModel.QueryParams;

			int perPage = CSGenio.framework.Configuration.NrRegDBedit;
			string rowsPerPageOptionsString = "";

			// If there was a recent operation on this table then force the primary persistence server to be called and ignore the read only feature
			if (string.IsNullOrEmpty(Navigation.GetStrValue("ForcePrimaryRead_userp")))
				UserContext.Current.SetPersistenceReadOnly(true);
			else
			{
				Navigation.DestroyEntry("ForcePrimaryRead_userp");
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

			Models.Comme parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_commen_UserpValName_ViewModel model = new(UserContext.Current, parentCtx);

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

		public class F_commen_MovieValTitleModel : RequestLookupModel
		{
			public F_commen_ViewModel Model { get; set; }
		}

		//
		// GET: /Comme/F_commen_MovieValTitle
		// POST: /Comme/F_commen_MovieValTitle
		[ActionName("F_commen_MovieValTitle")]
		public ActionResult F_commen_MovieValTitle([FromBody] F_commen_MovieValTitleModel requestModel)
		{
			var queryParams = requestModel.QueryParams;

			int perPage = CSGenio.framework.Configuration.NrRegDBedit;
			string rowsPerPageOptionsString = "";

			// If there was a recent operation on this table then force the primary persistence server to be called and ignore the read only feature
			if (string.IsNullOrEmpty(Navigation.GetStrValue("ForcePrimaryRead_movie")))
				UserContext.Current.SetPersistenceReadOnly(true);
			else
			{
				Navigation.DestroyEntry("ForcePrimaryRead_movie");
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

			Models.Comme parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_commen_MovieValTitle_ViewModel model = new(UserContext.Current, parentCtx);

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

		// POST: /Comme/F_commen_SaveEdit
		[HttpPost]
		public ActionResult F_commen_SaveEdit([FromBody] F_commen_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_commen_SaveEdit",
				ViewName = "F_commen",
				AreaName = "comme",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_COMMEN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_COMMEN]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_commenDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_commen_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_commen([FromBody] F_commenDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
