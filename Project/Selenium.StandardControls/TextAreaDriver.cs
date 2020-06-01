using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// TextArea Driver
    /// </summary>
    public class TextAreaDriver : TextBoxDriver
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public TextAreaDriver(IWebElement element) : base(element) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator TextAreaDriver(ElementFinder finder) => finder.Find<TextAreaDriver>();
    }
}
