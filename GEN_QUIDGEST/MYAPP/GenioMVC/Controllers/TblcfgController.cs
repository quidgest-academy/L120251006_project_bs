using CSGenio.business;
using CSGenio.core.framework.table;
using CSGenio.framework;
using Microsoft.AspNetCore.Mvc;

namespace GenioMVC.Controllers.Tblcfg;

/// <summary>
/// Web API controller for managing table configuration operations.
/// Provides endpoints for saving, loading, selecting, deleting, renaming, and copying
/// user-specific table view configurations through HTTP POST requests.
/// </summary>
/// <remarks>
/// This controller acts as a web API layer over the TableConfigurationManager,
/// handling HTTP requests and responses while delegating business logic to the manager.
/// All endpoints return JSON responses and include proper error handling.
/// </remarks>
/// <param name="userContextService">Service providing user context information for the current session.</param>
public class TblcfgController(UserContextService userContextService) : ControllerBase(userContextService)
{
	/// <summary>
	/// Base request model containing the fundamental identifiers needed for table configuration operations.
	/// </summary>
	public class RequestConfigModel
	{
		/// <summary>
		/// Gets or sets the unique identifier of the table whose configuration is being managed.
		/// </summary>
		public string Uuid { get; set; }

		/// <summary>
		/// Gets or sets the name of the table configuration.
		/// </summary>
		public string ConfigName { get; set; }
	}

	/// <summary>
	/// Request model that extends the base model with selection status for configuration operations.
	/// </summary>
	public class RequestConfigSelectedModel : RequestConfigModel
	{
		/// <summary>
		/// Gets or sets whether this configuration should be marked as default.
		/// Values: 1 = set as default, 0 = not default, -1 = no change to default status.
		/// </summary>
		public int IsSelected { get; set; } = -1;
	}

	/// <summary>
	/// Request model for saving table configuration data.
	/// Contains all the information needed to persist a table configuration.
	/// </summary>
	public class RequestConfigSaveModel : RequestConfigSelectedModel
	{
		/// <summary>
		/// Gets or sets the serialized configuration data to be saved.
		/// This typically contains JSON-serialized table settings including column visibility, filters, etc.
		/// </summary>
		public string Data { get; set; }
	}

	/// <summary>
	/// Request model for renaming an existing table configuration.
	/// Contains both the new name and the current name of the configuration to rename.
	/// </summary>
	public class RequestConfigRenameModel : RequestConfigSelectedModel
	{
		/// <summary>
		/// Gets or sets the current name of the configuration that should be renamed.
		/// This is used to identify the existing configuration in the database.
		/// </summary>
		public string RenameFromName { get; set; }
	}

	/// <summary>
	/// Request model for copying an existing table configuration to create a new one.
	/// Contains both the new configuration name and the source configuration to copy from.
	/// </summary>
	public class RequestConfigCopyModel : RequestConfigSelectedModel
	{
		/// <summary>
		/// Gets or sets the name of the existing configuration to copy from.
		/// This configuration will serve as the template for the new configuration.
		/// </summary>
		public string CopyFromName { get; set; }
	}

	/// <summary>
	/// Handles exceptions by extracting appropriate error messages and returning JSON error responses.
	/// </summary>
	/// <param name="e">The exception that occurred during the operation.</param>
	/// <returns>A JSON error response containing the user-friendly error message.</returns>
	/// <remarks>
	/// Business exceptions are handled specially to extract user-friendly messages,
	/// while other exceptions use their standard message property.
	/// </remarks>
	private JsonResult HandleException(Exception e)
	{
		string message = e is BusinessException
			? (e as BusinessException).UserMessage
			: e.Message;
		return JsonERROR(message);
	}

	/// <summary>
	/// Retrieves a specific table configuration for the current user.
	/// </summary>
	/// <param name="requestModel">The request model containing the table UUID and configuration name.</param>
	/// <returns>
	/// A JSON response containing the configuration data and name if found,
	/// or an error response if the configuration doesn't exist.
	/// </returns>
	/// <remarks>
	/// This endpoint fetches saved table configurations from the database.
	/// If the specified configuration is not found, it returns a localized error message.
	/// </remarks>
	[HttpPost]
	public ActionResult GetConfig([FromBody] RequestConfigModel requestModel)
	{
		User user = m_userContext.User;
		string uuid = requestModel.Uuid;
		string configName = requestModel.ConfigName;

		// Get saved configuration
		CSGenioAtblcfg userTableConfig = TableConfigurationManager.GetConfig(user, uuid, configName);

		return userTableConfig == null
			? JsonERROR(string.Format(Translations.Get("A vista com o nome '{0}' não existe.", user.Language), configName))
			: JsonOK(new { config = userTableConfig.ValConfig, configName });
	}

