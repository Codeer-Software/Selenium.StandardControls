using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class TextAreaDriver : TextBoxDriver
    {
        public TextAreaDriver(IWebElement element) : base(element) { }

        public static implicit operator TextAreaDriver(ElementFinder finder) => new TextAreaDriver(finder.Find());
    }
}
