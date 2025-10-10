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
	public class Chara : ModelBase
	{
		[JsonIgnore]
		public CSGenioAchara klass { get { return baseklass as CSGenioAchara; } set { baseklass = value; } }

		[Key]
		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValCodchara")]
		public string ValCodchara { get { return klass.ValCodchara; } set { klass.ValCodchara = value; } }

		[DisplayName("Name")]
		/// <summary>Field : "Name" Tipo: "C" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValName")]
		public string ValName { get { return klass.ValName; } set { klass.ValName = value; } }

		[DisplayName("Actor name")]
		/// <summary>Field : "Actor name" Tipo: "C" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValActorname")]
		public string ValActorname { get { return klass.ValActorname; } set { klass.ValActorname = value; } }

		[DisplayName("Create at")]
		/// <summary>Field : "Create at" Tipo: "D" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValCreateat")]
		[DataType(DataType.Date)]
		[DateAttribute("D")]
		public DateTime? ValCreateat { get { return klass.ValCreateat; } set { klass.ValCreateat = value ?? DateTime.MinValue; } }

		[DisplayName("Genre")]
		/// <summary>Field : "Genre" Tipo: "AC" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValGenre")]
		[DataArray("Genre", GenioMVC.Helpers.ArrayType.Character)]
		public string ValGenre { get { return klass.ValGenre; } set { klass.ValGenre = value; } }
		[JsonIgnore]
		public SelectList ArrayValgenre { get { return new SelectList(CSGenio.business.ArrayGenre.GetDictionary(), "Key", "Value", ValGenre); } set { ValGenre = value.SelectedValue as string; } }

		[DisplayName("Movie_id")]
		/// <summary>Field : "Movie_id" Tipo: "CE" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValMovieid")]
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

		[DisplayName("Photo")]
		/// <summary>Field : "Photo" Tipo: "IJ" Formula:  ""</summary>
		[ShouldSerialize("Chara.ValPhoto")]
		[ImageThumbnailJsonConverter(75, 75)]
		public ImageModel ValPhoto { get { return new ImageModel(klass.ValPhoto) { Ticket = ValPhotoQTicket }; } set { klass.ValPhoto = value; } }
		[JsonIgnore]
		public string ValPhotoQTicket = null;

		[DisplayName("ZZSTATE")]
		[ShouldSerialize("Chara.ValZzstate")]
		/// <summary>Field: "ZZSTATE", Type: "INT", Formula: ""</summary>
		public virtual int ValZzstate { get { return klass.ValZzstate; } set { klass.ValZzstate = value; } }

		public Chara(UserContext userContext, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = new CSGenioAchara(userContext.User);
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
		}

		public Chara(UserContext userContext, CSGenioAchara val, bool isEmpty = false, string[]? fieldsToSerialize = null) : base(userContext)
		{
			klass = val;
			isEmptyModel = isEmpty;
			if (fieldsToSerialize != null)
				SetFieldsToSerialize(fieldsToSerialize);
			FillRelatedAreas(val);
		}

		public void FillRelatedAreas(CSGenioAchara csgenioa)
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
		public static Chara Find(string id, UserContext userCtx, string identifier = null, string[] fieldsToSerialize = null, string[] fieldsToQuery = null)
		{
			var record = Find<CSGenioAchara>(id, userCtx, identifier, fieldsToQuery);
			return record == null ? null : new Chara(userCtx, record, false, fieldsToSerialize) { Identifier = identifier };
		}

		public static List<Chara> AllModel(UserContext userCtx, CriteriaSet args = null, string identifier = null)
		{
			return Where<CSGenioAchara>(userCtx, false, args, numRegs: -1, identifier: identifier).RowsForViewModel<Chara>((r) => new Chara(userCtx, r));
		}

// USE /[MANUAL MOV MODEL CHARA]/
	}
}
