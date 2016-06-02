using OpenQA.Selenium;
using Selenium.StandardControls.AdjustBrowser;
using Selenium.StandardControls.PageObjectUtility;
using System;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Anchor Driver
    /// </summary>
    public class AnchorDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public AnchorDriver(IWebElement element) : base(element) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Invoke</param>
        public AnchorDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        /// <summary>
        /// Anchor Text
        /// </summary>
        public string Text => Element.Text;
        /// <summary>
        /// Wait for end of the time of Invoke
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        /// Click the Anchor
        /// </summary>
        public void Invoke()
        {
            Element.ClickEx();
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator AnchorDriver(ElementFinder finder) => new AnchorDriver(finder.Find());
    }
}
