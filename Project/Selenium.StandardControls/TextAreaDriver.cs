using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

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
        public static implicit operator TextAreaDriver(ElementFinder finder) => new TextAreaDriver(finder.Find());
    }
}
