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
using GenioMVC.ViewModels.Chara;
using Quidgest.Persistence.GenericQuery;

// USE /[MANUAL MOV INCLUDE_CONTROLLER CHARA]/

namespace GenioMVC.Controllers
{
	public partial class CharaController : ControllerBase
	{
		#region NavigationLocation Names

		private static readonly NavigationLocation ACTION_F_CHARAC_CANCEL = new("CHARACTERS03735", "F_charac_Cancel", "Chara") { vueRouteName = "form-F_CHARAC", mode = "CANCEL" };
		private static readonly NavigationLocation ACTION_F_CHARAC_SHOW = new("CHARACTERS03735", "F_charac_Show", "Chara") { vueRouteName = "form-F_CHARAC", mode = "SHOW" };
		private static readonly NavigationLocation ACTION_F_CHARAC_NEW = new("CHARACTERS03735", "F_charac_New", "Chara") { vueRouteName = "form-F_CHARAC", mode = "NEW" };
		private static readonly NavigationLocation ACTION_F_CHARAC_EDIT = new("CHARACTERS03735", "F_charac_Edit", "Chara") { vueRouteName = "form-F_CHARAC", mode = "EDIT" };
		private static readonly NavigationLocation ACTION_F_CHARAC_DUPLICATE = new("CHARACTERS03735", "F_charac_Duplicate", "Chara") { vueRouteName = "form-F_CHARAC", mode = "DUPLICATE" };
		private static readonly NavigationLocation ACTION_F_CHARAC_DELETE = new("CHARACTERS03735", "F_charac_Delete", "Chara") { vueRouteName = "form-F_CHARAC", mode = "DELETE" };

		#endregion

		#region F_charac private

		private void FormHistoryLimits_F_charac()
		{

		}

		#endregion

		#region F_charac_Show

// USE /[MANUAL MOV CONTROLLER_SHOW F_CHARAC]/

		[HttpPost]
		public ActionResult F_charac_Show_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_charac_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Show_GET",
				AreaName = "chara",
				Location = ACTION_F_CHARAC_SHOW,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_charac();
// USE /[MANUAL MOV BEFORE_LOAD_SHOW F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_SHOW F_CHARAC]/
				}
			};

			return GenericHandleGetFormShow(eventSink, model, id);
		}

		#endregion

		#region F_charac_New

// USE /[MANUAL MOV CONTROLLER_NEW_GET F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_New_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;
			var prefillValues = requestModel.PrefillValues;

			var model = new F_charac_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_New_GET",
				AreaName = "chara",
				FormName = "F_CHARAC",
				Location = ACTION_F_CHARAC_NEW,
				BeforeAll = (sink, sp) =>
				{
					FormHistoryLimits_F_charac();
				},
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW F_CHARAC]/
				}
			};

			return GenericHandleGetFormNew(eventSink, model, id, isNewLocation, prefillValues);
		}

		//
		// POST: /Chara/F_charac_New
// USE /[MANUAL MOV CONTROLLER_NEW_POST F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_New([FromBody]F_charac_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_New",
				ViewName = "F_charac",
				AreaName = "chara",
				Location = ACTION_F_CHARAC_NEW,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_NEW F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_NEW F_CHARAC]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_NEW_EX F_CHARAC]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_NEW_EX F_CHARAC]/
				}
			};

			return GenericHandlePostFormNew(eventSink, model);
		}

		#endregion

		#region F_charac_Edit

// USE /[MANUAL MOV CONTROLLER_EDIT_GET F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_Edit_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_charac_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Edit_GET",
				AreaName = "chara",
				FormName = "F_CHARAC",
				Location = ACTION_F_CHARAC_EDIT,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_charac();
// USE /[MANUAL MOV BEFORE_LOAD_EDIT F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT F_CHARAC]/
				}
			};

			return GenericHandleGetFormEdit(eventSink, model, id);
		}

		//
		// POST: /Chara/F_charac_Edit
// USE /[MANUAL MOV CONTROLLER_EDIT_POST F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_Edit([FromBody]F_charac_ViewModel model, [FromQuery]bool redirect)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Edit",
				ViewName = "F_charac",
				AreaName = "chara",
				Location = ACTION_F_CHARAC_EDIT,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_EDIT F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_EDIT F_CHARAC]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_EDIT_EX F_CHARAC]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_EDIT_EX F_CHARAC]/
				}
			};

			return GenericHandlePostFormEdit(eventSink, model);
		}

		#endregion

		#region F_charac_Delete

// USE /[MANUAL MOV CONTROLLER_DELETE_GET F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_Delete_GET([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_charac_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Delete_GET",
				AreaName = "chara",
				FormName = "F_CHARAC",
				Location = ACTION_F_CHARAC_DELETE,
				BeforeOp = (sink, sp) =>
				{
					FormHistoryLimits_F_charac();
// USE /[MANUAL MOV BEFORE_LOAD_DELETE F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DELETE F_CHARAC]/
				}
			};

			return GenericHandleGetFormDelete(eventSink, model, id);
		}

		//
		// POST: /Chara/F_charac_Delete
