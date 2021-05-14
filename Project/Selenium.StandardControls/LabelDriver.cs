using OpenQA.Selenium;
using Selenium.StandardControls.AdjustBrowser;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
using System;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Label Driver
    /// </summary>
    public class LabelDriver : ControlDriverBase
    {
        /// <summary>
        /// Wait for end of the time of Invoke
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public LabelDriver(IWebElement element) : base(element) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Invoke</param>
        public LabelDriver(IWebElement element, Action wait = null) : base(element) { Wait = wait; }

        /// <summary>
        /// Label Text
        /// </summary>
        public string Text => Element.Text;

        /// <summary>
        /// Click the label
        /// </summary>
        public void Click()
        {
            //It does not move when you are viewing Show 's only part of the button .
            Element.Show();
            Element.Focus();
            Element.ClickEx();
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator LabelDriver(ElementFinder finder) => finder.Find<LabelDriver>();

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo TargetElement => new TargetElementInfo("label");

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Click();');
                    }}, false);";
        }
    }
}
