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
	public class Favor : ModelBase
	{
		[JsonIgnore]
		public CSGenioAfavor klass { get { return baseklass as CSGenioAfavor; } set { baseklass = value; } }

		[Key]
		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		[ShouldSerialize("Favor.ValCodfavor")]
		public string ValCodfavor { get { return klass.ValCodfavor; } set { klass.ValCodfavor = value; } }

		[DisplayName("user_id")]
		/// <summary>Field : "user_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Favor.ValCoduserp")]
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

		[DisplayName("Favorite at")]
		/// <summary>Field : "Favorite at" Tipo: "D" Formula:  ""</summary>
		[ShouldSerialize("Favor.ValFavorite_at")]
		[DataType(DataType.Date)]
		[DateAttribute("D")]
		public DateTime? ValFavorite_at { get { return klass.ValFavorite_at; } set { klass.ValFavorite_at = value ?? DateTime.MinValue; } }

		[DisplayName("Movie_id")]
		/// <summary>Field : "Movie_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Favor.ValMovieid")]
		public string ValMovieid { get { return klass.ValMovieid; } set { klass.ValMovieid = value; } }

		private Movie _movie;
		[DisplayName("Movie")]
		[ShouldSerialize("Movie")]
		public virtual Movie Movie
		{
			get
			{
				if (!isEmptyModel && (_movie == null || (!string.IsNullOrEmpty(ValMovieid) && (_movie.isEmptyModel || _movie.klass.QPrimaryKey != ValMovieid))))
					_movie = Models.Movie.Find(ValMovieid, m_userContext, Identifier, _fieldsToSerialize);
				_movie ??= new Models.Movie(m_userContext, true, _fieldsToSerialize);
				return _movie;
			}
			set { _movie = value; }
		}

		[DisplayName("ZZSTATE")]
		[ShouldSerialize("Favor.ValZzstate")]
		/// <summary>Field: "ZZSTATE", Type: "INT", Formula: ""</summary>
		public virtual int ValZzstate { get { return klass.ValZzstate; } set { klass.ValZzstate = value; } }

		public Favor(UserContext userContext, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = new CSGenioAfavor(userContext.User);
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
		}

		public Favor(UserContext userContext, CSGenioAfavor val, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = val;
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
			FillRelatedAreas(val);
		}

		public void FillRelatedAreas(CSGenioAfavor csgenioa)
		{
			if (csgenioa == null)
				return;

			foreach (RequestedField Qfield in csgenioa.Fields.Values)
			{
				switch (Qfield.Area)
				{
					case "userp":
						_userp ??= new Userp(m_userContext, true, _fieldsToSerialize);
						_userp.klass.insertNameValueField(Qfield.FullName, Qfield.Value);
						break;
					case "movie":
						_movie ??= new Movie(m_userContext, true, _fieldsToSerialize);
						_movie.klass.insertNameValueField(Qfield.FullName, Qfield.Value);
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
		public static Favor Find(string id, UserContext userCtx, string identifier = null, string[] fieldsToSerialize = null, string[] fieldsToQuery = null)
		{
			var record = Find<CSGenioAfavor>(id, userCtx, identifier, fieldsToQuery);
			return record == null ? null : new Favor(userCtx, record, false, fieldsToSerialize) { Identifier = identifier };
		}

		public static List<Favor> AllModel(UserContext userCtx, CriteriaSet args = null, string identifier = null)
		{
			return Where<CSGenioAfavor>(userCtx, false, args, numRegs: -1, identifier: identifier).RowsForViewModel<Favor>((r) => new Favor(userCtx, r));
		}

// USE /[MANUAL MOV MODEL FAVOR]/
	}
}
