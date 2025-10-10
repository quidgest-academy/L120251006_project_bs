using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using CSGenio.framework;
using CSGenio.framework.TableConfiguration;
using CSGenio.persistence;
using Quidgest.Persistence.GenericQuery;

namespace CSGenio.business
{
	/// <summary>
	/// For new table configurations (views) in VUE applications
	/// </summary>
    public class TableUiSettings : UserUiSettings
    {
        /// <summary>
        /// Database support.
        /// </summary>
        private PersistentSupport Sp { get; set; }

        /// <summary>
        /// User interface ID.
        /// </summary>
        private string Uuid { get; set; }

        /// <summary>
        /// User.
        /// </summary>
        private User User { get; set; }

        /// <summary>
        /// Gets the default table configuration.
        /// </summary>
        public TableConfiguration DefaultTableConfiguration { get; private set; }

        /// <summary>
        /// Gets the list of all table configuration names.
        /// </summary>
        public List<string> UserTableConfigNames { get; private set; }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TableUiSettings(string key, PersistentSupport sp, string uuid, User user) : base(key)
        {
            Sp = sp;
            Uuid = uuid;
            User = user;
        }

        /// <summary>
        /// Loads user interface settings from cache or database.
        /// </summary>
        /// <param name="sp">The persistence support instance for database operations.</param>
        /// <param name="uuid">The unique identifier for the settings.</param>
        /// <param name="user">The user for whom to load settings.</param>
        /// <returns>A UserUiSettings instance containing the loaded settings.</returns>
        public static TableUiSettings Load(PersistentSupport sp, string uuid, User user, TableConfigurationLoadOptions options = null)
        {
            string cacheKey = GenerateCacheKey(uuid, user);
            TableUiSettings settings = GetFromCache(cacheKey);

            if (settings == null)
            {
                settings = new TableUiSettings(cacheKey, sp, uuid, user);
                settings.LoadUserSettings(sp, uuid, user, options);
                settings.CacheSettings();
            }

            return settings;
        }

        /// <summary>
        /// Loads the names of all available table configurations.
        /// </summary>
        private void LoadUserSettings(PersistentSupport sp, string uuid, User user, TableConfigurationLoadOptions options = null)
        {
            DefaultTableConfiguration = GetTableDefaultConfig(sp, user, uuid, options);
            UserTableConfigNames = GetTableConfigNames(sp, user, uuid);
        }

        /// <summary>
        /// Retrieves settings from cache.
        /// </summary>
        protected new static TableUiSettings GetFromCache(string cacheKey)
        {
            return UserUiSettings.GetFromCache(cacheKey) as TableUiSettings;
        }

		/// <summary>
        /// Parse string-encoded table configuration data to an object.
		/// </summary>
        public static TableConfiguration ParseTableConfigData(string encodedString)
        {
            // Set options to allow converting numbers to strings (used in advanced filters, column filters, searchbar filters)
            JsonSerializerOptions serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            };

            TableConfiguration tableConfiguration;

            try
            {
                tableConfiguration = JsonSerializer.Deserialize<TableConfiguration>(encodedString, serializationOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                tableConfiguration = new TableConfiguration();
            }

            return tableConfiguration;
        }

        /// <summary>
        /// Get a table configuration record from the database.
		/// </summary>
        public static CSGenioAtblcfg GetTableConfigNameRecord(PersistentSupport sp, User user, string uuid, string configName)
        {
            //Get saved configuration
            return CSGenioAtblcfg.searchList(sp, user, CriteriaSet.And()
                .Equal(CSGenioAtblcfg.FldCodpsw, user.Codpsw)
                .Equal(CSGenioAtblcfg.FldUuid, uuid)
                .Equal(CSGenioAtblcfg.FldName, configName),
                new string[] { CSGenioAtblcfg.FldCodtblcfg, CSGenioAtblcfg.FldName.Field, CSGenioAtblcfg.FldConfig.Field, CSGenioAtblcfg.FldUsrsetv.Field })
                .FirstOrDefault();
        }

        /// <summary>
        /// Get a table configuration from the database.
		/// </summary>
        public static TableConfiguration GetTableConfig(PersistentSupport sp, User user, string uuid, string configName, TableConfigurationLoadOptions options = null)
        {
            // Get record from the database
            CSGenioAtblcfg configRecord = GetTableConfigNameRecord(sp, user, uuid, configName);

            // If configuration does not exist
            if (configRecord == null)
                return null;

            // Parse to object
            TableConfiguration tableConfig = ParseTableConfigData(configRecord.ValConfig);

            // Add configuration name
            tableConfig.Name = configRecord.ValName;

            // Add configuration version
            tableConfig.Version = configRecord.ValUsrsetv;

            // Update table configuration with load options and re-save if necessary
            TableConfigurationHelpers.UpdateSavedTableConfiguration(sp, user, configRecord, tableConfig, options);

            return tableConfig;
        }

