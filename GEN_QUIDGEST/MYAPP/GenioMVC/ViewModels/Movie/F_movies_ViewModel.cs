using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;

using CSGenio.business;
using CSGenio.framework;
using CSGenio.persistence;
using GenioMVC.Helpers;
using GenioMVC.Models.Exception;
using GenioMVC.Models.Navigation;
using Quidgest.Persistence;
using Quidgest.Persistence.GenericQuery;

namespace GenioMVC.ViewModels.Movie
{
	public class F_movies_ViewModel : FormViewModel<Models.Movie>, IPreparableForSerialization
	{
		[JsonIgnore]
		public override bool HasWriteConditions { get => false; }

		/// <summary>
		/// Reference for the Models MsqActive property
		/// </summary>
		[JsonIgnore]
		public bool MsqActive { get; set; } = false;

		#region Foreign keys

		#endregion
		/// <summary>
		/// Title: "Poster" | Type: "IJ"
		/// </summary>
		[ImageThumbnailJsonConverter(100, 50)]
		public GenioMVC.Models.ImageModel ValPoster { get; set; }
		/// <summary>
		/// Title: "Title" | Type: "C"
		/// </summary>
		public string ValTitle { get; set; }
		/// <summary>
		/// Title: "Realease date" | Type: "D"
		/// </summary>
		public DateTime? ValRealease_date { get; set; }
		/// <summary>
		/// Title: "Create at" | Type: "D"
		/// </summary>
		public DateTime? ValCreateat { get; set; }
		/// <summary>
		/// Title: "Movies Genre" | Type: "AC"
		/// </summary>
		public string ValMoviesgenre { get; set; }
		/// <summary>
		/// Title: "" | Type: "PSEUD"
		/// </summary>
		[JsonIgnore]
		public SelectList List_ValMoviesgenre { get; set; }
		/// <summary>
		/// Title: "Discription" | Type: "MO"
		/// </summary>
		public string ValDescription { get; set; }



		#region Navigations
		#endregion

		#region Auxiliar Keys for Image controls



		#endregion

		#region Extra database fields



		#endregion

		#region Fields for formulas


		#endregion

		public string ValCodmovie { get; set; }


		/// <summary>
		/// FOR DESERIALIZATION ONLY
		/// A call to Init() needs to be manually invoked after this constructor
		/// </summary>
		[Obsolete("For deserialization only")]
		public F_movies_ViewModel() : base(null!) { }

		public F_movies_ViewModel(UserContext userContext, bool nestedForm = false) : base(userContext, "FF_MOVIES", nestedForm) { }

		public F_movies_ViewModel(UserContext userContext, Models.Movie row, bool nestedForm = false) : base(userContext, "FF_MOVIES", row, nestedForm) { }

		public F_movies_ViewModel(UserContext userContext, string id, bool nestedForm = false, string[]? fieldsToLoad = null) : this(userContext, nestedForm)
		{
			this.Navigation.SetValue("movie", id);
			Model = Models.Movie.Find(id, userContext, "FF_MOVIES", fieldsToQuery: fieldsToLoad);
			if (Model == null)
				throw new ModelNotFoundException("Model not found");
			InitModel();
		}

		protected override void InitLevels()
		{
			this.RoleToShow = CSGenio.framework.Role.ROLE_1;
			this.RoleToEdit = CSGenio.framework.Role.ROLE_1;
		}

		#region Form conditions

		public override StatusMessage InsertConditions()
		{
			return InsertConditions(m_userContext);
		}

		public static StatusMessage InsertConditions(UserContext userContext)
		{
			var m_userContext = userContext;
			StatusMessage result = new StatusMessage(Status.OK, "");
			Models.Movie model = new Models.Movie(userContext) { Identifier = "FF_MOVIES" };

			var navigation = m_userContext.CurrentNavigation;
			// The "LoadKeysFromHistory" must be after the "LoadEPH" because the PHE's in the tree mark Foreign Keys to null
			// (since they cannot assign multiple values to a single field) and thus the value that comes from Navigation is lost.
			// And this makes it more like the order of loading the model when opening the form.
			model.LoadEPH("FF_MOVIES");
			if (navigation != null)
				model.LoadKeysFromHistory(navigation, navigation.CurrentLevel.Level);

			var tableResult = model.EvaluateTableConditions(ConditionType.INSERT);
			result.MergeStatusMessage(tableResult);
			return result;
		}

