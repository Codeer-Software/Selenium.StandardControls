using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class LabelDriver : ControlDriverBase
    {
        public LabelDriver(IWebElement element) : base(element) { }

        public string Text => Element.Text;

        public static implicit operator LabelDriver(ElementFinder finder) => new LabelDriver(finder.Find());
    }
}
