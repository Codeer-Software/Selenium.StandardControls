using System.Threading;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class CheckBoxDriver : ControlDriverBase
    {
        public CheckBoxDriver(IWebElement element) : base(element) { }

        public bool Checked => (bool)JS.ExecuteScript("return arguments[0].checked;", Element);

        public void Edit(bool check)
        {
            Element.Show();
            while (Checked != check)
            {
                Element.Show();
                Element.Click();
	            if (Checked == check)break;
	            Thread.Sleep(10);
            }
        }

        public static implicit operator CheckBoxDriver(ElementFinder finder) => new CheckBoxDriver(finder.Find());
    }
}
