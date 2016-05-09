using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;
using System.Threading;

namespace Selenium.StandardControls
{
    public class CheckBoxDriver : ControlDriverBase
    {
        public CheckBoxDriver(IWebElement element) : base(element){}
        public CheckBoxDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }
        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);
        public Action Wait { get; set; }

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

        public static implicit operator CheckBoxDriver(ElementFinder finder) => new CheckBoxDriver(finder.Find());
    }
}
