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
	/// Movies Genre
	/// </summary>
	public RadiobuttonControl MovieMoviegen => new RadiobuttonControl(driver, ContainerLocator, "container-F_MOVIESMOVIEMOVIEGEN");

	/// <summary>
	/// Title
	/// </summary>
	public BaseInputControl MovieTitle => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIETITLE___", "#F_MOVIESMOVIETITLE___");

	/// <summary>
	/// Realease date
	/// </summary>
	public DateInputControl MovieReldate => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIERELDATE_");

	/// <summary>
	/// Discription
	/// </summary>
	public BaseInputControl MovieDiscript => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEDISCRIPT", "#F_MOVIESMOVIEDISCRIPT");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl MovieCreateat => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIECREATEAT");

	/// <summary>
	/// Comments
	/// </summary>
	public ListControl PseudDatacome => new ListControl(driver, ContainerLocator, "#F_MOVIESPSEUDDATACOME");

	/// <summary>
	/// Num of likes
	/// </summary>
	public BaseInputControl MovieNumberof => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIENUMBEROF", "#F_MOVIESMOVIENUMBEROF");

	/// <summary>
	/// Like
	/// </summary>
	public ButtonControl PseudField001 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD001");

	/// <summary>
	/// Comment
	/// </summary>
	public ButtonControl PseudField002 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD002");

	/// <summary>
	/// Rate
	/// </summary>
	public ButtonControl PseudField003 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD003");

	/// <summary>
	/// Characters
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVIESPSEUDNEWGRP01-container");

	/// <summary>
	/// Characters
	/// </summary>
	public ListControl PseudDatachar => new ListControl(driver, ContainerLocator, "#F_MOVIESPSEUDDATACHAR");

	/// <summary>
	/// Backdrop
	/// </summary>
	public BaseInputControl MovieBackdrop => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEBACKDROP", "#F_MOVIESMOVIEBACKDROP");

	public F_moviesForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVIES", containerLocator: containerLocator) { }
}
