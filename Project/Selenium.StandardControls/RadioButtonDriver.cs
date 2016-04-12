using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class RadioButtonDriver : ControlDriverBase
    {
        public RadioButtonDriver(IWebElement element) : base(element) { }

        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);

        public void Edit()
        {
            Element.Show();
            Element.Click();
        }

        public static implicit operator RadioButtonDriver(ElementFinder finder) => new RadioButtonDriver(finder.Find());
    }
}
