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
	public class Comme : ModelBase
	{
		[JsonIgnore]
		public CSGenioAcomme klass { get { return baseklass as CSGenioAcomme; } set { baseklass = value; } }

		[Key]
		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		[ShouldSerialize("Comme.ValCodcomme")]
		public string ValCodcomme { get { return klass.ValCodcomme; } set { klass.ValCodcomme = value; } }

		[DisplayName("user_id")]
		/// <summary>Field : "user_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Comme.ValCoduserp")]
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

		[DisplayName("movie_id")]
		/// <summary>Field : "movie_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Comme.ValCodmovie")]
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

		[DisplayName("Post")]
		/// <summary>Field : "Post" Tipo: "MO" Formula:  ""</summary>
		[ShouldSerialize("Comme.ValPost")]
		[DataType(DataType.MultilineText)]
		public string ValPost { get { return klass.ValPost; } set { klass.ValPost = value; } }

		[DisplayName("Create at")]
		/// <summary>Field : "Create at" Tipo: "D" Formula:  ""</summary>
		[ShouldSerialize("Comme.ValCreateat")]
		[DataType(DataType.Date)]
		[DateAttribute("D")]
		public DateTime? ValCreateat { get { return klass.ValCreateat; } set { klass.ValCreateat = value ?? DateTime.MinValue; } }

		[DisplayName("ZZSTATE")]
		[ShouldSerialize("Comme.ValZzstate")]
		/// <summary>Field: "ZZSTATE", Type: "INT", Formula: ""</summary>
		public virtual int ValZzstate { get { return klass.ValZzstate; } set { klass.ValZzstate = value; } }

		public Comme(UserContext userContext, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = new CSGenioAcomme(userContext.User);
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
		}

		public Comme(UserContext userContext, CSGenioAcomme val, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = val;
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
			FillRelatedAreas(val);
		}

		public void FillRelatedAreas(CSGenioAcomme csgenioa)
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
		public static Comme Find(string id, UserContext userCtx, string identifier = null, string[] fieldsToSerialize = null, string[] fieldsToQuery = null)
		{
			var record = Find<CSGenioAcomme>(id, userCtx, identifier, fieldsToQuery);
			return record == null ? null : new Comme(userCtx, record, false, fieldsToSerialize) { Identifier = identifier };
		}

		public static List<Comme> AllModel(UserContext userCtx, CriteriaSet args = null, string identifier = null)
		{
			return Where<CSGenioAcomme>(userCtx, false, args, numRegs: -1, identifier: identifier).RowsForViewModel<Comme>((r) => new Comme(userCtx, r));
		}

// USE /[MANUAL MOV MODEL COMME]/
	}
}
