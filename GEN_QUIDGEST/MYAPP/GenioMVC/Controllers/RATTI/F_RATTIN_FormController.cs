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
using GenioMVC.ViewModels.Ratti;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER RATTI]/

namespace GenioMVC.Controllers
{
	public partial class RattiController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_RATTIN_CANCEL = new("RATTING61217", "F_rattin_Cancel", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_RATTIN_SHOW = new("RATTING61217", "F_rattin_Show", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_RATTIN_NEW = new("RATTING61217", "F_rattin_New", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_RATTIN_EDIT = new("RATTING61217", "F_rattin_Edit", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_RATTIN_DUPLICATE = new("RATTING61217", "F_rattin_Duplicate", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_RATTIN_DELETE = new("RATTING61217", "F_rattin_Delete", "Ratti") { vueRouteName = "form-F_RATTIN", mode = "DELETE" };

		#endregion

		#region F_rattin private

		private void FormHistoryLimits_F_rattin()
		{

		}

		#endregion

		#region F_rattin_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_RATTIN]/

		[HttpPost]
		public ActionResult F_rattin_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_rattin_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Show_GET",
				AreaName = "ratti",
				Location = ACTION_F_RATTIN_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_rattin();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_RATTIN]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_rattin_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_rattin_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_New_GET",
				AreaName = "ratti",
				FormName = "F_RATTIN",
				Location = ACTION_F_RATTIN_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_rattin();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_RATTIN]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Ratti/F_rattin_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_New([FromBody]F_rattin_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_New",
				ViewName = "F_rattin",
				AreaName = "ratti",
				Location = ACTION_F_RATTIN_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_RATTIN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_RATTIN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_RATTIN]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_rattin_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_rattin_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Edit_GET",
				AreaName = "ratti",
				FormName = "F_RATTIN",
				Location = ACTION_F_RATTIN_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_rattin();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_RATTIN]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Ratti/F_rattin_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_Edit([FromBody]F_rattin_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Edit",
				ViewName = "F_rattin",
				AreaName = "ratti",
				Location = ACTION_F_RATTIN_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_RATTIN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_RATTIN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_RATTIN]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_rattin_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_rattin_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Delete_GET",
				AreaName = "ratti",
				FormName = "F_RATTIN",
				Location = ACTION_F_RATTIN_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_rattin();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_RATTIN]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Ratti/F_rattin_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_rattin_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Delete",
				ViewName = "F_rattin",
				AreaName = "ratti",
				Location = ACTION_F_RATTIN_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_RATTIN]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_rattin_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_RATTIN");
		}

		#endregion

		#region F_rattin_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_RATTIN]/

		[HttpPost]
		public ActionResult F_rattin_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_rattin_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Duplicate_GET",
				AreaName = "ratti",
				FormName = "F_RATTIN",
				Location = ACTION_F_RATTIN_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_RATTIN]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Ratti/F_rattin_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_RATTIN]/
		[HttpPost]
		public ActionResult F_rattin_Duplicate([FromBody]F_rattin_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_rattin_Duplicate",
				ViewName = "F_rattin",
				AreaName = "ratti",
				Location = ACTION_F_RATTIN_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_RATTIN]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_RATTIN]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_RATTIN]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_rattin_Cancel

		//
		// GET: /Ratti/F_rattin_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_RATTIN]/
		public ActionResult F_rattin_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Ratti(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("ratti");

// USE /[MANUAL MOV BEFORE_CANCEL F_RATTIN]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_RATTIN]/

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

				Navigation.SetValue("ForcePrimaryRead_ratti", "true", true);
			}

			Navigation.ClearValue("ratti");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		public class F_rattin_MovieValTitleModel : RequestLookupModel
		{
			public F_rattin_ViewModel Model { get; set; }
		}

		//
		// GET: /Ratti/F_rattin_MovieValTitle
		// POST: /Ratti/F_rattin_MovieValTitle
		[ActionName("F_rattin_MovieValTitle")]
		public ActionResult F_rattin_MovieValTitle([FromBody] F_rattin_MovieValTitleModel requestModel)
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

			Models.Ratti parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_rattin_MovieValTitle_ViewModel model = new(UserContext.Current, parentCtx);

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

		public class F_rattin_UserpValNameModel : RequestLookupModel
		{
			public F_rattin_ViewModel Model { get; set; }
		}

		//
		// GET: /Ratti/F_rattin_UserpValName
		// POST: /Ratti/F_rattin_UserpValName
		[ActionName("F_rattin_UserpValName")]
		public ActionResult F_rattin_UserpValName([FromBody] F_rattin_UserpValNameModel requestModel)
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

			Models.Ratti parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_rattin_UserpValName_ViewModel model = new(UserContext.Current, parentCtx);

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
			// Map received value to field - The non-duplication prefix field for foreign key field with the unique value
			model.ValCodmovie = Navigation.GetValue<string>("ratti.codmovie");
			model.Load(tableConfig, requestValues, Request.IsAjaxRequest());

			return JsonOK(model);
		}

		// POST: /Ratti/F_rattin_SaveEdit
		[HttpPost]
		public ActionResult F_rattin_SaveEdit([FromBody] F_rattin_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_rattin_SaveEdit",
				ViewName = "F_rattin",
				AreaName = "ratti",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_RATTIN]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_RATTIN]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_rattinDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_rattin_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_rattin([FromBody] F_rattinDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
