using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class HompForm : Form
{
	/// <summary>
	/// Recent movies
	/// </summary>
	public ListControl PseudField001 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD001");

	/// <summary>
	/// Movies
	/// </summary>
	public ListControl PseudField002 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD002");

	/// <summary>
	/// Best Rating
	/// </summary>
	public ListControl PseudField003 => new ListControl(driver, ContainerLocator, "#HOMP____PSEUDFIELD003");

	public HompForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "HOMP", containerLocator: containerLocator) { }
}
