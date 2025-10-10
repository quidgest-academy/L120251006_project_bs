using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_rattinForm : PopupForm
{
	/// <summary>
	/// Poster
	/// </summary>
	public BaseInputControl MoviePoster => new BaseInputControl(driver, ContainerLocator, "container-F_RATTINMOVIEPOSTER__", "#F_RATTINMOVIEPOSTER__");

	/// <summary>
	/// Title
	/// </summary>
	public LookupControl MovieTitle => new LookupControl(driver, ContainerLocator, "container-F_RATTINMOVIETITLE___");
	public SeeMorePage MovieTitleSeeMorePage => new SeeMorePage(driver, "F_RATTIN", "F_RATTINMOVIETITLE___");

	/// <summary>
	/// Name
	/// </summary>
	public LookupControl UserpName => new LookupControl(driver, ContainerLocator, "container-F_RATTINUSERPNAME____");
	public SeeMorePage UserpNameSeeMorePage => new SeeMorePage(driver, "F_RATTIN", "F_RATTINUSERPNAME____");

	/// <summary>
	/// Rate
	/// </summary>
	public BaseInputControl RattiRate => new BaseInputControl(driver, ContainerLocator, "container-F_RATTINRATTIRATE____", "#F_RATTINRATTIRATE____");

	/// <summary>
	/// Rate_at
	/// </summary>
	public DateInputControl RattiRatedat => new DateInputControl(driver, ContainerLocator, "#F_RATTINRATTIRATEDAT_", "dd/MM/yyyy HH:mm");

	public F_rattinForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_RATTIN") { }
}
