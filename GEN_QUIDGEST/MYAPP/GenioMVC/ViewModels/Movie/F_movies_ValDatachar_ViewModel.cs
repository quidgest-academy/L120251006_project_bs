using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

using CSGenio.business;
using CSGenio.framework;
using GenioMVC.Helpers;
using GenioMVC.Models.Navigation;
using Quidgest.Persistence;
using Quidgest.Persistence.GenericQuery;

namespace GenioMVC.ViewModels.Movie
{
	public class F_movies_ValDatachar_ViewModel : ViewModelBase
	{
		public TablePartial<Models.TimelineItem> Menu { get; set; }

		/// <summary>
		/// The primary key field.
		/// </summary>
		[JsonIgnore]
		public string ValCodmovie { get; set; }

		/// <summary>
		/// The context of the parent.
		/// </summary>
		[JsonIgnore]
		public Models.ModelBase ParentCtx { get; set; }

		public string Uuid { get => "F_movies_ValDatachar"; }

		/// <summary>
		/// Initializes a new instance of the <see cref="F_movies_ValDatachar_ViewModel" /> class.
		/// </summary>
		/// <param name="userContext">The current user request context</param>
		public F_movies_ValDatachar_ViewModel(UserContext userContext) : base(userContext) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="F_movies_ValDatachar_ViewModel" /> class.
		/// </summary>
		/// <param name="userContext">The current user request context</param>
		/// <param name="parentCtx">The context of the parent</param>
		public F_movies_ValDatachar_ViewModel(UserContext userContext, Models.ModelBase parentCtx) : this(userContext)
		{
			ParentCtx = parentCtx;
		}

		public void Load(int numberListItems, bool ajaxRequest = false)
		{
			Load(numberListItems, new NameValueCollection(), ajaxRequest);
		}

		public void Load(int numberListItems, NameValueCollection requestValues, bool ajaxRequest = false)
		{
			List<Models.TimelineItem> listing = null;
			CriteriaSet conditions = null;
			Load(numberListItems, requestValues, ajaxRequest, false, ref listing, ref conditions);
		}

		public void Load(int numberListItems, NameValueCollection requestValues, bool ajaxRequest, bool isToExport, ref List<Models.TimelineItem> Qlisting, ref CriteriaSet conditions)
		{
			CSGenio.framework.TableConfiguration.TableConfiguration tableConfig = new CSGenio.framework.TableConfiguration.TableConfiguration();

			tableConfig.RowsPerPage = numberListItems;

			Load(tableConfig, requestValues, ajaxRequest, isToExport, ref Qlisting, ref conditions);
		}

		public void Load(CSGenio.framework.TableConfiguration.TableConfiguration tableConfig, NameValueCollection requestValues, bool ajaxRequest = false)
		{
			List<Models.TimelineItem> listing = null;
			CriteriaSet conditions = null;
			Load(tableConfig, requestValues, ajaxRequest, false, ref listing, ref conditions);
		}

		public void Load(CSGenio.framework.TableConfiguration.TableConfiguration tableConfig, NameValueCollection requestValues, bool ajaxRequest, bool isToExport, ref List<Models.TimelineItem> Qlisting, ref CriteriaSet conditions)
		{
			if (ajaxRequest)
				this.Navigation.SetValue("requestValues" + "F_movies_ValDatachar", requestValues);
			else if (!ajaxRequest && this.Navigation.CheckKey("requestValues" + "F_movies_ValDatachar"))
				requestValues = this.Navigation.GetValue<NameValueCollection>("requestValues" + "F_movies_ValDatachar");

			Menu = new TablePartial<Models.TimelineItem>();
			this.ValCodmovie = this.Navigation.GetValue("movie").ToString();
			List<Models.TimelineItem> datalist = new List<Models.TimelineItem>();
			int totalrecords = tableConfig.RowsPerPage;

			Menu.Elements = datalist.Select(item =>
			{
				item.Columns = item.Columns.OrderBy(column => column.Order).ToList();
				return item;
			}).OrderBy(p => p.Data).ToList();
		}

		#region Custom code

// USE /[MANUAL MOV VIEWMODEL_CUSTOM F_MOVIES_VALDATACHAR]/

		#endregion
	}
}
