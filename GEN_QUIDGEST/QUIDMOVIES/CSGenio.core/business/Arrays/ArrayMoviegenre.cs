using System.Collections.Generic;

namespace CSGenio.business
{
	/// <summary>
	/// Array MOVIEGENRE (Movies Genre)
	/// </summary>
	public class ArrayMoviegenre : Array<string>
	{
		/// <summary>
		/// The instance
		/// </summary>
		private static readonly ArrayMoviegenre _instance = new ArrayMoviegenre();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static ArrayMoviegenre Instance { get => _instance; }

		/// <summary>
		/// Array code type
		/// </summary>
		public static ArrayType Type { get { return ArrayType.STRING; } }

		/// <summary>
		/// Drama
		/// </summary>
		public const string E_DRAMA_1 = "Drama";
		/// <summary>
		/// Comedy
		/// </summary>
		public const string E_COMEDY_2 = "Comedy";
		/// <summary>
		/// Thriller
		/// </summary>
		public const string E_THRILLER_3 = "Thriller";
		/// <summary>
		/// Action
		/// </summary>
		public const string E_ACTION_4 = "Action";
		/// <summary>
		/// Romance
		/// </summary>
		public const string E_ROMANCE_5 = "Romance";

		/// <summary>
		/// Prevents a default instance of the <see cref="ArrayMoviegenre"/> class from being created.
		/// </summary>
		private ArrayMoviegenre() : base() {}

		/// <summary>
        /// Loads the dictionary.
        /// </summary>
        /// <returns></returns>
		protected override Dictionary<string, ArrayElement> LoadDictionary()
		{
			return new Dictionary<string, ArrayElement>()
			{
				{ E_DRAMA_1, new ArrayElement() { ResourceId = "DRAMA24263", HelpId = "", Group = "" } },
				{ E_COMEDY_2, new ArrayElement() { ResourceId = "COMEDY64227", HelpId = "", Group = "" } },
				{ E_THRILLER_3, new ArrayElement() { ResourceId = "THRILLER39900", HelpId = "", Group = "" } },
				{ E_ACTION_4, new ArrayElement() { ResourceId = "ACTION41832", HelpId = "", Group = "" } },
				{ E_ROMANCE_5, new ArrayElement() { ResourceId = "ROMANCE40818", HelpId = "", Group = "" } },
			};
		}

		/// <summary>
		/// Gets the element's description.
		/// </summary>
		/// <param name="cod">The cod.</param>
		/// <returns></returns>
		public static string CodToDescricao(string cod)
		{
			return Instance.CodToDescricaoImpl(cod);
		}

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <returns></returns>
		public static List<string> GetElements()
		{
			return Instance.GetElementsImpl();
		}

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <param name="cod">The cod.</param>
		/// <returns></returns>
		public static ArrayElement GetElement(string cod)
		{
            return Instance.GetElementImpl(cod);
        }

		/// <summary>
		/// Gets the dictionary.
		/// </summary>
		/// <returns></returns>
		public static IDictionary<string, string> GetDictionary()
		{
			return Instance.GetDictionaryImpl();
		}

		/// <summary>
		/// Gets the help identifier.
		/// </summary>
		/// <param name="cod">The cod.</param>
		/// <returns></returns>
		public static string GetHelpId(string cod)
		{
			return Instance.GetHelpIdImpl(cod);
		}
	}
}
