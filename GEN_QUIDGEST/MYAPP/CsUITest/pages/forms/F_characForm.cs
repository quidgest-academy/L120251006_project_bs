using quidgest.uitests.pages.forms.core;

#nullable enable

namespace quidgest.uitests.pages.forms;

[System.CodeDom.Compiler.GeneratedCode("Genio", "")]
public class F_characForm : Form
{
	/// <summary>
	/// Photo
	/// </summary>
	public BaseInputControl CharaPhoto => new BaseInputControl(driver, ContainerLocator, "container-F_CHARACCHARAPHOTO___", "#F_CHARACCHARAPHOTO___");

	/// <summary>
	/// Name
	/// </summary>
	public BaseInputControl CharaName => new BaseInputControl(driver, ContainerLocator, "container-F_CHARACCHARANAME____", "#F_CHARACCHARANAME____");

	/// <summary>
	/// Actor name
	/// </summary>
	public BaseInputControl CharaActornam => new BaseInputControl(driver, ContainerLocator, "container-F_CHARACCHARAACTORNAM", "#F_CHARACCHARAACTORNAM");

	/// <summary>
	/// Create at
	/// </summary>
	public DateInputControl CharaCreateat => new DateInputControl(driver, ContainerLocator, "#F_CHARACCHARACREATEAT");

	/// <summary>
	/// Genre
	/// </summary>
	public RadiobuttonControl CharaGenre => new RadiobuttonControl(driver, ContainerLocator, "container-F_CHARACCHARAGENRE___");

	/// <summary>
	/// Title
	/// </summary>
	public LookupControl MovieTitle => new LookupControl(driver, ContainerLocator, "container-F_CHARACMOVIETITLE___");
	public SeeMorePage MovieTitleSeeMorePage => new SeeMorePage(driver, "F_CHARAC", "F_CHARACMOVIETITLE___");

	public F_characForm(IWebDriver driver, FORM_MODE mode, By? containerLocator = null)
		: base(driver, mode, "F_CHARAC", containerLocator: containerLocator) { }
}
