﻿using OpenQA.Selenium;
using Selenium.StandardControls.AdjustBrowser;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
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
        [Obsolete("Please use Click()")]
        public void Invoke() => Click();

        /// <summary>
        /// Click the Anchor
        /// </summary>
        public void Click()
        {
            Element.Show();
            Element.Focus();
            Element.ClickEx();
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator AnchorDriver(ElementFinder finder) => finder.Find<AnchorDriver>();

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Click();');
                    }}, false);";
        }

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo TargetElementInfo => new TargetElementInfo("a");
    }
}
