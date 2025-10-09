using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_movadmForm : Form
{
	/// <summary>
	/// Content
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVADMPSEUDNEWGRP01-container");

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
	/// Total rate
	/// </summary>
	public BaseInputControl MovieTotalrat => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIETOTALRAT", "#F_MOVADMMOVIETOTALRAT");

	/// <summary>
	/// 
	/// </summary>
	public BaseInputControl MovieSumavg => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIESUMAVG__", "#F_MOVADMMOVIESUMAVG__");

	/// <summary>
	/// feedback zone
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp02 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_MOVADMPSEUDNEWGRP02-container");

	/// <summary>
	/// Numberoflikes
	/// </summary>
	public BaseInputControl MovieNumberof => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIENUMBEROF", "#F_MOVADMMOVIENUMBEROF");

	/// <summary>
	/// LastRate
	/// </summary>
	public BaseInputControl MovieLastrate => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIELASTRATE", "#F_MOVADMMOVIELASTRATE");

	/// <summary>
	/// Average Rate
	/// </summary>
	public BaseInputControl MovieArate => new BaseInputControl(driver, ContainerLocator, "container-F_MOVADMMOVIEARATE___", "#F_MOVADMMOVIEARATE___");

	public F_movadmForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_MOVADM", containerLocator: containerLocator) { }
}
