using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Button Driver
    /// </summary>
    public class ButtonDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public ButtonDriver(IWebElement element) : base(element){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Invoke</param>
        public ButtonDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        /// <summary>
        /// Button text
        /// </summary>
        public string Text => Info.Value;
        /// <summary>
        /// Wait for end of the time of Invoke
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        /// Press the button
        /// </summary>
        public void Invoke()
        {
            //It does not move when you are viewing Show 's only part of the button .
            Element.ScrollIntoView(true);
            Element.ClickEx();
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator ButtonDriver(ElementFinder finder) => finder.Find<ButtonDriver>();

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Invoke();');
                    }}, false);";
        }

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo[] TargetElementInfo => new[] 
        {
            new TargetElementInfo("button"),
            new TargetElementInfo("input", "type", "submit"),
            new TargetElementInfo("input", "type", "button"),
        };
    }
}
