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
using GenioMVC.ViewModels.Favor;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER FAVOR]/

namespace GenioMVC.Controllers
{
	public partial class FavorController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_FAVORI_CANCEL = new("FAVORITES12182", "F_favori_Cancel", "Favor") { vueRouteName = "form-F_FAVORI", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_FAVORI_SHOW = new("FAVORITES12182", "F_favori_Show", "Favor") { vueRouteName = "form-F_FAVORI", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_FAVORI_NEW = new("FAVORITES12182", "F_favori_New", "Favor") { vueRouteName = "form-F_FAVORI", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_FAVORI_EDIT = new("FAVORITES12182", "F_favori_Edit", "Favor") { vueRouteName = "form-F_FAVORI", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_FAVORI_DUPLICATE = new("FAVORITES12182", "F_favori_Duplicate", "Favor") { vueRouteName = "form-F_FAVORI", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_FAVORI_DELETE = new("FAVORITES12182", "F_favori_Delete", "Favor") { vueRouteName = "form-F_FAVORI", mode = "DELETE" };

		#endregion

		#region F_favori private

		private void FormHistoryLimits_F_favori()
		{

		}

		#endregion

		#region F_favori_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_FAVORI]/

		[HttpPost]
		public ActionResult F_favori_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_favori_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Show_GET",
				AreaName = "favor",
				Location = ACTION_F_FAVORI_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_favori();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_FAVORI]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_favori_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_favori_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_New_GET",
				AreaName = "favor",
				FormName = "F_FAVORI",
				Location = ACTION_F_FAVORI_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_favori();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_FAVORI]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Favor/F_favori_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_New([FromBody]F_favori_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_New",
				ViewName = "F_favori",
				AreaName = "favor",
				Location = ACTION_F_FAVORI_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_FAVORI]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_FAVORI]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_FAVORI]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_favori_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_favori_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Edit_GET",
				AreaName = "favor",
				FormName = "F_FAVORI",
				Location = ACTION_F_FAVORI_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_favori();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_FAVORI]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Favor/F_favori_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_Edit([FromBody]F_favori_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Edit",
				ViewName = "F_favori",
				AreaName = "favor",
				Location = ACTION_F_FAVORI_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_FAVORI]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_FAVORI]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_FAVORI]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_favori_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_favori_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Delete_GET",
				AreaName = "favor",
				FormName = "F_FAVORI",
				Location = ACTION_F_FAVORI_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_favori();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_FAVORI]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Favor/F_favori_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_favori_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Delete",
				ViewName = "F_favori",
				AreaName = "favor",
				Location = ACTION_F_FAVORI_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_FAVORI]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_favori_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_FAVORI");
		}

		#endregion

		#region F_favori_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_FAVORI]/

		[HttpPost]
		public ActionResult F_favori_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_favori_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Duplicate_GET",
				AreaName = "favor",
				FormName = "F_FAVORI",
				Location = ACTION_F_FAVORI_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_FAVORI]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Favor/F_favori_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_FAVORI]/
		[HttpPost]
		public ActionResult F_favori_Duplicate([FromBody]F_favori_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_favori_Duplicate",
				ViewName = "F_favori",
				AreaName = "favor",
				Location = ACTION_F_FAVORI_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_FAVORI]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_FAVORI]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_FAVORI]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_favori_Cancel

		//
		// GET: /Favor/F_favori_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_FAVORI]/
		public ActionResult F_favori_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Favor(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("favor");

// USE /[MANUAL MOV BEFORE_CANCEL F_FAVORI]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_FAVORI]/

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

				Navigation.SetValue("ForcePrimaryRead_favor", "true", true);
			}

			Navigation.ClearValue("favor");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		public class F_favori_UserpValNameModel : RequestLookupModel
		{
			public F_favori_ViewModel Model { get; set; }
		}

		//
		// GET: /Favor/F_favori_UserpValName
		// POST: /Favor/F_favori_UserpValName
		[ActionName("F_favori_UserpValName")]
		public ActionResult F_favori_UserpValName([FromBody] F_favori_UserpValNameModel requestModel)
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

			Models.Favor parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_favori_UserpValName_ViewModel model = new(UserContext.Current, parentCtx);

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

		public class F_favori_MovieValTitleModel : RequestLookupModel
		{
			public F_favori_ViewModel Model { get; set; }
		}

		//
		// GET: /Favor/F_favori_MovieValTitle
		// POST: /Favor/F_favori_MovieValTitle
		[ActionName("F_favori_MovieValTitle")]
		public ActionResult F_favori_MovieValTitle([FromBody] F_favori_MovieValTitleModel requestModel)
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

			Models.Favor parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_favori_MovieValTitle_ViewModel model = new(UserContext.Current, parentCtx);

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
			model.ValCoduserp = Navigation.GetValue<string>("favor.coduserp");
			model.Load(tableConfig, requestValues, Request.IsAjaxRequest());

			return JsonOK(model);
		}

		// POST: /Favor/F_favori_SaveEdit
		[HttpPost]
		public ActionResult F_favori_SaveEdit([FromBody] F_favori_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_favori_SaveEdit",
				ViewName = "F_favori",
				AreaName = "favor",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_FAVORI]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_FAVORI]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_favoriDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_favori_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_favori([FromBody] F_favoriDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
