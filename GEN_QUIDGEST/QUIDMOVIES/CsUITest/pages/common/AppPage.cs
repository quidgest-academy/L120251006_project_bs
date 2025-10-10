using System.Linq;

namespace quidgest.uitests.pages;

public class AppPage: PageObject
{
	private By containerLocator => By.ClassName("layout-container");
	private IWebElement Container => driver.FindElement(containerLocator);

	public IMenuControl Menu => new HorizontalMenuControl(driver, _menuTree);

	private By loginBtnLocator => By.Id("logon-menu-btn");
	private IWebElement loginBtn => driver.FindElement(loginBtnLocator);
	private By avatarLocator => By.Id("user-avatar");
	
	/// <summary>
	/// Right sidebar
	/// </summary>
	public SidebarPage Sidebar => new SidebarPage(driver, containerLocator);

	public AppPage(IWebDriver driver) : base(driver)
	{
		string url = Configuration.Instance.BaseUrl;
		driver.Navigate().GoToUrl(url);

		wait.Until(c => Container);
	}

	private void WaitForLoading()
	{
		wait.Until(c => Container.GetAttribute("data-loading") != "true");
	}

	public void ClickLogin()
	{
		WaitForLoading();

		// It seems there are cases when the login button takes longer to render than the server responses to arrive.
		wait.Until(c => loginBtn);

		loginBtn.Click();
	}

	public bool IsAuthenticated()
	{
		WaitForLoading();

		if (Container.FindElements(avatarLocator).Any())
			return true;

		return false;
	}

    public bool ValidateMenuNavigation(string moduleId, string itemId)
    {
        try
        {
            return wait.Until(driver => driver.Url.Contains($"{moduleId}/menu/{moduleId}_{itemId}"));
        }
        catch { return false; }
    }

	//Header
		//logo
		//avatar
	//Menu
	//MainContent
		//breadcrumbs
		//sidebar
	//Footer
		//version

	private readonly static MenuTree _menuTree = DeclareMenuTree();

    private static MenuTree DeclareMenuTree()
    {
        MenuTree res = new MenuTree();
		string module;

		module = "MOV";
		res.AddModule(module);
		res.AddMenu(module, "1", null);
		res.AddMenu(module, "2", null);
		res.AddMenu(module, "3", null);
		res.AddMenu(module, "4", null);
		res.AddMenu(module, "41", "4");
		res.AddMenu(module, "42", "4");
		res.AddMenu(module, "5", null);
		res.AddMenu(module, "51", "5");
		res.AddMenu(module, "52", "5");
		res.AddMenu(module, "53", "5");
		res.AddMenu(module, "54", "5");
		res.AddMenu(module, "55", "5");
        return res;
    }
}
