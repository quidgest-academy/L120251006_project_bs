using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using CSGenio.business;
using CSGenio.persistence;
using Quidgest.Persistence;

namespace CSGenio.framework.TableConfiguration
{
    public class ToStringArrayConverter : System.Text.Json.Serialization.JsonConverter<string[]>
    {
        public override string[] Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            List<string> stringList = new List<string>();
            object[] array;
            try
            {
                // Deserialize to an object array
                array = JsonSerializer.Deserialize<object[]>(JsonElement.ParseValue(ref reader));
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message);
                array = new object[0];
            }

            // Convert all values to strings
            foreach (object item in array) 
            {
                stringList.Add(item.ToString());
            }

            return stringList.ToArray();
        }

        public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
        {
            string serializedArray;
            try 
            {
                // Serialize array
                serializedArray = JsonSerializer.Serialize(value);
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message);
                serializedArray = "[]";
            }

            // Write value as raw so it is an array of strings
            writer.WriteRawValue(serializedArray);
        }
    }

    public class SearchFilter
    {
        [JsonPropertyName("name")]
		public string Name { get; set; }

        [JsonPropertyName("active")]
		public bool Active { get; set; }

        [JsonPropertyName("conditions")]
		public SearchFilterCondition[] Conditions { get; set; }
    }

    public class SearchFilterCondition
    {
        [JsonPropertyName("name")]
		public string Name { get; set; }

        [JsonPropertyName("active")]
		public bool Active { get; set; }

        [JsonPropertyName("field")]
		public string Field { get; set; }

        [JsonPropertyName("operator")]
		public string Operator { get; set; }

        [JsonPropertyName("values")]
		[System.Text.Json.Serialization.JsonConverter(typeof(ToStringArrayConverter))]
        public string[] Values { get; set; }
    }

    public class ActiveFilter
    {
        [JsonPropertyName("date")]
		public string Date { get; set; }

        [JsonPropertyName("active")]
		public bool Active { get; set; }

        [JsonPropertyName("inactive")]
		public bool Inactive { get; set; }

        [JsonPropertyName("future")]
		public bool Future { get; set; }
    }

    public class ColumnOrderBy
    {
        [JsonPropertyName("columnName")]
		public string ColumnName { get; set; }

        [JsonPropertyName("sortOrder")]
		public string SortOrder { get; set; }
    }

    public class ColumnConfiguration
    {
        [JsonPropertyName("name")]
		public string Name { get; set; }

        [JsonPropertyName("order")]
		public int Order { get; set; }

        [JsonPropertyName("visibility")]
		public int Visibility { get; set; }

        [JsonPropertyName("exportability")]
		public int Exportability { get; set; }
		
		/// <summary>
		/// Get the table name of the field
		/// Where the full name is in the form of [TABLE].Val[COLUMN], this returns [TABLE]
		/// </summary>
		/// <param name="mainTableName">Name of the main table</param>
		/// <param name="name">Name of the field: [TABLE].Val[COLUMN] || Val[COLUMN]</param>
		public static string getTableName(string mainTableName, string name)
		{
			if (string.IsNullOrEmpty(mainTableName) || string.IsNullOrEmpty(name))
				return null;

			int sepIndex = name.IndexOf('.');

			// If the table name is empty, the field is in the main table
			if (sepIndex == -1)
				return mainTableName;

			// Table name is everything before '.'
			return name.Substring(0, sepIndex).ToLowerInvariant();
		}

		/// <summary>
		/// Get the column name of the field
		/// Where the full name is in the form of [TABLE].Val[COLUMN], this returns [COLUMN]
		/// </summary>
		/// <param name="name">Name of the field: [TABLE].Val[COLUMN] || Val[COLUMN]</param>
		public static string getColumnName(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;

			int sepIndex = name.IndexOf(".Val");
			int sepLenth = 4;

			// If the name does not include the table name
			if (sepIndex == -1)
			{
				sepIndex = name.IndexOf("Val");
				sepLenth = 3;
			}

            // If the name is invalid
            if (sepIndex == -1)
				return null;

            // Column name is everything after ".Val or "Val"
            return name.Substring(sepIndex + sepLenth).ToLowerInvariant();
		}

		/// <summary>
		/// Get a FieldRef of the field
		/// </summary>
		/// <param name="mainTableName">Name of the main table</param>
		/// <param name="name">Name of the field: [TABLE].Val[COLUMN] || Val[COLUMN]</param>
		public static FieldRef getFieldRef(string mainTableName, string name)
		{
			if (string.IsNullOrEmpty(mainTableName) || string.IsNullOrEmpty(name))
				return null;

			return new FieldRef(getTableName(mainTableName, name), getColumnName(name));
		}
    }

    public class ColumnSizing
    {
        [JsonPropertyName("tableSize")]
		public string TableSize { get; set; }

        [JsonPropertyName("columnSizes")]
		public Dictionary<string, string> ColumnSizes { get; set; }
    }

	public class TableConfigurationSaved
	{
        [JsonPropertyName("columnConfiguration")]
        public List<ColumnConfiguration> ColumnConfiguration { get; set; }

        [JsonPropertyName("advancedFilters")]
        public List<SearchFilter> AdvancedFilters { get; set; }

        [JsonPropertyName("columnFilters")]
        public Dictionary<string, SearchFilter> ColumnFilters { get; set; }

        [JsonPropertyName("staticFilters")]
        public Dictionary<string, string> StaticFilters { get; set; } = new Dictionary<string, string>();

        [JsonPropertyName("activeFilter")]
        public ActiveFilter ActiveFilter { get; set; }

        [JsonPropertyName("columnOrderBy")]
        public ColumnOrderBy ColumnOrderBy { get; set; }

        [JsonPropertyName("defaultSearchColumn")]
        public string DefaultSearchColumn { get; set; }

        [JsonPropertyName("columnSizes")]
        public ColumnSizing ColumnSizes { get; set; }

        [JsonPropertyName("lineBreak")]
        public bool LineBreak { get; set; }

        [JsonPropertyName("rowsPerPage")]
        public int RowsPerPage { get; set; }

        [JsonPropertyName("activeViewMode")]
        public string ActiveViewMode { get; set; }
    }

    public class TableConfiguration : TableConfigurationSaved
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
		public int Version { get; set; } = 0;

        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("searchBarFilters")]
        public Dictionary<string, SearchFilter> SearchBarFilters { get; set; }

        [JsonPropertyName("totalizerColumns")]
        public List<string> TotalizerColumns { get; set; } = new List<string>();

        [JsonPropertyName("selectedRows")]
        public List<string> SelectedRows { get; set; }

        // Form field filters - should be ignored in the saved Json, as they're external to the table.
        [JsonIgnore]
        public Dictionary<string, object> FieldFilters { get; set; } = [];

        // Advanced filters, column filters, and searchbar filters merged
        [JsonIgnore]
        public List<SearchFilter> SearchFilters
        {
            get
            {
                List<SearchFilter> searchFilters = new List<SearchFilter>();

                if (AdvancedFilters != null)
                    searchFilters.AddRange(AdvancedFilters);

                if (ColumnFilters != null)
                    searchFilters.AddRange(ColumnFilters.Values);

                if (SearchBarFilters != null)
                    searchFilters.AddRange(SearchBarFilters.Values);

                return searchFilters;
            }
        }

		// First visible column, based on the column configuration.
		[JsonIgnore]
		public ColumnConfiguration FirstVisibleColumnConfig
		{
			get
			{
				if (ColumnConfiguration == null || ColumnConfiguration.Count == 0)
					return null;
				return ColumnConfiguration.FirstOrDefault(column => column.Visibility == 1);
			}
		}

		/// <summary>
		/// Get the first visible column
		/// </summary>
		/// <param name="mainTableName">Name of the main table</param>
		public FieldRef getFirstVisibleColumn(string mainTableName)
		{
			return CSGenio.framework.TableConfiguration.ColumnConfiguration.getFieldRef(mainTableName, FirstVisibleColumnConfig?.Name);
		}

        /// <summary>
		/// Visible column names (TABLE_NAME.COLUMN_NAME), based on the column configuration.
		/// </summary>
        /// <param name="columnConfiguration">Column configuration (order and visibility)</param>
		/// <param name="mainTableName">Name of the main table</param>
        /// <returns>Array of column names</returns>
        public static string[] getVisibleColumnNames(List<CSGenio.framework.TableConfiguration.ColumnConfiguration> columnConfiguration, string mainTableName)
        {
            if (columnConfiguration == null || columnConfiguration.Count == 0)
                return [];

            List<string> visibleColumnNames = new List<string>();

            foreach (ColumnConfiguration columnConfig in columnConfiguration)
            {
                if (columnConfig.Visibility == 1)
                {
                    string tableName = CSGenio.framework.TableConfiguration.ColumnConfiguration.getTableName(mainTableName, columnConfig.Name);
                    string columnName = CSGenio.framework.TableConfiguration.ColumnConfiguration.getColumnName(columnConfig.Name);

                    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                        continue;
                    
                    visibleColumnNames.Add(tableName + "." + columnName);
                }
            }

            return visibleColumnNames?.ToArray();
        }

        /// <summary>
		/// Get all the search filters in the column configuration that are valid and should be applied.
		/// </summary>
        /// <param name="tableConfig">Table configuration</param>
		/// <param name="mainTableName">Name of the main table</param>
        /// <param name="searchableColumnNames">Names of the searchable columns</param>
        /// <returns>Array of search filters</returns>
        public static List<SearchFilter> getValidSearchFilters(CSGenio.framework.TableConfiguration.TableConfiguration tableConfig, string mainTableName, List<string> searchableColumnNames)
        {
			// Clone the current search filters so the originals are not changed
			// The original data must be kept so the invalid filters can be included if they become valid again
            string searchFiltersJSON = JsonSerializer.Serialize(tableConfig.SearchFilters);
            List<SearchFilter> validSearchFilters = JsonSerializer.Deserialize<List<SearchFilter>>(searchFiltersJSON);

            string[] customVisibleColumnNames = getVisibleColumnNames(tableConfig?.ColumnConfiguration, mainTableName);

            List<string> finalSearchableColumnNames;
            if (customVisibleColumnNames != null && customVisibleColumnNames.Length > 0)
                finalSearchableColumnNames = searchableColumnNames.Select((x) => x.ToLowerInvariant()).Where(x => customVisibleColumnNames.Contains(x)).ToList();
            else
                finalSearchableColumnNames = searchableColumnNames.Select((x) => x.ToLowerInvariant()).ToList();

            // Remove conditions that use fields that are not searchable / visible
            foreach (SearchFilter filter in validSearchFilters)
            {
                SearchFilterCondition[] filterConditions = filter.Conditions.Where((condition) => finalSearchableColumnNames.Contains(condition.Field.ToLowerInvariant()))?.ToArray();
                filter.Conditions = filterConditions;
            }

            // Remove filters that have no conditions
            validSearchFilters = validSearchFilters.Where((filter) => filter.Conditions.Length > 0)?.ToList();

            return validSearchFilters;
        }
    }

	public class TableConfigurationLoadOptions
	{
        public Dictionary<string, int> StaticFiltersKeyShiftValues { get; set; }
    }
	
	public class TableConfigurationHelpers
	{
        /// <summary>
        /// Update table configuration in the database
        /// </summary>
        /// <param name="sp">Database support</param>
        /// <param name="user">User</param>
        /// <param name="tableConfigDbRecord">Table configuration database record</param>
        /// <param name="tableConfig">Table configuration</param>
        /// <returns>Success or failure</returns>
        public static bool UpdateTableConfigurationDbRecord(PersistentSupport sp, User user, CSGenioAtblcfg tableConfigDbRecord, TableConfiguration tableConfig)
        {
            bool succeeded = true;

            try
            {
                //< Update configuration data
                // Serialize only the properties that should be saved and ignore null values
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
                serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                string configStr = JsonSerializer.Serialize<TableConfigurationSaved>(tableConfig, serializerOptions);

                tableConfigDbRecord.ValConfig = configStr;
                //> Update configuration data

                // Update version
                tableConfigDbRecord.ValUsrsetv = Configuration.UserSettingsVersion;

                // Save to the database
                sp.openConnection();
                tableConfigDbRecord.change(sp, null);
                sp.closeConnection();
            }
            catch (Exception ex)
            {
                succeeded = false;
                Log.Error(ex.Message);
            }

            return succeeded;
        }

        /// <summary>
        /// Apply updates from load options to a table configuration caused by user settings version changes
        /// </summary>
        /// <param name="tableConfig">Table configuration</param>
        /// <param name="options">Load options</param>
        /// <returns>Whether the data should be re-saved</returns>
        public static bool ApplyTableConfigurationVersionChanges(TableConfiguration tableConfig, TableConfigurationLoadOptions options)
        {
            // Update values for which static filters are selected
            if (tableConfig?.Version < 2 && options?.StaticFiltersKeyShiftValues != null)
                tableConfig.StaticFilters = DetermineStaticFilterValues(tableConfig.StaticFilters, options.StaticFiltersKeyShiftValues);

            return tableConfig.Version < Configuration.UserSettingsVersion;
        }

        /// <summary>
        /// Update table configuration object and database record if necessary
        /// </summary>
        /// <param name="sp">Database support</param>
        /// <param name="user">User</param>
        /// <param name="tableConfigDbRecord">Table configuration database record</param>
        /// <param name="tableConfig">Table configuration</param>
        /// <returns>Success or failure</returns>
        public static bool UpdateSavedTableConfiguration(PersistentSupport sp, User user, CSGenioAtblcfg tableConfigDbRecord, TableConfiguration tableConfig, TableConfigurationLoadOptions options)
        {
            bool succeeded = true;

            // Update table configuration with load options accounting for version changes
            bool shouldSave = ApplyTableConfigurationVersionChanges(tableConfig, options);

            // Save updated table configuration to the database if necessary
            // Should not be done when in maintenance mode
            if (shouldSave && !Maintenance.Current.IsActive)
                succeeded = UpdateTableConfigurationDbRecord(sp, user, tableConfigDbRecord, tableConfig);

            return succeeded;
        }

        /// <summary>
        /// Determine the number of rows per page to use based on the tables options and configuration
        /// </summary>
        /// <param name="tableConfigRowsPerPage">Number of rows per page in table configuration</param>
        /// <param name="defaultRowsPerPage">Default number of rows per page</param>
        /// <param name="rowsPerPageOptionsString">Rows per page options as a string of values separated by commas</param>
        /// <returns>The number of rows per page.</returns>
        public static int DetermineRowsPerPage(int tableConfigRowsPerPage, int defaultRowsPerPage, string rowsPerPageOptionsString)
		{
			List<int> rowsPerPageOptions = new List<int>();

			// Split string into array of string values
			string[] optionsStrArr = string.IsNullOrEmpty(rowsPerPageOptionsString) ? new string[0] : rowsPerPageOptionsString.Split(',');
			int res;

			// Convert string values to integers and add to list
			foreach (string str in optionsStrArr)
			{
				if (int.TryParse(str, out res))
					rowsPerPageOptions.Add(res);
			}

			// If rows per page is the default or a value in the defined options, use it
			if (tableConfigRowsPerPage == defaultRowsPerPage
				|| (rowsPerPageOptions != null
				&& rowsPerPageOptions.Contains(tableConfigRowsPerPage)))
				return tableConfigRowsPerPage;

			// If not, use the default
			return defaultRowsPerPage;
		}

        /// <summary>
        /// Determine the values for the selected static filters, accounting for changes in the filter ID keys
        /// </summary>
        /// <param name="staticFilters">Static filter values in table configuration</param>
        /// <param name="keyShiftValues">Amount to shift each set of static filter values by</param>
        /// <returns>The number of rows per page.</returns>
        public static Dictionary<string, string> DetermineStaticFilterValues(Dictionary<string, string> staticFilters, Dictionary<string, int> keyShiftValues)
		{
            Dictionary<string, string> updatedStaticFilters = new Dictionary<string, string>();

			foreach (var entry in staticFilters)
			{
				List<int> valueArr = new List<int>();
				int parsedValue = 0;

                // Create an array of which filters are selected
                // with each value changed by the value in keyShiftValues that corresponds to this filter group.
				// This is the difference between the old starting value for the filter "order" field and the new one.
                foreach (char filterKey in entry.Value)
					if (int.TryParse(filterKey.ToString(), out parsedValue) && keyShiftValues.ContainsKey(entry.Key))
                        valueArr.Add(parsedValue + keyShiftValues[entry.Key]);

                // Convert the new value of which filters are selected back to a string and add to result
                updatedStaticFilters.Add(entry.Key, string.Join(string.Empty, valueArr));
			}

			return updatedStaticFilters;

        }
    }
}