	/// <summary>
	/// Saves a table configuration for the current user.
	/// </summary>
	/// <param name="requestModel">The request model containing all configuration data to save.</param>
	/// <returns>
	/// A JSON success response if the configuration was saved successfully,
	/// or a JSON error response if the operation failed.
	/// </returns>
	/// <remarks>
	/// This endpoint can create new configurations or update existing ones.
	/// If the configuration is marked as default, any previous default configurations
	/// for the same table will be automatically cleared.
	/// The system must not be in maintenance mode for this operation to succeed.
	/// </remarks>
	[HttpPost]
	public ActionResult SaveConfig([FromBody] RequestConfigSaveModel requestModel)
	{
		try
		{
			TableConfigurationManager.SaveConfig(
				m_userContext.User,
				requestModel.Uuid,
				requestModel.ConfigName,
				requestModel.IsSelected,
				requestModel.Data);
			return JsonOK();
		}
		catch (Exception e)
		{
			return HandleException(e);
		}
	}

	/// <summary>
	/// Selects a table configuration as the default for the current user,
	/// or clears the default selection if no configuration name is provided.
	/// </summary>
	/// <param name="requestModel">The request model containing the table UUID and configuration name to select.</param>
	/// <returns>
	/// A JSON success response if the selection was successful,
	/// or a JSON error response if the operation failed.
	/// </returns>
	/// <remarks>
	/// When a configuration name is provided, it becomes the new default and any previously
	/// selected default is cleared. When the configuration name is null or empty,
	/// all default selections for the table are cleared.
	/// The system must not be in maintenance mode for this operation to succeed.
	/// </remarks>
	[HttpPost]
	public ActionResult SelectConfig([FromBody] RequestConfigModel requestModel)
	{
		try
		{
			TableConfigurationManager.SelectConfig(
				m_userContext.User,
				requestModel.Uuid,
				requestModel.ConfigName);
			return JsonOK();
		}
		catch (Exception e)
		{
			return HandleException(e);
		}
	}

	/// <summary>
	/// Deletes a specific table configuration for the current user.
	/// </summary>
	/// <param name="requestModel">The request model containing the table UUID and configuration name to delete.</param>
	/// <returns>
	/// A JSON success response with information about whether the deleted configuration was the default,
	/// or a JSON error response if the operation failed.
	/// </returns>
	/// <remarks>
	/// This endpoint permanently removes the specified configuration from the database.
	/// The response includes a flag indicating whether the deleted configuration was marked as default,
	/// which can be useful for client-side UI updates.
	/// The system must not be in maintenance mode for this operation to succeed.
	/// </remarks>
	[HttpPost]
	public ActionResult DeleteConfig([FromBody] RequestConfigModel requestModel)
	{
		try
		{
			bool deletedDefaultView = TableConfigurationManager.DeleteConfig(
				m_userContext.User,
				requestModel.Uuid,
				requestModel.ConfigName);
			return JsonOK(new { deletedDefaultView });
		}
		catch (Exception e)
		{
			return HandleException(e);
		}
	}

	/// <summary>
	/// Renames an existing table configuration for the current user.
	/// </summary>
	/// <param name="requestModel">The request model containing the new name and the current name of the configuration to rename.</param>
	/// <returns>
	/// A JSON success response if the rename was successful,
	/// or a JSON error response if the operation failed.
	/// </returns>
	/// <remarks>
	/// This endpoint changes the name of an existing configuration and optionally updates
	/// its default status. It validates that the source configuration exists and that
	/// the new name is not already in use.
	/// The system must not be in maintenance mode for this operation to succeed.
	/// </remarks>
	[HttpPost]
	public ActionResult RenameConfig([FromBody] RequestConfigRenameModel requestModel)
	{
		try
		{
			TableConfigurationManager.RenameConfig(
				m_userContext.User,
				requestModel.Uuid,
				requestModel.ConfigName,
				requestModel.IsSelected,
				requestModel.RenameFromName);
			return JsonOK();
		}
		catch (Exception e)
		{
			return HandleException(e);
		}
	}

	/// <summary>
	/// Creates a copy of an existing table configuration with a new name for the current user.
	/// </summary>
	/// <param name="requestModel">The request model containing the new configuration name and the source configuration to copy from.</param>
	/// <returns>
	/// A JSON success response if the copy was successful,
	/// or a JSON error response if the operation failed.
	/// </returns>
	/// <remarks>
	/// This endpoint duplicates an existing configuration with all its settings and data,
	/// creating a new configuration record with a different name. The new configuration
	/// can optionally be marked as default.
	/// It validates that the source configuration exists and that the new name is available.
	/// The system must not be in maintenance mode for this operation to succeed.
	/// </remarks>
	[HttpPost]
	public ActionResult CopyConfig([FromBody] RequestConfigCopyModel requestModel)
	{
		try
		{
			TableConfigurationManager.CopyConfig(
				m_userContext.User,
				requestModel.Uuid,
				requestModel.ConfigName,
				requestModel.IsSelected,
				requestModel.CopyFromName);
			return JsonOK();
		}
		catch (Exception e)
		{
			return HandleException(e);
		}
	}
}
