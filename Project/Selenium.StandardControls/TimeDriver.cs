using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Time Driver
    /// </summary>
    public class TimeDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public TimeDriver(IWebElement element) : base(element){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Edit</param>
        public TimeDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        
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
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        public void Edit(int hour, int minute)
        {
            //todo multi browser.

            var js = JS;
            Element.Show();
            Element.Focus();
            js.ExecuteScript("arguments[0].select();", Element);
            Element.SendKeys(hour.ToString());
            Element.SendKeys(Keys.Right);
            Element.SendKeys(minute.ToString());

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
        public static implicit operator TimeDriver(ElementFinder finder) => finder.Find<TimeDriver>();

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('change', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);

                     var sp = this.value.split(':');
                    if (sp.length != 2) return;
                    __codeerTestAssistantPro.pushCode(name + '.Edit(' + sp[0] + ', ' + sp[1] + ');');
                    }}, false);
            ";
        }

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo TargetElementInfo => new TargetElementInfo("input", "type", "time");
    }
}
