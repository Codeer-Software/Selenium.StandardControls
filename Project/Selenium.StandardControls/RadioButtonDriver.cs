using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;
using System.Threading;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// RadioButton Driver
    /// </summary>
    public class RadioButtonDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public RadioButtonDriver(IWebElement element) : base(element){}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Edit</param>
        public RadioButtonDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        /// <summary>
        /// State of the check RadioButton
        /// </summary>
        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);
        /// <summary>
        /// Wait for end of the time of Edit
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        ///RadioButton of the check of the on-off
        /// </summary>
        public void Edit()
        {
            Element.Show();
            Element.Focus();
            while (!Checked)
            {
                try
                {
                    Element.ClickEx();
                    if (Checked) break;
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
        public static implicit operator RadioButtonDriver(ElementFinder finder) => new RadioButtonDriver(finder.Find());

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Edit();');
                    }}, false);";
        }
    }
}
