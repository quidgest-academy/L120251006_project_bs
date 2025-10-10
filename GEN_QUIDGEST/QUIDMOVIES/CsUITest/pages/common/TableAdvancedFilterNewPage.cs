namespace quidgest.uitests.pages;

public class TableAdvancedFilterNewPage : PageObject
{
    private string tableId;

    private string id => "q-modal-advanced-filters-new";

    private string valueControlId => tableId + "_filters_0_0_0";

    /// <summary>
    /// Dropdown container element
    /// </summary>
    private IWebElement popupContainer => driver.FindElement(By.Id(id));

    private string searchType => GetElement(popupContainer, By.CssSelector("[data-search-type]"))?.GetAttribute("data-search-type");

    public EnumControl Field => new EnumControl(driver, By.Id(id), By.CssSelector("[data-control-type='field']"));

    public EnumControl Operation => new EnumControl(driver, By.Id(id), By.CssSelector("[data-control-type='operator']"));

    public InputControl Value
    {
        get
        {
            switch (searchType)
            {
                case "date":
                    return new DateInputControl(driver, By.Id(id), "[data-control-type='value']");
                case "text":
                case "num":
                default:
                    return new BaseInputControl(driver, By.Id(id), "container-" + valueControlId, "#" + valueControlId);
            }
        }
    }

    public ButtonControl Save => new ButtonControl(driver, By.Id(id), "[data-control-type='save']");

    public ButtonControl Cancel => new ButtonControl(driver, By.Id(id), "[data-control-type='cancel']");

    public TableAdvancedFilterNewPage(IWebDriver driver, string tableId) : base(driver)
    {
        this.tableId = tableId;

        wait.Until(c => popupContainer != null);
    }
}
