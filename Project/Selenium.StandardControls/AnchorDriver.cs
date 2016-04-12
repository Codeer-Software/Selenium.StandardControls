using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class AnchorDriver : ControlDriverBase
    {
        public AnchorDriver(IWebElement element) : base(element) { }

        public string Text => Element.Text;

        public void Click()
        {
            Element.Show();
            Element.Click();
        }

        public static implicit operator AnchorDriver(ElementFinder finder) => new AnchorDriver(finder.Find());
    }
}
