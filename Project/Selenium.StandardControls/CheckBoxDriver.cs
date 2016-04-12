using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class CheckBoxDriver : ControlDriverBase
    {
        public CheckBoxDriver(IWebElement element) : base(element) { }

        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);

        public void Edit(bool check)
        {
            if (Checked != check)
            {
                Element.Show();
                Element.Click();
            }
        }

        public static implicit operator CheckBoxDriver(ElementFinder finder) => new CheckBoxDriver(finder.Find());
    }
}
