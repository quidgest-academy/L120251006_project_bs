using System.Collections.Generic;

namespace quidgest.uitests.pages;

public class TableColumnDropdown : PageObject
{
    /// <summary>
    /// Dropdown container element
    /// </summary>
    private IWebElement dropdownContainer => driver.FindElement(By.Id(id));

    private string tableId;

    private string columnName;

    private string id => tableId + "_" + columnName + "_column_drop";
    private string valueControlId => tableId + "_h_0_0_" + columnName;

    private IWebElement sortAscendingButton => GetElement(dropdownContainer, By.CssSelector("[data-control-type='sort-asc']"));

    private IWebElement sortDescendingButton => GetElement(dropdownContainer, By.CssSelector("[data-control-type='sort-desc']"));

    private string searchType => GetElement(dropdownContainer, By.CssSelector("[data-search-type]"))?.GetAttribute("data-search-type");
    public ButtonControl SortAscending => sortAscendingButton == null ? null : new ButtonControl(driver, By.Id(id), "[data-control-type='sort-asc']");

    public ButtonControl SortDescending => sortDescendingButton == null ? null : new ButtonControl(driver, By.Id(id), "[data-control-type='sort-desc']");

    public EnumControl Operation => new EnumControl(driver, By.Id(id), By.CssSelector("[data-control-type='operator']"));

    public InputControl Value
    {
        get
        {
            switch (searchType)
            {
                case "date":
                    return new DateInputControl(driver, By.Id(id), "#" + valueControlId);
                case "text":
                case "num":
                default:
                    return new BaseInputControl(driver, By.Id(id), "container-" + valueControlId, "#" + valueControlId);
            }
        }
    }

    public ButtonControl Save => new ButtonControl(driver, By.Id(id), "[data-control-type='save']");

    public ButtonControl Remove => new ButtonControl(driver, By.Id(id), "[data-control-type='remove']");

    public TableColumnDropdown(IWebDriver driver, string tableId, string columnName) : base(driver)
    {
        this.tableId = tableId;
        this.columnName = columnName;

        wait.Until(c => dropdownContainer != null);
    }
}