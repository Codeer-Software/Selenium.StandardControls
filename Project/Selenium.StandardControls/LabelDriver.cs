using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Label Driver
    /// </summary>
    public class LabelDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public LabelDriver(IWebElement element) : base(element) { }

        /// <summary>
        /// Label Text
        /// </summary>
        public string Text => Element.Text;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator LabelDriver(ElementFinder finder) => new LabelDriver(finder.Find());
    }
}
