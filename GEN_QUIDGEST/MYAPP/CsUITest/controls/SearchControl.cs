using System.Collections.Generic;

namespace quidgest.uitests.controls;

public class SearchControl : ControlObject
{
    private IWebElement input => m_control.FindElement(By.CssSelector("input[role='searchbox']"));
    private IWebElement clearBtn => m_control.FindElement(By.CssSelector(".q-table-search__field button"));
    private IList<IWebElement> searchFields => m_control.FindElements(By.CssSelector("[id*='-srch-flds'] .dropdown-item"));

    public SearchControl(IWebDriver driver, By containerLocator, By controlLocator)
		: base(driver, containerLocator, controlLocator)
    {
    }

    public void Search(string text, bool allFields = false)
    {
        input.SendKeys(text);

        if (!allFields)
            input.SendKeys(Keys.Return);
        else
        {
            if (searchFields.Count > 0)
            {
                int size = searchFields.Count;
                searchFields[size-1].Click();
            }
        }
    }

    public void Search(string text, int fieldIndex)
    {
        input.SendKeys(text);

        if (fieldIndex >= searchFields.Count)
            throw new ArgumentException($"Invalid field index: {searchFields}");

        searchFields[fieldIndex].Click();
    }

    public void Search(string text, string fieldName)
    {
        input.SendKeys(text);
        int index = searchFields.FindIndex(o => o.GetAttribute("data-search-field") == fieldName);

        if (index >= 0)
            searchFields[index].Click();
        else
            throw new ArgumentException($"Invalid field: {fieldName}");
    }

    public void Clear()
    {
        clearBtn.Click();
    }

}
