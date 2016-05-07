using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class RadioButtonDriver : ControlDriverBase
    {
        public RadioButtonDriver(IWebElement element) : base(element){}
        public RadioButtonDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);
        public Action Wait { get; set; }

        public void Edit()
        {
            Element.Show();
            bool check = !Checked;
            while (Checked != check)
            {
                Element.Show();
                Element.Click();
                if (Checked == check)break;
                Thread.Sleep(10);
            }
            Wait?.Invoke();
        }

        public static implicit operator RadioButtonDriver(ElementFinder finder) => new RadioButtonDriver(finder.Find());
    }
}
