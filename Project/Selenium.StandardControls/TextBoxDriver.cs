using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;

namespace Selenium.StandardControls
{
    public class TextBoxDriver : ControlDriverBase
    {
        public TextBoxDriver(IWebElement element) : base(element){}
        public TextBoxDriver(IWebElement element, Action wait) : base(element){ Wait = wait; }
        public Action Wait { get; set; }
        public string Text => Info.Value;

        public void Edit(string text)
        {
            Element.Show();
            Element.Clear();
            Element.SendKeys(text);
            Wait?.Invoke();
        }

        public static implicit operator TextBoxDriver(ElementFinder finder) => new TextBoxDriver(finder.Find());
    }
}
