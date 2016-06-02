using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;
using System.Threading;

namespace Selenium.StandardControls
{
    /// <summary>
    /// CheckBox Driver
    /// </summary>
    public class CheckBoxDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public CheckBoxDriver(IWebElement element) : base(element){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Invoke</param>
        public CheckBoxDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        /// <summary>
        /// State of the check check box
        /// </summary>
        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);
        /// <summary>
        /// Wait for end of the time of Invoke
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        /// Check box of the check of the on-off
        /// </summary>
        /// <param name="check">true:checked false:unchecked</param>
        public void Edit(bool check)
        {
            Element.Show();
            Element.Focus();
            while (Checked != check)
            {
                try
                {
                    Element.ClickEx();
                    if (Checked == check) break;
                    Thread.Sleep(100);
                }
                catch
                {
                    break;
                }
            }
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator CheckBoxDriver(ElementFinder finder) => new CheckBoxDriver(finder.Find());
    }
}
