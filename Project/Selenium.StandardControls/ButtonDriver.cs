using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.AdjustBrowser;

namespace Selenium.StandardControls
{
    public class ButtonDriver : ControlDriverBase
    {
        public ButtonDriver(IWebElement element) : base(element){}
        public ButtonDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        public string Text => Info.Value;
        public Action Wait { get; set; }

        public void Invoke()
        {
            //It does not move when you are viewing Show 's only part of the button .
            Element.ScrollIntoView(true);
            Element.ClickEx();
            Wait?.Invoke();
        }

        public static implicit operator ButtonDriver(ElementFinder finder)=> new ButtonDriver(finder.Find());
    }
}
