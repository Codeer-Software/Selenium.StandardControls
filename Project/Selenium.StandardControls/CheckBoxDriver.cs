using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;
using System.Threading;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

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
        /// <param name="wait">Wait for end of the time of Edit</param>
        public CheckBoxDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        /// <summary>
        /// State of the check check box
        /// </summary>
        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);
        /// <summary>
        /// Wait for end of the time of Edit
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

        //@@@
        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('change', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Clear();');
                      __codeerTestAssistantPro.pushCode(name + '.SendKeys(""' + this.value + '"");');
                    }}, false);
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Click();');
                    }}, false);";
        }
    }
}
