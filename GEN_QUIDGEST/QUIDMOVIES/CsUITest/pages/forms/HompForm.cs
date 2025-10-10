using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class HompForm : Form
{
	/// <summary>
	///                                 
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#HOMP____PSEUDNEWGRP01-container");

	/// <summary>
	/// Recent movies
	/// </summary>
	public ListControl PseudField001 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD001");

	/// <summary>
	/// 
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp03 => new CollapsibleZoneControl(driver, ContainerLocator, "#HOMP____PSEUDNEWGRP03-container");

	/// <summary>
	/// Movies
	/// </summary>
	public ListControl PseudField002 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD002");

	/// <summary>
	/// Best Rating
	/// </summary>
	public ListControl PseudField003 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD003");

	/// <summary>
	/// New Zone
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp02 => new CollapsibleZoneControl(driver, ContainerLocator, "#HOMP____PSEUDNEWGRP02-container");

	public HompForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "HOMP", containerLocator: containerLocator) { }
}
