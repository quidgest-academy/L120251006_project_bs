using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_profilForm : Form
{
	/// <summary>
	/// Details
	/// </summary>
	public CollapsibleZoneControl PseudNewgrp01 => new CollapsibleZoneControl(driver, ContainerLocator, "#F_PROFILPSEUDNEWGRP01-container");

	/// <summary>
	/// Photo
	/// </summary>
	public BaseInputControl UserpPhoto => new BaseInputControl(driver, ContainerLocator, "container-F_PROFILUSERPPHOTO___", "#F_PROFILUSERPPHOTO___");

	/// <summary>
	/// Name
	/// </summary>
	public BaseInputControl UserpName => new BaseInputControl(driver, ContainerLocator, "container-F_PROFILUSERPNAME____", "#F_PROFILUSERPNAME____");

	/// <summary>
	/// Email
	/// </summary>
	public BaseInputControl UserpEmail => new BaseInputControl(driver, ContainerLocator, "container-F_PROFILUSERPEMAIL___", "#F_PROFILUSERPEMAIL___");

	public F_profilForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_PROFIL", containerLocator: containerLocator) { }
}
