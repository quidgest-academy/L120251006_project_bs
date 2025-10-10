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

namespace GenioMVC.ViewModels.Chara
{
	public class F_charac_ViewModel : FormViewModel<Models.Chara>, IPreparableForSerialization
	{
		[JsonIgnore]
		public override bool HasWriteConditions { get => false; }

		/// <summary>
		/// Reference for the Models MsqActive property
		/// </summary>
		[JsonIgnore]
		public bool MsqActive { get; set; } = false;

		#region Foreign keys
		/// <summary>
		/// Title: "Title" | Type: "CE"
		/// </summary>
		public string ValMovieid { get; set; }

		#endregion
		/// <summary>
		/// Title: "Photo" | Type: "IJ"
		/// </summary>
		[ImageThumbnailJsonConverter(300, 500)]
		public GenioMVC.Models.ImageModel ValPhoto { get; set; }
		/// <summary>
		/// Title: "Genre" | Type: "AC"
		/// </summary>
		public string ValGenre { get; set; }
		/// <summary>
		/// Title: "" | Type: "PSEUD"
		/// </summary>
		[JsonIgnore]
		public SelectList List_ValGenre { get; set; }
		/// <summary>
		/// Title: "Name" | Type: "C"
		/// </summary>
		public string ValName { get; set; }
		/// <summary>
		/// Title: "Actor name" | Type: "C"
		/// </summary>
		public string ValActorname { get; set; }
		/// <summary>
		/// Title: "Title" | Type: "C"
		/// </summary>
		[ValidateSetAccess]
		public TableDBEdit<GenioMVC.Models.Movie> TableMovieTitle { get; set; }
		/// <summary>
		/// Title: "Create at" | Type: "D"
		/// </summary>
		public DateTime? ValCreateat { get; set; }



		#region Navigations
		#endregion

		#region Auxiliar Keys for Image controls



		#endregion

		#region Extra database fields



		#endregion

		#region Fields for formulas


		#endregion

		public string ValCodchara { get; set; }


		/// <summary>
		/// FOR DESERIALIZATION ONLY
		/// A call to Init() needs to be manually invoked after this constructor
		/// </summary>
		[Obsolete("For deserialization only")]
		public F_charac_ViewModel() : base(null!) { }

		public F_charac_ViewModel(UserContext userContext, bool nestedForm = false) : base(userContext, "FF_CHARAC", nestedForm) { }

		public F_charac_ViewModel(UserContext userContext, Models.Chara row, bool nestedForm = false) : base(userContext, "FF_CHARAC", row, nestedForm) { }

