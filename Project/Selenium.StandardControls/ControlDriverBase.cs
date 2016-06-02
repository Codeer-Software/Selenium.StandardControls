using OpenQA.Selenium;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Based Control Driver
    /// </summary>
    public abstract class ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        protected ControlDriverBase(IWebElement element)
        {
            Element = element;
        }

        /// <summary>
        /// Control driver element
        /// </summary>
        public IWebElement Element { get; }
        /// <summary>
        /// Simple to WebDriver accessor
        /// </summary>
        public IWebDriver WebDriver => Element.GetWebDriver();
        /// <summary>
        /// More information of Element
        /// </summary>
        public ElementInfo Info => new ElementInfo(Element);
        /// <summary>
        /// Simple to IJavaScriptExecutor accessor
        /// </summary>
        public IJavaScriptExecutor JS => Element.GetJS();
        /// <summary>
        /// Parent element
        /// </summary>
        public IWebElement Parent => Element.GetParent();

        /// <summary>
        /// Show Scroll If there is no element in the screen
        /// </summary>
        public void Show() => Element.Show();

        /// <summary>
        /// Remove the focus from Element
        /// </summary>
        public void Blur() => Element.Blur();

        /// <summary>
        /// The focus to Element
        /// </summary>
        public void Focus() => Element.Focus();
    }
}
