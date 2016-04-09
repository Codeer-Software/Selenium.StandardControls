using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using Selenium.StandardControls.PageObjectUtility;
using System.Collections.Generic;

namespace Selenium.StandardControls
{
    public class DropDownListDriver : ControlDriverBase
    {
        public DropDownListDriver(IWebElement element) : base(element) { }

        public SelectElement Core => new SelectElement(Element);
        public string Text => Items[SelectedIndex];
        public long SelectedIndex => (long)JS.ExecuteScript("return arguments[0].selectedIndex;", Element);
        
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

        public void Edit(string text)
        {
            JS.ExecuteScript("return arguments[0].blur();", Element);
            Core.SelectByText(text);
        }

        public void Edit(int index)
        {
            JS.ExecuteScript("return arguments[0].blur();", Element);
            Core.SelectByIndex(index);
        }

        public static implicit operator DropDownListDriver(ElementFinder finder) => new DropDownListDriver(finder.Find());
    }
}
