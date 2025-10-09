using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_movadmForm : Form
{
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
	public EnumControl MovieMoviegen => new EnumControl(driver, ContainerLocator, "container-F_MOVADMMOVIEMOVIEGEN");

	/// <summary>
	/// Poster
	/// </summary>
	public BaseInputControl MoviePoster => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEPOSTER__", "#F_MOVADMMOVIEPOSTER__");

	/// <summary>
	/// Discription
	/// </summary>
	public BaseInputControl MovieDiscript => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEDISCRIPT", "#F_MOVADMMOVIEDISCRIPT");

	/// <summary>
	/// 
	/// </summary>
	public BaseInputControl MovieNumberof => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIENUMBEROF", "#F_MOVADMMOVIENUMBEROF");

	public F_movadmForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVADM", containerLocator: containerLocator) { }
}