		public override StatusMessage UpdateConditions()
		{
			StatusMessage result = new StatusMessage(Status.OK, "");
			var model = Model;

			var tableResult = model.EvaluateTableConditions(ConditionType.UPDATE);
			result.MergeStatusMessage(tableResult);
			return result;
		}

		public override StatusMessage DeleteConditions()
		{
			StatusMessage result = new StatusMessage(Status.OK, "");
			var model = Model;

			var tableResult = model.EvaluateTableConditions(ConditionType.DELETE);
			result.MergeStatusMessage(tableResult);
			return result;
		}

		public override StatusMessage ViewConditions()
		{
			var model = Model;
			StatusMessage result = model.EvaluateTableConditions(ConditionType.VIEW);
			var tableResult = model.EvaluateTableConditions(ConditionType.VIEW);
			result.MergeStatusMessage(tableResult);
			return result;
		}

		public override StatusMessage EvaluateWriteConditions(bool isApply)
		{
			StatusMessage result = new StatusMessage(Status.OK, "");
			return result;
		}

		public StatusMessage EvaluateTableConditions(ConditionType type)
		{
			return Model.EvaluateTableConditions(type);
		}

		#endregion

		#region Mapper

		/// <inheritdoc />
		public override void MapFromModel(Models.Movie m)
		{
			if (m == null)
			{
				CSGenio.framework.Log.Error("Map Model (Movie) to ViewModel (F_movies) - Model is a null reference");
				throw new ModelNotFoundException("Model not found");
			}

			try
			{
				ValPoster = ViewModelConversion.ToImage(m.ValPoster);
				ValTitle = ViewModelConversion.ToString(m.ValTitle);
				ValRealease_date = ViewModelConversion.ToDateTime(m.ValRealease_date);
				ValCreateat = ViewModelConversion.ToDateTime(m.ValCreateat);
				ValMoviesgenre = ViewModelConversion.ToString(m.ValMoviesgenre);
				ValDescription = ViewModelConversion.ToString(m.ValDescription);
				ValCodmovie = ViewModelConversion.ToString(m.ValCodmovie);
			}
			catch (Exception)
			{
				CSGenio.framework.Log.Error("Map Model (Movie) to ViewModel (F_movies) - Error during mapping");
				throw;
			}
		}

		/// <inheritdoc />
		public override void MapToModel()
		{
			MapToModel(this.Model);
		}

		/// <inheritdoc />
		public override void MapToModel(Models.Movie m)
		{
			if (m == null)
			{
				CSGenio.framework.Log.Error("Map ViewModel (F_movies) to Model (Movie) - Model is a null reference");
				throw new ModelNotFoundException("Model not found");
			}

			try
			{
				if (ValPoster == null || !ValPoster.IsThumbnail)
					m.ValPoster = ViewModelConversion.ToImage(ValPoster);
				m.ValTitle = ViewModelConversion.ToString(ValTitle);
				m.ValRealease_date = ViewModelConversion.ToDateTime(ValRealease_date);
				m.ValCreateat = ViewModelConversion.ToDateTime(ValCreateat);
				m.ValMoviesgenre = ViewModelConversion.ToString(ValMoviesgenre);
				m.ValDescription = ViewModelConversion.ToString(ValDescription);
				m.ValCodmovie = ViewModelConversion.ToString(ValCodmovie);
			}
			catch (Exception)
			{
				CSGenio.framework.Log.Error($"Map ViewModel (F_movies) to Model (Movie) - Error during mapping. All user values: {HasDisabledUserValuesSecurity}");
				throw;
			}
		}

