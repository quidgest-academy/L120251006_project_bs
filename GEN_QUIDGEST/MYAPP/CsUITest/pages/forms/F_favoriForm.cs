using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_favoriForm : Form
{
	/// <summary>
	/// Name
	/// </summary>
	public LookupControl UserpName => new LookupControl(driver, ContainerLocator, "container-F_FAVORIUSERPNAME____");
	public SeeMorePage UserpNameSeeMorePage => new SeeMorePage(driver, "F_FAVORI", "F_FAVORIUSERPNAME____");

	/// <summary>
	/// Favorite at
	/// </summary>
	public DateInputControl FavorFavdate => new DateInputControl(driver, ContainerLocator, "#F_FAVORIFAVORFAVDATE_");

	/// <summary>
	/// Title
	/// </summary>
	public LookupControl MovieTitle => new LookupControl(driver, ContainerLocator, "container-F_FAVORIMOVIETITLE___");
	public SeeMorePage MovieTitleSeeMorePage => new SeeMorePage(driver, "F_FAVORI", "F_FAVORIMOVIETITLE___");

	public F_favoriForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_FAVORI", containerLocator: containerLocator) { }
}
