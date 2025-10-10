using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_commenForm : Form
{
	/// <summary>
	/// Details
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_COMMENPSEUDNEWGRP01-container");

	/// <summary>
	/// Name
	/// </summary>
	public LookupControl UserpName => new LookupControl(driver, ContainerLocator, "container-F_COMMENUSERPNAME____");
	public SeeMorePage UserpNameSeeMorePage => new SeeMorePage(driver, "F_COMMEN", "F_COMMENUSERPNAME____");

	/// <summary>
	/// Title
	/// </summary>
	public LookupControl MovieTitle => new LookupControl(driver, ContainerLocator, "container-F_COMMENMOVIETITLE___");
	public SeeMorePage MovieTitleSeeMorePage => new SeeMorePage(driver, "F_COMMEN", "F_COMMENMOVIETITLE___");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl CommeCreateat => new DateInputControl(driver, ContainerLocator, "#F_COMMENCOMMECREATEAT");

	/// <summary>
	/// Post
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp02 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_COMMENPSEUDNEWGRP02-container");

	/// <summary>
	/// Post
	/// </summary>
	public BaseInputControl CommePost => new BaseInputControl(driver, ContainerLocator, "container-F_COMMENCOMMEPOST____", "#F_COMMENCOMMEPOST____");

	public F_commenForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_COMMEN", containerLocator: containerLocator) { }
}
