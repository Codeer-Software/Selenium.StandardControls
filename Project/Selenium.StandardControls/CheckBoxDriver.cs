using System;
using System.Threading;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

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
            while (Checked != check)
            {
                Element.Show();
                Element.SendKeys(Keys.Space);
                if (Checked == check)break;
	            Thread.Sleep(10);
            }
            Wait?.Invoke();
        }

        public static implicit operator CheckBoxDriver(ElementFinder finder) => new CheckBoxDriver(finder.Find());
    }
}
