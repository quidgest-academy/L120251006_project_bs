using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_moviesForm : Form
{
	/// <summary>
	/// Poster
	/// </summary>
	public BaseInputControl MoviePoster => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEPOSTER__", "#F_MOVIESMOVIEPOSTER__");

	/// <summary>
	/// Title
	/// </summary>
	public BaseInputControl MovieTitle => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIETITLE___", "#F_MOVIESMOVIETITLE___");

	/// <summary>
	/// Realease date
	/// </summary>
	public DateInputControl MovieReldate => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIERELDATE_");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl MovieCreateat => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIECREATEAT");

	/// <summary>
	/// Movies Genre
	/// </summary>
	public EnumControl MovieMoviegen => new EnumControl(driver, ContainerLocator, "container-F_MOVIESMOVIEMOVIEGEN");

	/// <summary>
	/// Discription
	/// </summary>
	public BaseInputControl MovieDiscript => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEDISCRIPT", "#F_MOVIESMOVIEDISCRIPT");

	public F_moviesForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVIES", containerLocator: containerLocator) { }
}
