using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_userForm : Form
{
	/// <summary>
	/// Name
	/// </summary>
	public BaseInputControl UserpName => new BaseInputControl(driver, ContainerLocator, "container-F_USER__USERPNAME____", "#F_USER__USERPNAME____");

	/// <summary>
	/// Email
	/// </summary>
	public BaseInputControl UserpEmail => new BaseInputControl(driver, ContainerLocator, "container-F_USER__USERPEMAIL___", "#F_USER__USERPEMAIL___");

	/// <summary>
	/// Photo
	/// </summary>
	public BaseInputControl UserpPhoto => new BaseInputControl(driver, ContainerLocator, "container-F_USER__USERPPHOTO___", "#F_USER__USERPPHOTO___");

	public F_userForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_USER", containerLocator: containerLocator) { }
}
