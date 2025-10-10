using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using JsonPropertyName = System.Text.Json.Serialization.JsonPropertyNameAttribute;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

using CSGenio.business;
using CSGenio.framework;
using GenioMVC.Models;
using GenioMVC.Models.Navigation;
using Quidgest.Persistence;

namespace GenioMVC.ViewModels
{
	public class SearchParams
	{
		public IDictionary<string, IDictionary<string, string>> Filters { get; set; }

		public string Query { get; set; }

		public string SimilarQueries { get; set; }

		public SearchParams()
		{
			Filters = new Dictionary<string, IDictionary<string, string>>();
		}
	}

	public class TablePartial<A>
	{
		/// <summary>
		/// [MH] - [17-08-2015]: Ainda nao usado, mas vai ser refatorizado o Identificador nos menus
		/// </summary>
		[JsonIgnore]
		public string Identifier { get; set; }

		[JsonIgnore]
		public string TableName { get; set; }

		public TablePagination Pagination { get; set; }

		public List<Totalizer> Totalizers { get; set; }

		[JsonIgnore]
		public TableSort Sort { get; set; }

		[JsonIgnore]
		public TableFiltering Filters { get; set; }

		[JsonIgnore]
		public string Query { get; set; }

		[JsonIgnore]
		public bool TableFilters { get; set; }

		public virtual IEnumerable<A> Elements { get; set; }

		// Slot report list
		[JsonIgnore]
		public Dictionary<string, List<object>> Slots { get; set; }

		public bool HasMore => Pagination.HasMore;

		public TablePartial()
		{
			Elements = new List<A>();
			Totalizers = new List<Totalizer>();
			Pagination = new TablePagination(1, 0, false, false, 0);
			Filters = new TableFiltering();
		}

		public void SetPagination(int pageNumber, int itemsNumber, bool hasMore, bool showTotal, int totalRows)
		{
			Pagination = new TablePagination(pageNumber, itemsNumber, hasMore, showTotal, totalRows);
		}

		public void SetTotalizers(List<Totalizer> totalizers)
		{
			Totalizers = new List<Totalizer>(totalizers);
		}

		public void SetSort(string column, string direction)
		{
			Sort = new TableSort(column, direction);
		}

		public void SetFilters(bool showTableFilters, bool hasFilters)
		{
			Filters = new TableFiltering(showTableFilters, hasFilters, new Dictionary<string, string>());
		}
	}

	public class TableDBEdit<A> : TablePartial<A>
	{
		public SelectList List { get; set; }

		public string Selected { get; set; }

		public object Value { get; set; }

		public bool IsLazyLoad { get; set; }

		public TableDBEdit() : base()
		{
			List = new SelectList(new List<string>());
		}

		public override string ToString()
		{
			if (this.Value == null || this.Value is DateTime && (DateTime)this.Value == DateTime.MinValue)
				return String.Empty;
			if (this.Value is DateTime)
				return ((DateTime)this.Value).ToString(System.Globalization.CultureInfo.InvariantCulture);
			return this.Value.ToString();
		}
	}

	public class TableSort
	{
		public string Column { get; set; }

		public string Direction { get; set; }

		public TableSort(string column, string direction)
		{
			Column = column;
			Direction = direction;
		}
	}

	public class TablePagination
	{
		public bool HasTotal { get; set; }

		public int TotalRows { get; set; }

		public bool HasMore { get; set; }

		public int PageNumber { get; set; }

		public int NumberOfItems { get; set; }

		public TablePagination(int pageNumber, int numberOfItems, bool hasMore, bool hasTotal, int totalRows)
		{
			PageNumber = pageNumber;
			NumberOfItems = numberOfItems;
			HasMore = hasMore;
			HasTotal = hasTotal;
			TotalRows = totalRows;
		}
	}

	public class TableFiltering
	{
		public bool ShowTableFilters { get; set; }

		public bool HasFilters { get; set; }

		public Dictionary<string, string> FiltersValues { get; set; }

		public string Query { get; set; }

		public string QueryField { get; set; }

		public FieldRef FilterDateStart { get; set; }

		public FieldRef FilterDateEnd { get; set; }

		/// <summary>
		/// Parameterless constructor for deserializing
		/// </summary>
		public TableFiltering() { }

		public TableFiltering(bool showTableFilters, bool hasFilters, Dictionary<string, string> filtersValues)
		{
			this.ShowTableFilters = showTableFilters;
			this.HasFilters = hasFilters;
			this.FiltersValues = filtersValues;
		}
	}

	public class TableSearchColumn
	{
		public string Field { get; private set; }

		public FieldRef AreaField { get; private set; }

		public Type FieldType { get; private set; }

		public string ArrayName { get; private set; }

		public bool Visible { get; private set; }

		public bool IsDefaultSearch { get; private set; }