        /// <summary>
        /// Get default table configuration from the database.
		/// </summary>
        public static TableConfiguration GetTableDefaultConfig(PersistentSupport sp, User user, string uuid, TableConfigurationLoadOptions options = null)
        {
            // Get table configuration and name fields from the default configuration record
            SelectQuery query = new SelectQuery()
                .Select(CSGenioAtblcfg.FldCodtblcfg)
                .Select(CSGenioAtblcfg.FldName)
                .Select(CSGenioAtblcfg.FldConfig)
                .Select(CSGenioAtblcfg.FldUsrsetv)
                .From(Area.AreaTBLCFG)
                .Where(CriteriaSet.And()
                    .Equal(CSGenioAtblcfg.FldCodpsw, user.Codpsw)
                    .Equal(CSGenioAtblcfg.FldUuid, uuid)
                    .Equal(CSGenioAtblcfg.FldIsdefault, 1)
                );

            var result = sp.Execute(query);

            // If configuration does not exist
            if (result.NumRows == 0 || result.NumCols != 4)
                return null;

            string tableConfigPk = DBConversion.ToString(result.GetDirect(0, 0));
            string tableConfigName = DBConversion.ToString(result.GetDirect(0, 1));
            string tableConfigJson = DBConversion.ToString(result.GetDirect(0, 2));
            int tableConfigVersion = DBConversion.ToInteger(result.GetDirect(0, 3));

            // If configuration is empty
            if (string.IsNullOrEmpty(tableConfigJson))
                return null;

            // Parse to object
            TableConfiguration tableConfig = ParseTableConfigData(tableConfigJson);

            // Add configuration name
            tableConfig.Name = tableConfigName;

            // Add configuration version
            tableConfig.Version = tableConfigVersion;

            // Update table configuration with load options accounting for version changes
            bool shouldSave = TableConfigurationHelpers.ApplyTableConfigurationVersionChanges(tableConfig, options);

            // Re-save updated configuration if necessary
            // Should not be done when in maintenance mode
            if (shouldSave && !Maintenance.Current.IsActive)
            {
                try
                {
                    // Update configuration data
                    // Serialize only the properties that should be saved and ignore null values
                    JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                    serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                    string configStr = JsonSerializer.Serialize<TableConfigurationSaved>(tableConfig, serializerOptions);

                    UpdateQuery updateQuery = new UpdateQuery()
                        .Update(Area.AreaTBLCFG)
                        .Set(CSGenioAtblcfg.FldConfig, configStr)
                        .Set(CSGenioAtblcfg.FldUsrsetv, Configuration.UserSettingsVersion)
                        .Where(CriteriaSet.And()
                            .Equal(CSGenioAtblcfg.FldCodtblcfg, tableConfigPk)
                        );

                    sp.openConnection();
                    sp.Execute(updateQuery);
                    sp.closeConnection();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            return tableConfig;
        }

        /// <summary>
        /// Determine which table configuration to use.
		/// </summary>
        public TableConfiguration DetermineTableConfig(PersistentSupport sp, User user, string uuid, TableConfiguration currentTableConfig, string configName = "", bool loadDefaultView = false, TableConfigurationLoadOptions options = null, TableConfiguration defaultTableConfig = null)
        {
            // Default to the current table configuration
            TableConfiguration tableConfig = currentTableConfig;

            // If loading the default configuration
            if (!string.IsNullOrEmpty(uuid) && loadDefaultView)
                tableConfig = DefaultTableConfiguration;
            // If loading a saved table configuration
            else if (!string.IsNullOrEmpty(uuid) && !string.IsNullOrEmpty(configName))
                tableConfig = GetTableConfig(sp, user, uuid, configName, options);

            tableConfig ??= defaultTableConfig ?? new TableConfiguration();

            return tableConfig;
        }

        /// <summary>
        /// Determine which table configuration to use.
		/// </summary>
        public TableConfiguration DetermineTableConfig(TableConfiguration currentTableConfig, string configName = "", bool loadDefaultView = false, TableConfigurationLoadOptions options = null, TableConfiguration defaultTableConfig = null)
        {
            return DetermineTableConfig(Sp, User, Uuid, currentTableConfig, configName, loadDefaultView, options, defaultTableConfig);
        }

        /// <summary>
        /// Gets the names of all available table configurations.
        /// </summary>
        public static List<string> GetTableConfigNames(PersistentSupport sp, User user, string uuid)
        {
            List<CSGenioAtblcfg> userTableConfigs = CSGenioAtblcfg.searchList(sp, user, CriteriaSet.And()
                .Equal(CSGenioAtblcfg.FldCodpsw, user.Codpsw)
                .Equal(CSGenioAtblcfg.FldUuid, uuid),
                new string[] { CSGenioAtblcfg.FldName.Field })
                .ToList();

            return userTableConfigs.Select(c => c.ValName).ToList();
        }

        /// <summary>
        /// Gets the names of all available table configurations.
        /// </summary>
        public List<string> GetTableConfigNames()
        {
            return GetTableConfigNames(Sp, User, Uuid);
        }
    }
}