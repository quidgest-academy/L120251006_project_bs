using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_moviesForm : Form
{
	/// <summary>
	/// Details
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp02 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVIESPSEUDNEWGRP02-container");

	/// <summary>
	/// Poster
	/// </summary>
	public BaseInputControl MoviePoster => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEPOSTER__", "#F_MOVIESMOVIEPOSTER__");

	/// <summary>
	/// Title
	/// </summary>
	public BaseInputControl MovieTitle => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIETITLE___", "#F_MOVIESMOVIETITLE___");

	/// <summary>
	/// Discription
	/// </summary>
	public BaseInputControl MovieDiscript => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEDISCRIPT", "#F_MOVIESMOVIEDISCRIPT");

	/// <summary>
	/// Movies Genre
	/// </summary>
	public RadiobuttonControl MovieMoviegen => new RadiobuttonControl(driver, ContainerLocator, "container-F_MOVIESMOVIEMOVIEGEN");

	/// <summary>
	/// Realease date
	/// </summary>
	public DateInputControl MovieReldate => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIERELDATE_");

	/// <summary>
	/// FeedBack
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp03 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVIESPSEUDNEWGRP03-container");

	/// <summary>
	/// Num Likes
	/// </summary>
	public BaseInputControl MovieNumberof => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIENUMBEROF", "#F_MOVIESMOVIENUMBEROF");

	/// <summary>
	/// Like
	/// </summary>
	public ButtonControl PseudField001 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD001");

	/// <summary>
	/// Average Rate
	/// </summary>
	public BaseInputControl MovieArate => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEARATE___", "#F_MOVIESMOVIEARATE___");

	/// <summary>
	/// Rate
	/// </summary>
	public ButtonControl PseudField003 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD003");

	/// <summary>
	/// LastRate
	/// </summary>
	public BaseInputControl MovieLastrate => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIELASTRATE", "#F_MOVIESMOVIELASTRATE");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl MovieCreateat => new DateInputControl(driver, ContainerLocator, "#F_MOVIESMOVIECREATEAT");

	/// <summary>
	/// Backdrop
	/// </summary>
	public BaseInputControl MovieBackdrop => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIEBACKDROP", "#F_MOVIESMOVIEBACKDROP");

	/// <summary>
	/// Total rate
	/// </summary>
	public BaseInputControl MovieTotalrat => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIETOTALRAT", "#F_MOVIESMOVIETOTALRAT");

	/// <summary>
	/// 
	/// </summary>
	public BaseInputControl MovieSumavg => new BaseInputControl(driver, ContainerLocator, "container-F_MOVIESMOVIESUMAVG__", "#F_MOVIESMOVIESUMAVG__");

	/// <summary>
	/// Comments
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp04 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVIESPSEUDNEWGRP04-container");

	/// <summary>
	/// Comments
	/// </summary>
	public ListControl PseudDatacome => new ListControl(driver, ContainerLocator, "#F_MOVIESPSEUDDATACOME");

	/// <summary>
	/// Comment
	/// </summary>
	public ButtonControl PseudField002 => new ButtonControl(driver, ContainerLocator, "#F_MOVIESPSEUDFIELD002");

	/// <summary>
	/// Characters
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVIESPSEUDNEWGRP01-container");

	/// <summary>
	/// Characters
	/// </summary>
	public ListControl PseudDatachar => new ListControl(driver, ContainerLocator, "#F_MOVIESPSEUDDATACHAR");

	public F_moviesForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVIES", containerLocator: containerLocator) { }
}