		public TableSearchColumn(string field, FieldRef areaField, Type fieldType, bool visible = true, bool defaultSearch = false, string array = null)
		{
			this.Field = field;
			this.AreaField = areaField;
			this.FieldType = fieldType;
			this.ArrayName = array;
			this.Visible = visible;
			this.IsDefaultSearch = defaultSearch;
		}
	}

	public class TableRowCrudButtonPermissions
	{
		[JsonPropertyName("editBtnDisabled")]
		public bool EditBtnDisabled { get; set; } = true;

		[JsonPropertyName("viewBtnDisabled")]
		public bool ViewBtnDisabled { get; set; } = true;

		[JsonPropertyName("deleteBtnDisabled")]
		public bool DeleteBtnDisabled { get; set; } = true;

		[JsonPropertyName("insertBtnDisabled")]
		public bool InsertBtnDisabled { get; set; } = true;

		[JsonPropertyName("duplicateBtnDisabled")]
		public bool DuplicateBtnDisabled { get; set; } = true;
	}

	public class ListColumn
	{
		[JsonPropertyName("order")]
		public int Order { get; set; }

		[JsonPropertyName("area")]
		public string Area { get; set; }

		[JsonPropertyName("field")]
		public string Field { get; set; }

		[JsonPropertyName("foregroundColor")]
		public string ForegroundColor => TextColorFormula?.Invoke() ?? "";

		[JsonPropertyName("backgroundColor")]
		public string BackgroundColor => BackColorFormula?.Invoke() ?? "";

		[JsonIgnore]
		public Func<string> TextColorFormula { get; set; }

		[JsonIgnore]
		public Func<string> BackColorFormula { get; set; }
	}

