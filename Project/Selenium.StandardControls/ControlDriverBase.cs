using OpenQA.Selenium;

namespace Selenium.StandardControls
{
    public abstract class ControlDriverBase
    {
        public IWebElement Element { get; private set; }
        public IWebDriver WebDriver => Element.GetWebDriver();
        public ElementInfo Info => new ElementInfo(Element);
        public IJavaScriptExecutor JS => Element.GetJS();
        public IWebElement Parent => Element.GetParent();

        protected ControlDriverBase(IWebElement element)
        {
            Element = element;
        }
    }
}
