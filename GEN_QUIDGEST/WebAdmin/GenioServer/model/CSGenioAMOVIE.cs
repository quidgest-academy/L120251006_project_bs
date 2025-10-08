
 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using CSGenio.framework;
using CSGenio.persistence;
using Quidgest.Persistence;
using Quidgest.Persistence.GenericQuery;
using System.Linq;

namespace CSGenio.business
{
	/// <summary>
	/// Movies
	/// </summary>
	public class CSGenioAmovie : DbArea
	{
		/// <summary>
		/// Meta-information on this area
		/// </summary>
		protected readonly static AreaInfo informacao = InicializaAreaInfo();

		public CSGenioAmovie(User user, string module)
		{
            this.user = user;
            this.module = module;
			// USE /[MANUAL MOV CONSTRUTOR MOVIE]/
		}

		public CSGenioAmovie(User user) : this(user, user.CurrentModule)
		{
		}

		/// <summary>
		/// Initializes the metadata relative to the fields of this area
		/// </summary>
		private static void InicializaCampos(AreaInfo info)
		{
			Field Qfield = null;
#pragma warning disable CS0168, S1481 // Variable is declared but never used
			List<ByAreaArguments> argumentsListByArea;
#pragma warning restore CS0168, S1481 // Variable is declared but never used
			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "codmovie", FieldType.KEY_INT);
			Qfield.FieldDescription = "";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "title", FieldType.TEXT);
			Qfield.FieldDescription = "Title";
			Qfield.FieldSize =  80;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "TITLE21885";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "realease_date", FieldType.DATE);
			Qfield.FieldDescription = "Realease date";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "REALEASE_DATE49316";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "createat", FieldType.DATE);
			Qfield.FieldDescription = "Create at";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "CREATE_AT36393";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "moviesgenre", FieldType.ARRAY_TEXT);
			Qfield.FieldDescription = "Movies Genre";
			Qfield.FieldSize =  15;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "MOVIES_GENRE22042";

			Qfield.Dupmsg = "";
            Qfield.ArrayName = "dbo.GetValArrayCmoviegenre";
            Qfield.ArrayClassName = "Moviegenre";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "poster", FieldType.IMAGE);
			Qfield.FieldDescription = "Poster";
			Qfield.FieldSize =  3;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "POSTER52933";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "description", FieldType.MEMO);
			Qfield.FieldDescription = "Discription";
			Qfield.FieldSize =  200;
			Qfield.MQueue = false;
			Qfield.Decimals = 3;
			Qfield.CavDesignation = "DISCRIPTION02169";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "zzstate", FieldType.INTEGER);
			Qfield.FieldDescription = "Estado da ficha";
			info.RegisterFieldDB(Qfield);

		}

		/// <summary>
		/// Initializes metadata for paths direct to other areas
		/// </summary>
		private static void InicializaRelacoes(AreaInfo info)
		{
			// Daughters Relations
			//------------------------------
			info.ChildTable = new ChildRelation[3];
			info.ChildTable[0]= new ChildRelation("favor", new String[] {"movieid"}, DeleteProc.NA);
			info.ChildTable[1]= new ChildRelation("comme", new String[] {"codmovie"}, DeleteProc.NA);
			info.ChildTable[2]= new ChildRelation("chara", new String[] {"movieid"}, DeleteProc.NA);

			// Mother Relations
			//------------------------------
			info.ParentTables = new Dictionary<string, Relation>();
		}

		/// <summary>
		/// Initializes metadata for indirect paths to other areas
		/// </summary>
		private static void InicializaCaminhos(AreaInfo info)
		{
			// Pathways
			//------------------------------
			info.Pathways = new Dictionary<string, string>(0);
		}

		/// <summary>
		/// Initializes metadata for triggers and formula arguments
		/// </summary>
		private static void InicializaFormulas(AreaInfo info)
		{
			// Formulas
			//------------------------------








			//Write conditions
			List<ConditionFormula> conditions = new List<ConditionFormula>();
			info.WriteConditions = conditions.Where(c=> c.IsWriteCondition()).ToList();
			info.CrudConditions = conditions.Where(c=> c.IsCrudCondition()).ToList();

		}

		/// <summary>
		/// static CSGenioAmovie()
		/// </summary>
		private static AreaInfo InicializaAreaInfo()
		{
			AreaInfo info = new AreaInfo();

			// Area meta-information
			info.QSystem="MOV";
			info.TableName="movmovies";
			info.ShadowTabName="";
			info.ShadowTabKeyName="";

			info.PrimaryKeyName="codmovie";
			info.HumanKeyName="title,".TrimEnd(',');
			info.Alias="movie";
			info.IsDomain = true;
			info.PersistenceType = PersistenceType.Database;
			info.AreaDesignation="Movies";
			info.AreaPluralDesignation="Movies";
			info.DescriptionCav="MOVIES48538";

			//sincronização
			info.SyncIncrementalDateStart = TimeSpan.FromHours(8);
			info.SyncIncrementalDateEnd = TimeSpan.FromHours(23);
			info.SyncCompleteHour = TimeSpan.FromHours(0.5);
			info.SyncIncrementalPeriod = TimeSpan.FromHours(1);
			info.BatchSync = 100;
			info.SyncType = SyncType.Central;
            info.SolrList = new List<string>();
        	info.QueuesList = new List<GenioServer.business.QueueGenio>();





			//RS 22.03.2011 I separated in submetodos due to performance problems with the JIT in 64bits
			// that in very large projects took 2 minutes on the first call.
			// After a Microsoft analysis of the JIT algortimo it was revealed that it has a
			// complexity O(n*m) where n are the lines of code and m the number of variables of a function.
			// Tests have revealed that splitting into subfunctions cuts the JIT time by more than half by 64-bit.
			//------------------------------
			InicializaCampos(info);

			//------------------------------
			InicializaRelacoes(info);

			//------------------------------
			InicializaCaminhos(info);

			//------------------------------
			InicializaFormulas(info);

			// Automatic audit stamps in BD
            //------------------------------

            // Documents in DB
            //------------------------------

            // Historics
            //------------------------------

			// Duplication
			//------------------------------

			// Ephs
			//------------------------------
			info.Ephs=new Hashtable();

			// Table minimum roles and access levels
			//------------------------------
            info.QLevel = new QLevel();
            info.QLevel.Query = Role.AUTHORIZED;
            info.QLevel.Create = Role.AUTHORIZED;
            info.QLevel.AlterAlways = Role.AUTHORIZED;
            info.QLevel.RemoveAlways = Role.AUTHORIZED;

      		return info;
		}

		/// <summary>
		/// Meta-information about this area
		/// </summary>
		public override AreaInfo Information
		{
			get { return informacao; }
		}
		/// <summary>
		/// Meta-information about this area
		/// </summary>
		public static AreaInfo GetInformation()
		{
			return informacao;
		}

		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		public static FieldRef FldCodmovie { get { return m_fldCodmovie; } }
		private static FieldRef m_fldCodmovie = new FieldRef("movie", "codmovie");

		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		public string ValCodmovie
		{
			get { return (string)returnValueField(FldCodmovie); }
			set { insertNameValueField(FldCodmovie, value); }
		}

		/// <summary>Field : "Title" Tipo: "C" Formula:  ""</summary>
		public static FieldRef FldTitle { get { return m_fldTitle; } }
		private static FieldRef m_fldTitle = new FieldRef("movie", "title");

		/// <summary>Field : "Title" Tipo: "C" Formula:  ""</summary>
		public string ValTitle
		{
			get { return (string)returnValueField(FldTitle); }
			set { insertNameValueField(FldTitle, value); }
		}

		/// <summary>Field : "Realease date" Tipo: "D" Formula:  ""</summary>
		public static FieldRef FldRealease_date { get { return m_fldRealease_date; } }
		private static FieldRef m_fldRealease_date = new FieldRef("movie", "realease_date");

		/// <summary>Field : "Realease date" Tipo: "D" Formula:  ""</summary>
		public DateTime ValRealease_date
		{
			get { return (DateTime)returnValueField(FldRealease_date); }
			set { insertNameValueField(FldRealease_date, value); }
		}

		/// <summary>Field : "Create at" Tipo: "D" Formula:  ""</summary>
		public static FieldRef FldCreateat { get { return m_fldCreateat; } }
		private static FieldRef m_fldCreateat = new FieldRef("movie", "createat");

		/// <summary>Field : "Create at" Tipo: "D" Formula:  ""</summary>
		public DateTime ValCreateat
		{
			get { return (DateTime)returnValueField(FldCreateat); }
			set { insertNameValueField(FldCreateat, value); }
		}

		/// <summary>Field : "Movies Genre" Tipo: "AC" Formula:  ""</summary>
		public static FieldRef FldMoviesgenre { get { return m_fldMoviesgenre; } }
		private static FieldRef m_fldMoviesgenre = new FieldRef("movie", "moviesgenre");

		/// <summary>Field : "Movies Genre" Tipo: "AC" Formula:  ""</summary>
		public string ValMoviesgenre
		{
			get { return (string)returnValueField(FldMoviesgenre); }
			set { insertNameValueField(FldMoviesgenre, value); }
		}

		/// <summary>Field : "Poster" Tipo: "IJ" Formula:  ""</summary>
		public static FieldRef FldPoster { get { return m_fldPoster; } }
		private static FieldRef m_fldPoster = new FieldRef("movie", "poster");

		/// <summary>Field : "Poster" Tipo: "IJ" Formula:  ""</summary>
		public byte[] ValPoster
		{
			get { return (byte[])returnValueField(FldPoster); }
			set { insertNameValueField(FldPoster, value); }
		}

		/// <summary>Field : "Discription" Tipo: "MO" Formula:  ""</summary>
		public static FieldRef FldDescription { get { return m_fldDescription; } }
		private static FieldRef m_fldDescription = new FieldRef("movie", "description");

		/// <summary>Field : "Discription" Tipo: "MO" Formula:  ""</summary>
		public string ValDescription
		{
			get { return (string)returnValueField(FldDescription); }
			set { insertNameValueField(FldDescription, value); }
		}

		/// <summary>Field : "ZZSTATE" Type: "INT" Formula:  ""</summary>
		public static FieldRef FldZzstate { get { return m_fldZzstate; } }
		private static FieldRef m_fldZzstate = new FieldRef("movie", "zzstate");



		/// <summary>Field : "ZZSTATE" Type: "INT"</summary>
		public int ValZzstate
		{
			get { return (int)returnValueField(FldZzstate); }
			set { insertNameValueField(FldZzstate, value); }
		}

        /// <summary>
        /// Obtains a partially populated area with the record corresponding to a primary key
        /// </summary>
        /// <param name="sp">Persistent support from where to get the registration</param>
        /// <param name="key">The value of the primary key</param>
        /// <param name="user">The context of the user</param>
        /// <param name="fields">The fields to be filled in the area</param>
        /// <returns>An area with the fields requests of the record read or null if the key does not exist</returns>
        /// <remarks>Persistence operations should not be used on a partially positioned register</remarks>
        public static CSGenioAmovie search(PersistentSupport sp, string key, User user, string[] fields = null)
        {
			if (string.IsNullOrEmpty(key))
				return null;

		    CSGenioAmovie area = new CSGenioAmovie(user, user.CurrentModule);

            if (sp.getRecord(area, key, fields))
                return area;
			return null;
        }


		public static string GetkeyFromControlledRecord(PersistentSupport sp, string ID, User user)
		{
			if (informacao.ControlledRecords != null)
				return informacao.ControlledRecords.GetPrimaryKeyFromControlledRecord(sp, user, ID);
			return String.Empty;
		}


        /// <summary>
        /// Search for all records of this area that comply with a condition
        /// </summary>
        /// <param name="sp">Persistent support from where to get the list</param>
        /// <param name="user">The context of the user</param>
        /// <param name="where">The search condition for the records. Use null to get all records</param>
        /// <param name="fields">The fields to be filled in the area</param>
        /// <param name="distinct">Get distinct from fields</param>
        /// <param name="noLock">NOLOCK</param>
        /// <returns>A list of area records with all fields populated</returns>
        /// <remarks>Persistence operations should not be used on a partially positioned register</remarks>
        public static List<CSGenioAmovie> searchList(PersistentSupport sp, User user, CriteriaSet where, string[] fields = null, bool distinct = false, bool noLock = false)
        {
				return sp.searchListWhere<CSGenioAmovie>(where, user, fields, distinct, noLock);
        }



       	/// <summary>
        /// Search for all records of this area that comply with a condition
        /// </summary>
        /// <param name="sp">Persistent support from where to get the list</param>
        /// <param name="user">The context of the user</param>
        /// <param name="where">The search condition for the records. Use null to get all records</param>
        /// <param name="listing">List configuration</param>
        /// <returns>A list of area records with all fields populated</returns>
        /// <remarks>Persistence operations should not be used on a partially positioned register</remarks>
        public static void searchListAdvancedWhere(PersistentSupport sp, User user, CriteriaSet where, ListingMVC<CSGenioAmovie> listing)
        {
			sp.searchListAdvancedWhere<CSGenioAmovie>(where, listing);
        }




		/// <summary>
		/// Check if a record exist
		/// </summary>
		/// <param name="key">Record key</param>
		/// <param name="sp">DB conecntion</param>
		/// <returns>True if the record exist</returns>
		public static bool RecordExist(string key, PersistentSupport sp) => DbArea.RecordExist(key, informacao, sp);








		// USE /[MANUAL MOV TABAUX MOVIE]/

 

        

	}
}