	public class ListCustomAction
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("isVisible")]
		public bool IsVisible => IsVisibleFormula?.Invoke() ?? true;

		[JsonPropertyName("isBlocked")]
		public bool IsBlocked => IsBlockedFormula?.Invoke() ?? false;

		[JsonIgnore]
		public Func<bool> IsVisibleFormula { get; set; }

		[JsonIgnore]
		public Func<bool> IsBlockedFormula { get; set; }
	}

	public class GridTableList<T> : TablePartial<T> where T : class, ICrudViewModel
	{
		private UserContext m_userContext;

		[JsonPropertyName("elements")]
		public override IEnumerable<T> Elements { get; set; }

		[JsonPropertyName("newElements")]
		public List<T> NewElements { get; set; }

		[JsonPropertyName("editedElements")]
		public List<T> EditedElements { get; set; }

		[JsonPropertyName("removedElements")]
		public List<string> RemovedElements { get; set; }

		[JsonPropertyName("newRecordTemplate")]
		public T NewRecordTemplate { get; set; }

		public T CreateModelBase()
		{
			return Activator.CreateInstance(typeof(T), m_userContext, false) as T ?? throw new InvalidOperationException("Failed to create ModelBase of type " + typeof(T));
		}

		/// <summary>
		/// FOR DESERIALIZATION ONLY
		/// A call to Init() needs to be manually invoked after this constructor
		/// </summary>
		[Obsolete("For deserialization only")]
		public GridTableList() { }

		public void Init(UserContext userContext)
		{
			m_userContext = userContext;
			foreach (var e in NewElements)
				e.Init(userContext);
			foreach (var e in EditedElements)
				e.Init(userContext);
		}

		public GridTableList(UserContext userContext)
		{
			m_userContext = userContext;
			NewRecordTemplate = CreateModelBase();

			// Make the template row have data already calculated
			NewRecordTemplate.NewLoad();

			Elements = [];

			EditedElements = [];
			NewElements = [];
			RemovedElements = [];
		}

		/// <summary>
		/// Validates the elements within the editable table list.
		/// </summary>
		/// <remarks>
		/// This method iterates through both the edited and new elements, invoking the Validate method on each individual
		/// element of type T. The validation results are then merged into a single <see cref="CrudViewModelValidationResult"/>.
		/// </remarks>
		/// <returns>
		/// A <see cref="CrudViewModelValidationResult"/> containing the consolidated validation results for all elements
		/// within the editable table list.
		/// </returns>
		public CrudViewModelValidationResult Validate()
		{
			CrudViewModelValidationResult result = new();

			foreach (var model in EditedElements)
			{
				var partialResult = model.Validate();
				result.Merge(partialResult, $"editedElements[{model.QPrimaryKey}]");
			}

			for (int i = 0; i < NewElements.Count; i++)
			{
				var model = NewElements[i];
				var partialResult = model.Validate();
				result.Merge(partialResult, $"newElements[{i}]");
			}

			return result;
		}

		/// <summary>
		/// Load the empty Models
		/// </summary>
		public void LoadModel()
		{
			foreach (var model in EditedElements)
				model.LoadModel();
		}

		/// <summary>
		/// Performs the mapping of field values from the Model to the ViewModel.
		/// </summary>
		/// <exception cref="ModelNotFoundException">Thrown if Model is null.</exception>
		public void MapFromModel()
		{
			foreach (var model in EditedElements)
				model.MapFromModel();
		}

		/// <summary>
		/// Performs the mapping of field values from the ViewModel to the Model.
		/// </summary>
		/// <exception cref="ModelNotFoundException">Thrown if Model is null.</exception>
		public void MapToModel()
		{
			foreach (var model in EditedElements)
				model.MapToModel();
		}

		public void Save()
		{
			var result = StatusMessage.GetAggregator();

			// 1. Delete rows marked to be deleted
			foreach (string pk in RemovedElements)
			{
				try
				{
					T model = CreateModelBase();
					model.Destroy(pk);
				}
				catch (BusinessException e)
				{
					result.MergeStatusMessage(StatusMessage.Error(e.UserMessage, string.Format("removedElements[{0}]", pk)));
				}
			}

			// 2. Save edited rows
			foreach (T model in EditedElements)
			{
				try
				{
					model.Save();
				}
				catch (FieldValidationException fvExc)
				{
					foreach (var message in fvExc.StatusMessage.GetErrorList())
						result.MergeStatusMessage(StatusMessage.Error(message.Message, string.Format("editedElements[{0}]", model.QPrimaryKey)));
				}
				catch (BusinessException e)
				{
					result.MergeStatusMessage(StatusMessage.Error(e.UserMessage, string.Format("editedElements[{0}]", model.QPrimaryKey)));
				}
			}

			// 3. Insert new rows
			foreach (T model in NewElements)
			{
				try
				{
					// Add the primary key
					model.New();
					model.Save();
				}
				catch (FieldValidationException fvExc)
				{
					foreach (var message in fvExc.StatusMessage.GetErrorList())
						result.MergeStatusMessage(StatusMessage.Error(message.Message, string.Format("newElements[{0}]", NewElements.IndexOf(model))));
				}
				catch (BusinessException e)
				{
					result.MergeStatusMessage(StatusMessage.Error(e.UserMessage, string.Format("newElements[{0}]", NewElements.IndexOf(model))));
				}
			}

			if (result.Status != Status.OK)
				throw new FieldValidationException(result, "Grid table list - Save");
		}
	}

	public abstract class PropertyList<T>: TablePartial<T> where T : ModelBase
	{
		public List<T> propertyListRows;

		public PropertyList() { }

		public abstract void Init(UserContext userContext);

		public void Save()
		{
			var result = StatusMessage.GetAggregator();
			foreach (var row in propertyListRows)
			{
				try
				{
					row.Save();
				}
				catch (FieldValidationException fvExc)
				{
					foreach (var message in fvExc.StatusMessage.GetErrorList())
						result.MergeStatusMessage(StatusMessage.Error(message.Message, string.Format("propertyListRows[{0}]", propertyListRows.IndexOf(row))));
				}
				catch (BusinessException e)
				{
					result.MergeStatusMessage(StatusMessage.Error(e.UserMessage, string.Format("propertyListRows[{0}]", propertyListRows.IndexOf(row))));
				}
			}
		}

		public abstract CrudViewModelValidationResult Validate();

		public abstract void MapFromModels();
	}

	public class PropertyListProperty
	{
		[JsonPropertyName("rowId")]
		public string RowId { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("value")]
		public string Value { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("isRowDirty")]
		public bool IsDirty { get; set; }
	}

	public enum VersionSubmitAction
	{
		Insert, Submit, UnlockFile
	}

	public enum VersionDeleteAction
	{
		LastVersion, Historic, All
	}

	public class RequestDocumGetTicketsModel
	{
		public string FieldName { get; set; } = string.Empty;
		public string KeyValue { get; set; } = string.Empty;
	}

	public class RequestDocumFieldTicket
	{
		[JsonPropertyName("fieldId")]
		public string FieldId { get; set; }
		[JsonPropertyName("ticket")]
		public string Ticket { get; set; }
	}

	public class RequestDocumValidateTickets
	{
		public List<RequestDocumFieldTicket> Tickets { get; set; }
		public bool IsApply { get; set; }
	}

	public class RequestDocumGetModel
	{
		public string? Ticket { get; set; }
		public DocumentViewTypeMode ViewType { get; set; } = DocumentViewTypeMode.Print;
	}

	public class RequestDocumChangeModel : RequestDocumGetModel
	{
		public VersionDeleteAction DeleteType { get; set; } = VersionDeleteAction.All;
		public bool Delete { get; set; }
		public bool Editing { get; set; }
		public string CurrentVersion { get; set; }
	}

	public class RequestDocumsChangeModel
	{
		public List<RequestDocumChangeModel> Documents { get; set; }
	}

	public class RequestDocumsCreateModel
	{
		public string Ticket { get; set; }
		public VersionSubmitAction Mode { get; set; } = VersionSubmitAction.Insert;
		public string Version { get; set; } = "1";
	}
}