// USE /[MANUAL MOV CONTROLLER_DELETE_POST F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_Delete([FromBody]RequestIdModel requestModel)
		{
			var id = requestModel.Id;
			var model = new F_charac_ViewModel (UserContext.Current, id);
			model.MapFromModel();

			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Delete",
				ViewName = "F_charac",
				AreaName = "chara",
				Location = ACTION_F_CHARAC_DELETE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_DESTROY_DELETE F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_DESTROY_DELETE F_CHARAC]/
				}
			};

			return GenericHandlePostFormDelete(eventSink, model);
		}

		public ActionResult F_charac_Delete_Redirect()
		{
			//FOR: FORM MENU GO BACK
			return RedirectToFormMenuGoBack("F_CHARAC");
		}

		#endregion

		#region F_charac_Duplicate

// USE /[MANUAL MOV CONTROLLER_DUPLICATE_GET F_CHARAC]/

		[HttpPost]
		public ActionResult F_charac_Duplicate_GET([FromBody]RequestNewGetModel requestModel)
		{
			var id = requestModel.Id;
			var isNewLocation = requestModel.IsNewLocation;

			var model = new F_charac_ViewModel(UserContext.Current);
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Duplicate_GET",
				AreaName = "chara",
				FormName = "F_CHARAC",
				Location = ACTION_F_CHARAC_DUPLICATE,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE F_CHARAC]/
				}
			};

			return GenericHandleGetFormDuplicate(eventSink, model, id, isNewLocation);
		}

		//
		// POST: /Chara/F_charac_Duplicate
// USE /[MANUAL MOV CONTROLLER_DUPLICATE_POST F_CHARAC]/
		[HttpPost]
		public ActionResult F_charac_Duplicate([FromBody]F_charac_ViewModel model, [FromQuery]bool redirect = true)
		{
			var eventSink = new EventSink()
			{
				MethodName = "F_charac_Duplicate",
				ViewName = "F_charac",
				AreaName = "chara",
				Location = ACTION_F_CHARAC_DUPLICATE,
				Redirect = redirect,
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_SAVE_DUPLICATE F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_SAVE_DUPLICATE F_CHARAC]/
				},
				BeforeException = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_LOAD_DUPLICATE_EX F_CHARAC]/
				},
				AfterException = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_LOAD_DUPLICATE_EX F_CHARAC]/
				}
			};

			return GenericHandlePostFormDuplicate(eventSink, model);
		}

		#endregion

		#region F_charac_Cancel

		//
		// GET: /Chara/F_charac_Cancel
// USE /[MANUAL MOV CONTROLLER_CANCEL_GET F_CHARAC]/
		public ActionResult F_charac_Cancel()
		{
			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				PersistentSupport sp = UserContext.Current.PersistentSupport;
				try
				{
					var model = new GenioMVC.Models.Chara(UserContext.Current);
					model.klass.QPrimaryKey = Navigation.GetStrValue("chara");

// USE /[MANUAL MOV BEFORE_CANCEL F_CHARAC]/

					sp.openTransaction();
					model.Destroy();
					sp.closeTransaction();

// USE /[MANUAL MOV AFTER_CANCEL F_CHARAC]/

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

				Navigation.SetValue("ForcePrimaryRead_chara", "true", true);
			}

			Navigation.ClearValue("chara");

			return JsonOK(new { Success = true, currentNavigationLevel = Navigation.CurrentLevel.Level });
		}

		#endregion


		public class F_charac_MovieValTitleModel : RequestLookupModel
		{
			public F_charac_ViewModel Model { get; set; }
		}

		//
		// GET: /Chara/F_charac_MovieValTitle
		// POST: /Chara/F_charac_MovieValTitle
		[ActionName("F_charac_MovieValTitle")]
		public ActionResult F_charac_MovieValTitle([FromBody] F_charac_MovieValTitleModel requestModel)
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

			Models.Chara parentCtx = requestModel.Model == null ? null : new(UserContext.Current);
			requestModel.Model?.Init(UserContext.Current);
			requestModel.Model?.MapToModel(parentCtx);
			F_charac_MovieValTitle_ViewModel model = new(UserContext.Current, parentCtx);

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

		// POST: /Chara/F_charac_SaveEdit
		[HttpPost]
		public ActionResult F_charac_SaveEdit([FromBody] F_charac_ViewModel model)
		{
			EventSink eventSink = new()
			{
				MethodName = "F_charac_SaveEdit",
				ViewName = "F_charac",
				AreaName = "chara",
				BeforeOp = (sink, sp) =>
				{
// USE /[MANUAL MOV BEFORE_APPLY_EDIT F_CHARAC]/
				},
				AfterOp = (sink, sp) =>
				{
// USE /[MANUAL MOV AFTER_APPLY_EDIT F_CHARAC]/
				}
			};

			return GenericHandlePostFormApply(eventSink, model);
		}

		public class F_characDocumValidateTickets : RequestDocumValidateTickets
		{
			public F_charac_ViewModel Model { get; set; }
		}

		/// <summary>
		/// Checks if the model is valid and, if so, updates the specified tickets with write permissions
		/// </summary>
		/// <param name="requestModel">The request model with a list of tickets and the form model</param>
		/// <returns>A JSON response with the result of the operation</returns>
		public ActionResult UpdateFilesTicketsF_charac([FromBody] F_characDocumValidateTickets requestModel)
		{
			requestModel.Model.Init(UserContext.Current);
			return UpdateFilesTickets(requestModel.Tickets, requestModel.Model, requestModel.IsApply);
		}
	}
}
