using System;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Selenium.StandardControls.PageObjectUtility;
using System.Collections.Generic;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// DropDownList Driver
    /// </summary>
    public class DropDownListDriver : ControlDriverBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public DropDownListDriver(IWebElement element) : base(element){}
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        /// <param name="wait">Wait for end of the time of Edit</param>
        public DropDownListDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }

        /// <summary>
        /// Simple to SelectElement accessor
        /// </summary>
        public SelectElement Core => new SelectElement(Element);
        /// <summary>
        /// The DropDown selection number
        /// </summary>
        public string Text => Items[SelectedIndex];
        /// <summary>
        /// The DropDown selection number
        /// </summary>
        public long SelectedIndex => (long)JS.ExecuteScript("return arguments[0].selectedIndex;", Element);
        /// <summary>
        /// Wait for end of the time of Edit
        /// </summary>
        public Action Wait { get; set; }

        /// <summary>
        /// String of each item in the DropDown
        /// </summary>
        public string[] Items
        {
            get
            {
                dynamic items = null;
                if (WebDriver is InternetExplorerDriver)
                {
                    items = JS.ExecuteScript("var options = arguments[0].options;" +
                        "var array = new Array(options.length);" +
                        "for (var i = 0; i < array.length; i++) array[i] = options[i];" +
                        "return array;", Element);
                }
                else
                {
                    items = JS.ExecuteScript("return arguments[0].options;", Element);
                }
                var l = new List<string>();
                for (int i = 0; i < items.Count; i++)
                {
                    l.Add(items[i].Text);
                }
                return l.ToArray();
            }
        }

        /// <summary>
        /// Select the text of the item in the DropDown
        /// </summary>
        /// <param name="text">Item Text</param>
        public void Edit(string text)
        {
            Element.Show();
            JS.ExecuteScript("return arguments[0].blur();", Element);
            Core.SelectByText(text);
            Wait?.Invoke();
        }


        /// <summary>
        /// Select the number of the item in the DropDown
        /// </summary>
        /// <param name="index">Item Index</param>
        public void Edit(int index)
        {
            Element.Show();
            JS.ExecuteScript("return arguments[0].blur();", Element);
            Core.SelectByIndex(index);
            Wait?.Invoke();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="finder">A variety of find to the elements</param>
        public static implicit operator DropDownListDriver(ElementFinder finder) => finder.Find<DropDownListDriver>();

        [CaptureCodeGenerator]
        public string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('change', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Edit(""' + this.value + '"");');
                    }}, false);
                    ";
        }
    }
}
