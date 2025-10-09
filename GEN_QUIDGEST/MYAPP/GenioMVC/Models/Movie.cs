using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using CSGenio.business;
using CSGenio.framework;
using CSGenio.persistence;
using GenioMVC.Helpers;
using GenioMVC.Models.Navigation;
using Quidgest.Persistence;
using Quidgest.Persistence.GenericQuery;

using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace GenioMVC.Models
{
	public class Movie : ModelBase
	{
		[JsonIgnore]
		public CSGenioAmovie klass { get { return baseklass as CSGenioAmovie; } set { baseklass = value; } }

		[Key]
		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValCodmovie")]
		public string ValCodmovie { get { return klass.ValCodmovie; } set { klass.ValCodmovie = value; } }

		[DisplayName("Title")]
		/// <summary>Field : "Title" Tipo: "C" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValTitle")]
		public string ValTitle { get { return klass.ValTitle; } set { klass.ValTitle = value; } }

		[DisplayName("Realease date")]
		/// <summary>Field : "Realease date" Tipo: "D" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValRealease_date")]
		[DataType(DataType.Date)]
		[DateAttribute("D")]
		public DateTime? ValRealease_date { get { return klass.ValRealease_date; } set { klass.ValRealease_date = value ?? DateTime.MinValue; } }

		[DisplayName("Create at")]
		/// <summary>Field : "Create at" Tipo: "D" Formula: DF "[Today]"</summary>
		[ShouldSerialize("Movie.ValCreateat")]
		[DataType(DataType.Date)]
		[DateAttribute("D")]
		public DateTime? ValCreateat { get { return klass.ValCreateat; } set { klass.ValCreateat = value ?? DateTime.MinValue; } }

		[DisplayName("Movies Genre")]
		/// <summary>Field : "Movies Genre" Tipo: "AC" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValMoviesgenre")]
		[DataArray("Moviegenre", GenioMVC.Helpers.ArrayType.Character)]
		public string ValMoviesgenre { get { return klass.ValMoviesgenre; } set { klass.ValMoviesgenre = value; } }
		[JsonIgnore]
		public SelectList ArrayValmoviesgenre { get { return new SelectList(CSGenio.business.ArrayMoviegenre.GetDictionary(), "Key", "Value", ValMoviesgenre); } set { ValMoviesgenre = value.SelectedValue as string; } }

		[DisplayName("Poster")]
		/// <summary>Field : "Poster" Tipo: "IJ" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValPoster")]
		[ImageThumbnailJsonConverter(75, 75)]
		public ImageModel ValPoster { get { return new ImageModel(klass.ValPoster) { Ticket = ValPosterQTicket }; } set { klass.ValPoster = value; } }
		[JsonIgnore]
		public string ValPosterQTicket = null;

		[DisplayName("Discription")]
		/// <summary>Field : "Discription" Tipo: "MO" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValDescription")]
		[DataType(DataType.MultilineText)]
		public string ValDescription { get { return klass.ValDescription; } set { klass.ValDescription = value; } }

		[DisplayName("")]
		/// <summary>Field : "" Tipo: "N" Formula: SR "[FAVOR->1]"</summary>
		[ShouldSerialize("Movie.ValNumberoflikes")]
		[NumericAttribute(0)]
		public decimal? ValNumberoflikes { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValNumberoflikes, 0)); } set { klass.ValNumberoflikes = Convert.ToDecimal(value); } }

		[DisplayName("Backdrop")]
		/// <summary>Field : "Backdrop" Tipo: "IJ" Formula:  ""</summary>
		[ShouldSerialize("Movie.ValBackdrop")]
		[ImageThumbnailJsonConverter(75, 75)]
		public ImageModel ValBackdrop { get { return new ImageModel(klass.ValBackdrop) { Ticket = ValBackdropQTicket }; } set { klass.ValBackdrop = value; } }
		[JsonIgnore]
		public string ValBackdropQTicket = null;

		[DisplayName("Average Rate")]
		/// <summary>Field : "Average Rate" Tipo: "N" Formula: + "[MOVIE->SUMAVG]/[MOVIE->TOTALRAT]"</summary>
		[ShouldSerialize("Movie.ValAveragerate")]
		[NumericAttribute(0)]
		public decimal? ValAveragerate { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValAveragerate, 0)); } set { klass.ValAveragerate = Convert.ToDecimal(value); } }

		[DisplayName("Total rate")]
		/// <summary>Field : "Total rate" Tipo: "N" Formula: SR "[RATTI->1]"</summary>
		[ShouldSerialize("Movie.ValTotalrate")]
		[NumericAttribute(0)]
		public decimal? ValTotalrate { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValTotalrate, 0)); } set { klass.ValTotalrate = Convert.ToDecimal(value); } }

		[DisplayName("")]
		/// <summary>Field : "" Tipo: "N" Formula: SR "[RATTI->RATE]"</summary>
		[ShouldSerialize("Movie.ValSumavg")]
		[NumericAttribute(0)]
		public decimal? ValSumavg { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValSumavg, 0)); } set { klass.ValSumavg = Convert.ToDecimal(value); } }

		[DisplayName("LastRate")]
		/// <summary>Field : "LastRate" Tipo: "N" Formula: U1 "RATTI[RATTI->CODRATTI][RATTI->RATE]"</summary>
		[ShouldSerialize("Movie.ValLastrate")]
		[NumericAttribute(0)]
		public decimal? ValLastrate { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValLastrate, 0)); } set { klass.ValLastrate = Convert.ToDecimal(value); } }

		[DisplayName("ZZSTATE")]
		[ShouldSerialize("Movie.ValZzstate")]
		/// <summary>Field: "ZZSTATE", Type: "INT", Formula: ""</summary>
		public virtual int ValZzstate { get { return klass.ValZzstate; } set { klass.ValZzstate = value; } }

		public Movie(UserContext userContext, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = new CSGenioAmovie(userContext.User);
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
		}

		public Movie(UserContext userContext, CSGenioAmovie val, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = val;
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
			FillRelatedAreas(val);
		}

		public void FillRelatedAreas(CSGenioAmovie csgenioa)
		{
			if (csgenioa == null)
				return;

			foreach (RequestedField Qfield in csgenioa.Fields.Values)
			{
				switch (Qfield.Area)
				{
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Search the row by key.
		/// </summary>
		/// <param name="id">The primary key.</param>
		/// <param name="userCtx">The user context.</param>
		/// <param name="identifier">The identifier.</param>
		/// <param name="fieldsToSerialize">The fields to serialize.</param>
		/// <param name="fieldsToQuery">The fields to query.</param>
		/// <returns>Model or NULL</returns>
		public static Movie Find(string id, UserContext userCtx, string identifier = null, string[] fieldsToSerialize = null, string[] fieldsToQuery = null)
		{
			var record = Find<CSGenioAmovie>(id, userCtx, identifier, fieldsToQuery);
			return record == null ? null : new Movie(userCtx, record, false, fieldsToSerialize) { Identifier = identifier };
		}

		public static List<Movie> AllModel(UserContext userCtx, CriteriaSet args = null, string identifier = null)
		{
			return Where<CSGenioAmovie>(userCtx, false, args, numRegs: -1, identifier: identifier).RowsForViewModel<Movie>((r) => new Movie(userCtx, r));
		}

// USE /[MANUAL MOV MODEL MOVIE]/
	}
}
