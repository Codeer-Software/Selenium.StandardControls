using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Date Driver
    /// </summary>
    public class DateDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public DateDriver(IWebElement element) : base(element){}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Edit</param>
        public DateDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        
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
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        /// <param name="day">day</param>
        public void Edit(int year, int month, int day)
        {
            //todo multi browser.

            var js = JS;
            Element.Show();
            Element.Focus();
            js.ExecuteScript("arguments[0].select();", Element);
            Element.SendKeys(year.ToString());
            Element.SendKeys(Keys.Right);
            Element.SendKeys(month.ToString());
            Element.SendKeys(Keys.Right);
            Element.SendKeys(day.ToString());

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
        public static implicit operator DateDriver(ElementFinder finder) => finder.Find<DateDriver>();

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('change', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);

                     var sp = this.value.split('-');
                    if (sp.length != 3) return;
                    __codeerTestAssistantPro.pushCode(name + '.Edit(' + sp[0] + ', ' + sp[1] + ', ' + sp[2] + ');');
                    }}, false);
            ";
        }

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo TargetElementInfo => new TargetElementInfo("input", "type", "date");
    }
}
