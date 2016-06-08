using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    /// <summary>
    /// TextBox Driver
    /// </summary>
    public class TextBoxDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public TextBoxDriver(IWebElement element) : base(element){}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Edit</param>
        public TextBoxDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        /// <summary>
        /// Wait for end of the time of Edit
        /// </summary>
        public Action Wait { get; set; }
        /// <summary>
        ///  TextBox text
        /// </summary>
        public string Text => Info.Value;

        /// <summary>
        /// To edit the text in the TextBox
        /// </summary>
        /// <param name="text"></param>
        public void Edit(string text)
        {
            var js = JS;
            Element.Show();
            Element.Focus();
            js.ExecuteScript("arguments[0].select();", Element);
            Element.SendKeys(Keys.Delete);
            Element.SendKeys(text);
            try
            {
                js.ExecuteScript("");//sync.
            }
            catch { }
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator TextBoxDriver(ElementFinder finder) => new TextBoxDriver(finder.Find());
    }
}
