using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class ButtonDriver : ControlDriverBase
    {
        public ButtonDriver(IWebElement element) : base(element) { }

        public string Text => Info.Value;

        public void Click()
        {
            Element.Show();
            Element.Click();
        }

        public static implicit operator ButtonDriver(ElementFinder finder)=> new ButtonDriver(finder.Find());
    }
}
