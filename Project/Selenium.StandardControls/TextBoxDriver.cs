using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class TextBoxDriver : ControlDriverBase
    {
        public TextBoxDriver(IWebElement element) : base(element) { }

        public string Text => Info.Value;

        public void Edit(string text)
        {
            Element.Clear();
            Element.SendKeys(text);
        }

        public static implicit operator TextBoxDriver(ElementFinder finder) => new TextBoxDriver(finder.Find());
    }
}