		/// <summary>
		/// Sets the value of a single property of the view model based on the provided table and field names.
		/// </summary>
		/// <param name="fullFieldName">The full field name in the format "table.field".</param>
		/// <param name="value">The field value.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="fullFieldName"/> is null.</exception>
		public override void SetViewModelValue(string fullFieldName, object value)
		{
			try
			{
				ArgumentNullException.ThrowIfNull(fullFieldName);
				// Obtain a valid value from JsonValueKind that can come from "prefillValues" during the pre-filling of fields during insertion
				var _value = ViewModelConversion.ToRawValue(value);

				switch (fullFieldName)
				{
					case "movie.poster":
						this.ValPoster = ViewModelConversion.ToImage(_value);
						break;
					case "movie.title":
						this.ValTitle = ViewModelConversion.ToString(_value);
						break;
					case "movie.realease_date":
						this.ValRealease_date = ViewModelConversion.ToDateTime(_value);
						break;
					case "movie.createat":
						this.ValCreateat = ViewModelConversion.ToDateTime(_value);
						break;
					case "movie.moviesgenre":
						this.ValMoviesgenre = ViewModelConversion.ToString(_value);
						break;
					case "movie.description":
						this.ValDescription = ViewModelConversion.ToString(_value);
						break;
					case "movie.codmovie":
						this.ValCodmovie = ViewModelConversion.ToString(_value);
						break;
					default:
						Log.Error($"SetViewModelValue (F_movies) - Unexpected field identifier {fullFieldName}");
						break;
				}
			}
			catch (Exception ex)
			{
				throw new FrameworkException(Resources.Resources.PEDIMOS_DESCULPA__OC63848, "SetViewModelValue (F_movies)", "Unexpected error", ex);
			}
		}

		#endregion

		/// <summary>
		/// Reads the Model from the database based on the key that is in the history or that was passed through the parameter
		/// </summary>
		/// <param name="id">The primary key of the record that needs to be read from the database. Leave NULL to use the value from the History.</param>
		public override void LoadModel(string id = null)
		{
			try { Model = Models.Movie.Find(id ?? Navigation.GetStrValue("movie"), m_userContext, "FF_MOVIES"); }
			finally { Model ??= new Models.Movie(m_userContext) { Identifier = "FF_MOVIES" }; }

			base.LoadModel();
		}

		public override void Load(NameValueCollection qs, bool editable, bool ajaxRequest = false, bool lazyLoad = false)
		{
			this.editable = editable;
			CSGenio.business.Area oldvalues = null;

			// TODO: Deve ser substituido por search do CSGenioA
			try
			{
				Model = Models.Movie.Find(Navigation.GetStrValue("movie"), m_userContext, "FF_MOVIES");
			}
			finally
			{
				if (Model == null)
					throw new ModelNotFoundException("Model not found");

				if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
					LoadDefaultValues();
				else
					oldvalues = Model.klass;
			}

			Model.Identifier = "FF_MOVIES";
			InitModel(qs, lazyLoad);

			if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Edit || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
			{
				// MH - Voltar calcular as formulas to "atualizar" os Qvalues dos fields fixos
				// Conexão deve estar aberta de fora. Podem haver formulas que utilizam funções "manuais".
				// TODO: It needs to be analyzed whether we should disable the security of field filling here. If there is any case where the field with the block condition can only be calculated after the double calculation of the formulas.
				MapToModel(Model);

				// If it's inserting or duplicating, needs to fill the default values.
				if (Navigation.CurrentLevel.FormMode == FormMode.New || Navigation.CurrentLevel.FormMode == FormMode.Duplicate)
				{
					FunctionType funcType = Navigation.CurrentLevel.FormMode == FormMode.New
						? FunctionType.INS
						: FunctionType.DUP;

					Model.baseklass.fillValuesDefault(m_userContext.PersistentSupport, funcType);
				}

				// Preencher operações internas
				Model.klass.fillInternalOperations(m_userContext.PersistentSupport, oldvalues);
				MapFromModel(Model);
			}

			// Load just the selected row primary keys for checklists.
			// Needed for submitting forms incase checklists are in collapsible zones that have not been expanded to load the checklist data.
			LoadChecklistsSelectedIDs();
		}