		public F_charac_ViewModel(UserContext userContext, string id, bool nestedForm = false, string[]? fieldsToLoad = null) : this(userContext, nestedForm)
		{
			this.Navigation.SetValue("chara", id);
			Model = Models.Chara.Find(id, userContext, "FF_CHARAC", fieldsToQuery: fieldsToLoad);
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
			Models.Chara model = new Models.Chara(userContext) { Identifier = "FF_CHARAC" };

			var navigation = m_userContext.CurrentNavigation;
			// The "LoadKeysFromHistory" must be after the "LoadEPH" because the PHE's in the tree mark Foreign Keys to null
			// (since they cannot assign multiple values to a single field) and thus the value that comes from Navigation is lost.
			// And this makes it more like the order of loading the model when opening the form.
			model.LoadEPH("FF_CHARAC");
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
		public override void MapFromModel(Models.Chara m)
		{
			if (m == null)
			{
				CSGenio.framework.Log.Error("Map Model (Chara) to ViewModel (F_charac) - Model is a null reference");
				throw new ModelNotFoundException("Model not found");
			}

			try
			{
				ValMovieid = ViewModelConversion.ToString(m.ValMovieid);
				ValPhoto = ViewModelConversion.ToImage(m.ValPhoto);
				ValGenre = ViewModelConversion.ToString(m.ValGenre);
				ValName = ViewModelConversion.ToString(m.ValName);
				ValActorname = ViewModelConversion.ToString(m.ValActorname);
				ValCreateat = ViewModelConversion.ToDateTime(m.ValCreateat);
				ValCodchara = ViewModelConversion.ToString(m.ValCodchara);
			}
			catch (Exception)
			{
				CSGenio.framework.Log.Error("Map Model (Chara) to ViewModel (F_charac) - Error during mapping");
				throw;
			}
		}

		/// <inheritdoc />
		public override void MapToModel()
		{
			MapToModel(this.Model);
		}

		/// <inheritdoc />
		public override void MapToModel(Models.Chara m)
		{
			if (m == null)
			{
				CSGenio.framework.Log.Error("Map ViewModel (F_charac) to Model (Chara) - Model is a null reference");
				throw new ModelNotFoundException("Model not found");
			}

			try
			{
				m.ValMovieid = ViewModelConversion.ToString(ValMovieid);
				if (ValPhoto == null || !ValPhoto.IsThumbnail)
					m.ValPhoto = ViewModelConversion.ToImage(ValPhoto);
				m.ValGenre = ViewModelConversion.ToString(ValGenre);
				m.ValName = ViewModelConversion.ToString(ValName);
				m.ValActorname = ViewModelConversion.ToString(ValActorname);
				m.ValCreateat = ViewModelConversion.ToDateTime(ValCreateat);
				m.ValCodchara = ViewModelConversion.ToString(ValCodchara);
			}
			catch (Exception)
			{
				CSGenio.framework.Log.Error($"Map ViewModel (F_charac) to Model (Chara) - Error during mapping. All user values: {HasDisabledUserValuesSecurity}");
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
					case "chara.movieid":
						this.ValMovieid = ViewModelConversion.ToString(_value);
						break;
					case "chara.photo":
						this.ValPhoto = ViewModelConversion.ToImage(_value);
						break;
					case "chara.genre":
						this.ValGenre = ViewModelConversion.ToString(_value);
						break;
					case "chara.name":
						this.ValName = ViewModelConversion.ToString(_value);
						break;
					case "chara.actorname":
						this.ValActorname = ViewModelConversion.ToString(_value);
						break;
					case "chara.createat":
						this.ValCreateat = ViewModelConversion.ToDateTime(_value);
						break;
					case "chara.codchara":
						this.ValCodchara = ViewModelConversion.ToString(_value);
						break;
					default:
						Log.Error($"SetViewModelValue (F_charac) - Unexpected field identifier {fullFieldName}");
						break;
				}
			}
			catch (Exception ex)
			{
				throw new FrameworkException(Resources.Resources.PEDIMOS_DESCULPA__OC63848, "SetViewModelValue (F_charac)", "Unexpected error", ex);
			}
		}

		#endregion

		/// <summary>
		/// Reads the Model from the database based on the key that is in the history or that was passed through the parameter
		/// </summary>
		/// <param name="id">The primary key of the record that needs to be read from the database. Leave NULL to use the value from the History.</param>
		public override void LoadModel(string id = null)
		{
			try { Model = Models.Chara.Find(id ?? Navigation.GetStrValue("chara"), m_userContext, "FF_CHARAC"); }
			finally { Model ??= new Models.Chara(m_userContext) { Identifier = "FF_CHARAC" }; }

			base.LoadModel();
		}

		public override void Load(NameValueCollection qs, bool editable, bool ajaxRequest = false, bool lazyLoad = false)
		{
			this.editable = editable;
			CSGenio.business.Area oldvalues = null;

			// TODO: Deve ser substituido por search do CSGenioA
			try
			{
				Model = Models.Chara.Find(Navigation.GetStrValue("chara"), m_userContext, "FF_CHARAC");
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

			Model.Identifier = "FF_CHARAC";
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

		protected override void LoadDocumentsProperties(Models.Chara row)
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
				Model = Models.Chara.Find(Navigation.GetStrValue("chara"), m_userContext, "FF_CHARAC");
				if (Model == null)
				{
					Model = new Models.Chara(m_userContext) { Identifier = "FF_CHARAC" };
					Model.klass.QPrimaryKey = Navigation.GetStrValue("chara");
				}
				MapToModel(Model);
				LoadDocumentsProperties(Model);
			}
			// Add characteristics
			Characs = new List<string>();

			Load_F_characmovietitle___(qs, lazyLoad);
// USE /[MANUAL MOV VIEWMODEL_LOADPARTIAL F_CHARAC]/
		}

// USE /[MANUAL MOV VIEWMODEL_NEW F_CHARAC]/

