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
	public class Ratti : ModelBase
	{
		[JsonIgnore]
		public CSGenioAratti klass { get { return baseklass as CSGenioAratti; } set { baseklass = value; } }

		[Key]
		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		[ShouldSerialize("Ratti.ValCodratti")]
		public string ValCodratti { get { return klass.ValCodratti; } set { klass.ValCodratti = value; } }

		[DisplayName("movie_id")]
		/// <summary>Field : "movie_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Ratti.ValCodmovie")]
		public string ValCodmovie { get { return klass.ValCodmovie; } set { klass.ValCodmovie = value; } }

		private Movie _movie;
		[DisplayName("Movie")]
		[ShouldSerialize("Movie")]
		public virtual Movie Movie
		{
			get
			{
				if (!isEmptyModel && (_movie == null || (!string.IsNullOrEmpty(ValCodmovie) && (_movie.isEmptyModel || _movie.klass.QPrimaryKey != ValCodmovie))))
					_movie = Models.Movie.Find(ValCodmovie, m_userContext, Identifier, _fieldsToSerialize);
				_movie ??= new Models.Movie(m_userContext, true, _fieldsToSerialize);
				return _movie;
			}
			set { _movie = value; }
		}

		[DisplayName("user_id")]
		/// <summary>Field : "user_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Ratti.ValCoduserp")]
		public string ValCoduserp { get { return klass.ValCoduserp; } set { klass.ValCoduserp = value; } }

		private Userp _userp;
		[DisplayName("Userp")]
		[ShouldSerialize("Userp")]
		public virtual Userp Userp
		{
			get
			{
				if (!isEmptyModel && (_userp == null || (!string.IsNullOrEmpty(ValCoduserp) && (_userp.isEmptyModel || _userp.klass.QPrimaryKey != ValCoduserp))))
					_userp = Models.Userp.Find(ValCoduserp, m_userContext, Identifier, _fieldsToSerialize);
				_userp ??= new Models.Userp(m_userContext, true, _fieldsToSerialize);
				return _userp;
			}
			set { _userp = value; }
		}

		[DisplayName("Rate")]
		/// <summary>Field : "Rate" Tipo: "N" Formula:  ""</summary>
		[ShouldSerialize("Ratti.ValRate")]
		[NumericAttribute(0)]
		public decimal? ValRate { get { return Convert.ToDecimal(GenFunctions.RoundQG(klass.ValRate, 0)); } set { klass.ValRate = Convert.ToDecimal(value); } }

		[DisplayName("Rate_at")]
		/// <summary>Field : "Rate_at" Tipo: "DT" Formula: DF "[Now]"</summary>
		[ShouldSerialize("Ratti.ValRatedat")]
		[DataType(DataType.Date)]
		[DateAttribute("DT")]
		public DateTime? ValRatedat { get { return klass.ValRatedat; } set { klass.ValRatedat = value ?? DateTime.MinValue; } }

		[DisplayName("ZZSTATE")]
		[ShouldSerialize("Ratti.ValZzstate")]
		/// <summary>Field: "ZZSTATE", Type: "INT", Formula: ""</summary>
		public virtual int ValZzstate { get { return klass.ValZzstate; } set { klass.ValZzstate = value; } }

		public Ratti(UserContext userContext, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = new CSGenioAratti(userContext.User);
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
		}

		public Ratti(UserContext userContext, CSGenioAratti val, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = val;
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
			FillRelatedAreas(val);
		}

		public void FillRelatedAreas(CSGenioAratti csgenioa)
		{
			if (csgenioa == null)
				return;

			foreach (RequestedField Qfield in csgenioa.Fields.Values)
			{
				switch (Qfield.Area)
				{
					case "movie":
						_movie ??= new Movie(m_userContext, true, _fieldsToSerialize);
						_movie.klass.insertNameValueField(Qfield.FullName, Qfield.Value);
						break;
					case "userp":
						_userp ??= new Userp(m_userContext, true, _fieldsToSerialize);
						_userp.klass.insertNameValueField(Qfield.FullName, Qfield.Value);
						break;
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
		public static Ratti Find(string id, UserContext userCtx, string identifier = null, string[] fieldsToSerialize = null, string[] fieldsToQuery = null)
		{
			var record = Find<CSGenioAratti>(id, userCtx, identifier, fieldsToQuery);
			return record == null ? null : new Ratti(userCtx, record, false, fieldsToSerialize) { Identifier = identifier };
		}

		public static List<Ratti> AllModel(UserContext userCtx, CriteriaSet args = null, string identifier = null)
		{
			return Where<CSGenioAratti>(userCtx, false, args, numRegs: -1, identifier: identifier).RowsForViewModel<Ratti>((r) => new Ratti(userCtx, r));
		}

// USE /[MANUAL MOV MODEL RATTI]/
	}
}
