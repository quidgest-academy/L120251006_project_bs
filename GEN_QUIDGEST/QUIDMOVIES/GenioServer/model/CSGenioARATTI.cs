
 
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
	/// Ratting
	/// </summary>
	public class CSGenioAratti : DbArea
	{
		/// <summary>
		/// Meta-information on this area
		/// </summary>
		protected readonly static AreaInfo informacao = InicializaAreaInfo();

		public CSGenioAratti(User user, string module)
		{
            this.user = user;
            this.module = module;
			// USE /[MANUAL MOV CONSTRUTOR RATTI]/
		}

		public CSGenioAratti(User user) : this(user, user.CurrentModule)
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
			Qfield = new Field(info.Alias, "codratti", FieldType.KEY_INT);
			Qfield.FieldDescription = "";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "codmovie", FieldType.KEY_INT);
			Qfield.FieldDescription = "movie_id";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "MOVIE_ID24048";

			Qfield.Dupmsg = "";
            Qfield.SufNDup = "coduserp";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "coduserp", FieldType.KEY_INT);
			Qfield.FieldDescription = "user_id";
			Qfield.FieldSize =  8;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "USER_ID19581";

			Qfield.Dupmsg = "Already Rated Movie";
            Qfield.NotDup = true;
            Qfield.PrefNDup = "codmovie";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "rate", FieldType.NUMERIC);
			Qfield.FieldDescription = "Rate";
			Qfield.FieldSize =  1;
			Qfield.MQueue = false;
			Qfield.IntegerDigits = 1;
			Qfield.CavDesignation = "RATE50728";

			Qfield.Dupmsg = "";
			info.RegisterFieldDB(Qfield);

			//- - - - - - - - - - - - - - - - - - -
			Qfield = new Field(info.Alias, "ratedat", FieldType.DATETIME);
			Qfield.FieldDescription = "Rate_at";
			Qfield.FieldSize =  16;
			Qfield.MQueue = false;
			Qfield.CavDesignation = "RATE_AT01141";

			Qfield.Dupmsg = "";
			argumentsListByArea= new List<ByAreaArguments>();
			Qfield.DefaultValue = new DefaultValue(new InternalOperationFormula(argumentsListByArea, 0, delegate(object []args,User user,string module,PersistentSupport sp) {
				return (object)(DateTime.Now);
			}));

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

			// Mother Relations
			//------------------------------
			info.ParentTables = new Dictionary<string, Relation>();
			info.ParentTables.Add("movie", new Relation("MOV", "movratting", "ratti", "codratti", "codmovie", "MOV", "movmovies", "movie", "codmovie", "codmovie"));
			info.ParentTables.Add("userp", new Relation("MOV", "movratting", "ratti", "codratti", "coduserp", "MOV", "movuserprofile", "userp", "coduserp", "coduserp"));
		}

		/// <summary>
		/// Initializes metadata for indirect paths to other areas
		/// </summary>
		private static void InicializaCaminhos(AreaInfo info)
		{
			// Pathways
			//------------------------------
			info.Pathways = new Dictionary<string, string>(3);
			info.Pathways.Add("movie","movie");
			info.Pathways.Add("userp","userp");
			info.Pathways.Add("psw","userp");
		}

		/// <summary>
		/// Initializes metadata for triggers and formula arguments
		/// </summary>
		private static void InicializaFormulas(AreaInfo info)
		{
			// Formulas
			//------------------------------
			//Actualiza as seguintes somas relacionadas:
			info.RelatedSumArgs = new List<RelatedSumArgument>();
			info.RelatedSumArgs.Add( new RelatedSumArgument("ratti", "movie", "totalrate", "1", '+', false));
			info.RelatedSumArgs.Add( new RelatedSumArgument("ratti", "movie", "sumavg", "rate", '+', true));



			//Actualiza as seguintes rotinas de ultimo Qvalue:
			info.LastValueArgs = new List<LastValueArgument>();
			info.LastValueArgs.Add( new LastValueArgument("movie",
				new string [] {"lastrate"},
				new string [] {"rate"},
				"codratti",
				null,

				null, false));


			info.DefaultValues = new string[] {
			 "ratedat"
			};






			//Write conditions
			List<ConditionFormula> conditions = new List<ConditionFormula>();
			info.WriteConditions = conditions.Where(c=> c.IsWriteCondition()).ToList();
			info.CrudConditions = conditions.Where(c=> c.IsCrudCondition()).ToList();

		}

		/// <summary>
		/// static CSGenioAratti()
		/// </summary>
		private static AreaInfo InicializaAreaInfo()
		{
			AreaInfo info = new AreaInfo();

			// Area meta-information
			info.QSystem="MOV";
			info.TableName="movratting";
			info.ShadowTabName="";
			info.ShadowTabKeyName="";

			info.PrimaryKeyName="codratti";
			info.HumanKeyName="";
			info.Alias="ratti";
			info.IsDomain = true;
			info.PersistenceType = PersistenceType.Database;
			info.AreaDesignation="Ratting";
			info.AreaPluralDesignation="Ratting";
			info.DescriptionCav="RATTING61217";

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
			EPHField[] camposEPH;
						camposEPH = new EPHField[1];
			camposEPH[0] = new EPHField("USER_ACESS", "favor", "coduserp", "=", "ratti", "coduserp", "=", false , false);
			info.Ephs.Add(new Par("MOV", "5"), camposEPH);

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
		public static FieldRef FldCodratti { get { return m_fldCodratti; } }
		private static FieldRef m_fldCodratti = new FieldRef("ratti", "codratti");

		/// <summary>Field : "" Tipo: "+" Formula:  ""</summary>
		public string ValCodratti
		{
			get { return (string)returnValueField(FldCodratti); }
			set { insertNameValueField(FldCodratti, value); }
		}

		/// <summary>Field : "movie_id" Tipo: "CE" Formula:  ""</summary>
		public static FieldRef FldCodmovie { get { return m_fldCodmovie; } }
		private static FieldRef m_fldCodmovie = new FieldRef("ratti", "codmovie");

		/// <summary>Field : "movie_id" Tipo: "CE" Formula:  ""</summary>
		public string ValCodmovie
		{
			get { return (string)returnValueField(FldCodmovie); }
			set { insertNameValueField(FldCodmovie, value); }
		}

		/// <summary>Field : "user_id" Tipo: "CE" Formula:  ""</summary>
		public static FieldRef FldCoduserp { get { return m_fldCoduserp; } }
		private static FieldRef m_fldCoduserp = new FieldRef("ratti", "coduserp");

		/// <summary>Field : "user_id" Tipo: "CE" Formula:  ""</summary>
		public string ValCoduserp
		{
			get { return (string)returnValueField(FldCoduserp); }
			set { insertNameValueField(FldCoduserp, value); }
		}

		/// <summary>Field : "Rate" Tipo: "N" Formula:  ""</summary>
		public static FieldRef FldRate { get { return m_fldRate; } }
		private static FieldRef m_fldRate = new FieldRef("ratti", "rate");

		/// <summary>Field : "Rate" Tipo: "N" Formula:  ""</summary>
		public decimal ValRate
		{
			get { return (decimal)returnValueField(FldRate); }
			set { insertNameValueField(FldRate, value); }
		}

		/// <summary>Field : "Rate_at" Tipo: "DT" Formula: DF "[Now]"</summary>
		public static FieldRef FldRatedat { get { return m_fldRatedat; } }
		private static FieldRef m_fldRatedat = new FieldRef("ratti", "ratedat");

		/// <summary>Field : "Rate_at" Tipo: "DT" Formula: DF "[Now]"</summary>
		public DateTime ValRatedat
		{
			get { return (DateTime)returnValueField(FldRatedat); }
			set { insertNameValueField(FldRatedat, value); }
		}

		/// <summary>Field : "ZZSTATE" Type: "INT" Formula:  ""</summary>
		public static FieldRef FldZzstate { get { return m_fldZzstate; } }
		private static FieldRef m_fldZzstate = new FieldRef("ratti", "zzstate");



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
        public static CSGenioAratti search(PersistentSupport sp, string key, User user, string[] fields = null)
        {
			if (string.IsNullOrEmpty(key))
				return null;

		    CSGenioAratti area = new CSGenioAratti(user, user.CurrentModule);

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
        public static List<CSGenioAratti> searchList(PersistentSupport sp, User user, CriteriaSet where, string[] fields = null, bool distinct = false, bool noLock = false)
        {
				return sp.searchListWhere<CSGenioAratti>(where, user, fields, distinct, noLock);
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
        public static void searchListAdvancedWhere(PersistentSupport sp, User user, CriteriaSet where, ListingMVC<CSGenioAratti> listing)
        {
			sp.searchListAdvancedWhere<CSGenioAratti>(where, listing);
        }




		/// <summary>
		/// Check if a record exist
		/// </summary>
		/// <param name="key">Record key</param>
		/// <param name="sp">DB conecntion</param>
		/// <returns>True if the record exist</returns>
		public static bool RecordExist(string key, PersistentSupport sp) => DbArea.RecordExist(key, informacao, sp);








		// USE /[MANUAL MOV TABAUX RATTI]/

 

      

	}
}