		// Preencher Qvalues default dos fields do form
		protected override void LoadDefaultValues()
		{
		}

		public override CrudViewModelValidationResult Validate()
		{
			CrudViewModelFieldValidator validator = new(m_userContext.User.Language);


			validator.Required("ValGenre", Resources.Resources.GENRE63303, ViewModelConversion.ToString(ValGenre), FieldType.ARRAY_TEXT.GetFormatting());
			validator.StringLength("ValName", Resources.Resources.NAME31974, ValName, 80);

			validator.Required("ValName", Resources.Resources.NAME31974, ViewModelConversion.ToString(ValName), FieldType.TEXT.GetFormatting());
			validator.StringLength("ValActorname", Resources.Resources.ACTOR_NAME48089, ValActorname, 80);


			return validator.GetResult();
		}

		public override void Init(UserContext userContext)
		{
			base.Init(userContext);
		}
// USE /[MANUAL MOV VIEWMODEL_SAVE F_CHARAC]/
		public override void Save()
		{


			base.Save();
		}

// USE /[MANUAL MOV VIEWMODEL_APPLY F_CHARAC]/

// USE /[MANUAL MOV VIEWMODEL_DUPLICATE F_CHARAC]/

// USE /[MANUAL MOV VIEWMODEL_DESTROY F_CHARAC]/
		public override void Destroy(string id)
		{
			Model = Models.Chara.Find(id, m_userContext, "FF_CHARAC");
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

		/// <summary>
		/// TableMovieTitle -> (DB)
		/// </summary>
		/// <param name="qs"></param>
		/// <param name="lazyLoad">Lazy loading of dropdown items</param>
		public void Load_F_characmovietitle___(NameValueCollection qs, bool lazyLoad = false)
		{
			bool f_characmovietitle___DoLoad = true;
			CriteriaSet f_characmovietitle___Conds = CriteriaSet.And();
			{
				object hValue = Navigation.GetValue("movie", true);
				if (hValue != null && !(hValue is Array) && !string.IsNullOrEmpty(Convert.ToString(hValue)))
				{
					f_characmovietitle___Conds.Equal(CSGenioAmovie.FldCodmovie, hValue);
					this.ValMovieid = DBConversion.ToString(hValue);
				}
			}

			TableMovieTitle = new TableDBEdit<Models.Movie>
			{
				IsLazyLoad = lazyLoad
			};

			if (lazyLoad)
			{
				if (Navigation.CurrentLevel.GetEntry("RETURN_movie") != null)
				{
					this.ValMovieid = Navigation.GetStrValue("RETURN_movie");
					Navigation.CurrentLevel.SetEntry("RETURN_movie", null);
				}
				FillDependant_F_characTableMovieTitle(lazyLoad);
				return;
			}

			if (f_characmovietitle___DoLoad)
			{
				List<ColumnSort> sorts = new List<ColumnSort>();
				ColumnSort requestedSort = GetRequestSort(TableMovieTitle, "sTableMovieTitle", "dTableMovieTitle", qs, "movie");
				if (requestedSort != null)
					sorts.Add(requestedSort);
				sorts.Add(new ColumnSort(new ColumnReference(CSGenioAmovie.FldTitle), SortOrder.Ascending));

				string query = "";
				if (!string.IsNullOrEmpty(qs["TableMovieTitle_tableFilters"]))
					TableMovieTitle.TableFilters = bool.Parse(qs["TableMovieTitle_tableFilters"]);
				else
					TableMovieTitle.TableFilters = false;

				query = qs["qTableMovieTitle"];

				//RS 26.07.2016 O preenchimento da lista de ajuda dos Dbedits passa a basear-se apenas no campo do próprio DbEdit
				// O interface de pesquisa rápida não fica coerente quando se visualiza apenas uma coluna mas a pesquisa faz matching com 5 ou 6 colunas diferentes
				//  tornando confuso to o user porque determinada row foi devolvida quando o Qresult não mostra como o matching foi feito
				CriteriaSet search_filters = CriteriaSet.And();
				if (!string.IsNullOrEmpty(query))
				{
					search_filters.Like(CSGenioAmovie.FldTitle, query + "%");
				}
				f_characmovietitle___Conds.SubSet(search_filters);

				string tryParsePage = qs["pTableMovieTitle"] != null ? qs["pTableMovieTitle"].ToString() : "1";
				int page = !string.IsNullOrEmpty(tryParsePage) ? int.Parse(tryParsePage) : 1;
				int numberItems = CSGenio.framework.Configuration.NrRegDBedit;
				int offset = (page - 1) * numberItems;

				FieldRef[] fields = new FieldRef[] { CSGenioAmovie.FldCodmovie, CSGenioAmovie.FldTitle, CSGenioAmovie.FldZzstate };

// USE /[MANUAL MOV OVERRQ F_CHARAC_MOVIETITLE]/

				// Limitation by Zzstate
				/*
					Records that are currently being inserted or duplicated will also be included.
					Client-side persistence will try to fill the "text" value of that option.
				*/
				if (Navigation.checkFormMode("movie", FormMode.New) || Navigation.checkFormMode("movie", FormMode.Duplicate))
					f_characmovietitle___Conds.SubSet(CriteriaSet.Or()
						.Equal(CSGenioAmovie.FldZzstate, 0)
						.Equal(CSGenioAmovie.FldCodmovie, Navigation.GetStrValue("movie")));
				else
					f_characmovietitle___Conds.Criterias.Add(new Criteria(new ColumnReference(CSGenioAmovie.FldZzstate), CriteriaOperator.Equal, 0));

				FieldRef firstVisibleColumn = new FieldRef("movie", "title");
				ListingMVC<CSGenioAmovie> listing = Models.ModelBase.Where<CSGenioAmovie>(m_userContext, false, f_characmovietitle___Conds, fields, offset, numberItems, sorts, "LED_F_CHARACMOVIETITLE___", true, false, firstVisibleColumn: firstVisibleColumn);

				TableMovieTitle.SetPagination(page, numberItems, listing.HasMore, listing.GetTotal, listing.TotalRecords);
				TableMovieTitle.Query = query;
				TableMovieTitle.Elements = listing.RowsForViewModel<GenioMVC.Models.Movie>((r) => new GenioMVC.Models.Movie(m_userContext, r, true, _fieldsToSerialize_F_CHARACMOVIETITLE___));

				//created by [ MH ] at [ 14.04.2016 ] - Foi alterada a forma de retornar a key do novo registo inserido / editado no form de apoio do DBEdit.
				//last update by [ MH ] at [ 10.05.2016 ] - Validação se key encontra-se no level atual, as chaves dos niveis anteriores devem ser ignorados.
				if (Navigation.CurrentLevel.GetEntry("RETURN_movie") != null)
				{
					this.ValMovieid = Navigation.GetStrValue("RETURN_movie");
					Navigation.CurrentLevel.SetEntry("RETURN_movie", null);
				}

				TableMovieTitle.List = new SelectList(TableMovieTitle.Elements.ToSelectList(x => x.ValTitle, x => x.ValCodmovie,  x => x.ValCodmovie == this.ValMovieid), "Value", "Text", this.ValMovieid);
				FillDependant_F_characTableMovieTitle();
			}
		}

		/// <summary>
		/// Get Dependant fields values -> TableMovieTitle (DB)
		/// </summary>
		/// <param name="PKey">Primary Key of Movie</param>
		public ConcurrentDictionary<string, object> GetDependant_F_characTableMovieTitle(string PKey)
		{
			FieldRef[] refDependantFields = [CSGenioAmovie.FldCodmovie, CSGenioAmovie.FldTitle];

			var returnEmptyDependants = false;
			CriteriaSet wherecodition = CriteriaSet.And();

			// Return default values
			if (GenFunctions.emptyG(PKey) == 1)
				returnEmptyDependants = true;

			// Check if the limit(s) is filled if exists
			// - - - - - - - - - - - - - - - - - - - - -

			if (returnEmptyDependants)
				return GetViewModelFieldValues(refDependantFields);

			PersistentSupport sp = m_userContext.PersistentSupport;
			User u = m_userContext.User;

			CSGenioAmovie tempArea = new(u);

			// Fields to select
			SelectQuery querySelect = new();
			querySelect.PageSize(1);
			foreach (FieldRef field in refDependantFields)
				querySelect.Select(field);

			querySelect.From(tempArea.QSystem, tempArea.TableName, tempArea.Alias)
				.Where(wherecodition.Equal(CSGenioAmovie.FldCodmovie, PKey));

			string[] dependantFields = refDependantFields.Select(f => f.FullName).ToArray();
			QueryUtils.SetInnerJoins(dependantFields, null, tempArea, querySelect);

			ArrayList values = sp.executeReaderOneRow(querySelect);
			bool useDefaults = values.Count == 0;

			if (useDefaults)
				return GetViewModelFieldValues(refDependantFields);
			return GetViewModelFieldValues(refDependantFields, values);
		}

		/// <summary>
		/// Fill Dependant fields values -> TableMovieTitle (DB)
		/// </summary>
		/// <param name="lazyLoad">Lazy loading of dropdown items</param>
		public void FillDependant_F_characTableMovieTitle(bool lazyLoad = false)
		{
			var row = GetDependant_F_characTableMovieTitle(this.ValMovieid);
			try
			{

				// Fill List fields
				this.ValMovieid = ViewModelConversion.ToString(row["movie.codmovie"]);
				TableMovieTitle.Value = (string)row["movie.title"];
				if (GenFunctions.emptyG(this.ValMovieid) == 1)
				{
					this.ValMovieid = "";
					TableMovieTitle.Value = "";
					Navigation.ClearValue("movie");
				}
				else if (lazyLoad)
				{
					TableMovieTitle.SetPagination(1, 0, false, false, 1);
					TableMovieTitle.List = new SelectList(new List<SelectListItem>()
					{
						new SelectListItem
						{
							Value = Convert.ToString(this.ValMovieid),
							Text = Convert.ToString(TableMovieTitle.Value),
							Selected = true
						}
					}, "Value", "Text", this.ValMovieid);
				}

				TableMovieTitle.Selected = this.ValMovieid;
			}
			catch (Exception ex)
			{
				CSGenio.framework.Log.Error(string.Format("FillDependant_Error (TableMovieTitle): {0}; {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
			}
		}

		private readonly string[] _fieldsToSerialize_F_CHARACMOVIETITLE___ = ["Movie", "Movie.ValCodmovie", "Movie.ValZzstate", "Movie.ValTitle"];

		protected override object GetViewModelValue(string identifier, object modelValue)
		{
			return identifier switch
			{
				"chara.movieid" => ViewModelConversion.ToString(modelValue),
				"chara.photo" => ViewModelConversion.ToImage(modelValue),
				"chara.genre" => ViewModelConversion.ToString(modelValue),
				"chara.name" => ViewModelConversion.ToString(modelValue),
				"chara.actorname" => ViewModelConversion.ToString(modelValue),
				"chara.createat" => ViewModelConversion.ToDateTime(modelValue),
				"chara.codchara" => ViewModelConversion.ToString(modelValue),
				"movie.codmovie" => ViewModelConversion.ToString(modelValue),
				"movie.title" => ViewModelConversion.ToString(modelValue),
				_ => modelValue
			};
		}

		/// <inheritdoc/>
		protected override void SetTicketToImageFields()
		{
			if (ValPhoto != null)
				ValPhoto.Ticket = Helpers.Helpers.GetFileTicket(m_userContext.User, CSGenio.business.Area.AreaCHARA, CSGenioAchara.FldPhoto.Field, null, ValCodchara);
		}

		#region Charts


		#endregion

		#region Custom code

// USE /[MANUAL MOV VIEWMODEL_CUSTOM F_CHARAC]/

		#endregion
	}
}