		protected override void FillExtraProperties()
		{
		}

		protected override void LoadDocumentsProperties(Models.Movie row)
		{
		}

		/// <summary>
		/// Load Partial
		/// </summary>
		/// <param name="lazyLoad">Lazy loading of dropdown items</param>
		public override void LoadPartial(NameValueCollection qs, bool lazyLoad = false)
		{
			// MH [bugfix] - Quando o POST da ficha falha, ao recaregar a view os documentos na BD perdem alguma informação (ex: name do file)
			if (Model == null)
			{
				// Precisamos fazer o Find to obter as chaves dos documentos que já foram anexados
				// TODO: Conseguir passar estas chaves no POST to poder retirar o Find.
				Model = Models.Movie.Find(Navigation.GetStrValue("movie"), m_userContext, "FF_MOVIES");
				if (Model == null)
				{
					Model = new Models.Movie(m_userContext) { Identifier = "FF_MOVIES" };
					Model.klass.QPrimaryKey = Navigation.GetStrValue("movie");
				}
				MapToModel(Model);
				LoadDocumentsProperties(Model);
			}
			// Add characteristics
			Characs = new List<string>();

// USE /[MANUAL MOV VIEWMODEL_LOADPARTIAL F_MOVIES]/
		}

// USE /[MANUAL MOV VIEWMODEL_NEW F_MOVIES]/

		// Preencher Qvalues default dos fields do form
		protected override void LoadDefaultValues()
		{
		}

		public override CrudViewModelValidationResult Validate()
		{
			CrudViewModelFieldValidator validator = new(m_userContext.User.Language);

			validator.StringLength("ValTitle", Resources.Resources.TITLE21885, ValTitle, 80);


			return validator.GetResult();
		}

		public override void Init(UserContext userContext)
		{
			base.Init(userContext);
		}
// USE /[MANUAL MOV VIEWMODEL_SAVE F_MOVIES]/
		public override void Save()
		{


			base.Save();
		}

// USE /[MANUAL MOV VIEWMODEL_APPLY F_MOVIES]/

// USE /[MANUAL MOV VIEWMODEL_DUPLICATE F_MOVIES]/

// USE /[MANUAL MOV VIEWMODEL_DESTROY F_MOVIES]/
		public override void Destroy(string id)
		{
			Model = Models.Movie.Find(id, m_userContext, "FF_MOVIES");
			if (Model == null)
				throw new ModelNotFoundException("Model not found");
			this.flashMessage = Model.Destroy();
		}

		/// <summary>
		/// Load selected row primary keys for all checklists
		/// </summary>
		public void LoadChecklistsSelectedIDs()
		{
		}

		protected override object GetViewModelValue(string identifier, object modelValue)
		{
			return identifier switch
			{
				"movie.poster" => ViewModelConversion.ToImage(modelValue),
				"movie.title" => ViewModelConversion.ToString(modelValue),
				"movie.realease_date" => ViewModelConversion.ToDateTime(modelValue),
				"movie.createat" => ViewModelConversion.ToDateTime(modelValue),
				"movie.moviesgenre" => ViewModelConversion.ToString(modelValue),
				"movie.description" => ViewModelConversion.ToString(modelValue),
				"movie.codmovie" => ViewModelConversion.ToString(modelValue),
				_ => modelValue
			};
		}

		/// <inheritdoc/>
		protected override void SetTicketToImageFields()
		{
			if (ValPoster != null)
				ValPoster.Ticket = Helpers.Helpers.GetFileTicket(m_userContext.User, CSGenio.business.Area.AreaMOVIE, CSGenioAmovie.FldPoster.Field, null, ValCodmovie);
		}

		#region Charts


		#endregion

		#region Custom code

// USE /[MANUAL MOV VIEWMODEL_CUSTOM F_MOVIES]/

		#endregion
	}
}
