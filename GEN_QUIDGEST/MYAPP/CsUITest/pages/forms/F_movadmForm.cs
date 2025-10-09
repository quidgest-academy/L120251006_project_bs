using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_movadmForm : Form
{
	/// <summary>
	/// Poster
	/// </summary>
	public BaseInputControl MoviePoster => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEPOSTER__", "#F_MOVADMMOVIEPOSTER__");

	/// <summary>
	/// Backdrop
	/// </summary>
	public BaseInputControl MovieBackdrop => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEBACKDROP", "#F_MOVADMMOVIEBACKDROP");

	/// <summary>
	/// Title
	/// </summary>
	public BaseInputControl MovieTitle => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIETITLE___", "#F_MOVADMMOVIETITLE___");

	/// <summary>
	/// Realease date
	/// </summary>
	public DateInputControl MovieReldate => new DateInputControl(driver, ContainerLocator, "#F_MOVADMMOVIERELDATE_");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl MovieCreateat => new DateInputControl(driver, ContainerLocator, "#F_MOVADMMOVIECREATEAT");

	/// <summary>
	/// Movies Genre
	/// </summary>
	public RadiobuttonControl MovieMoviegen => new RadiobuttonControl(driver, ContainerLocator, "container-F_MOVADMMOVIEMOVIEGEN");

	/// <summary>
	/// Discription
	/// </summary>
	public BaseInputControl MovieDiscript => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEDISCRIPT", "#F_MOVADMMOVIEDISCRIPT");

	/// <summary>
	/// Numberoflikes
	/// </summary>
	public BaseInputControl MovieNumberof => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIENUMBEROF", "#F_MOVADMMOVIENUMBEROF");

	public F_movadmForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVADM", containerLocator: containerLocator) { }
}
