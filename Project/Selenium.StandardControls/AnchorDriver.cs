using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using System;

namespace Selenium.StandardControls
{
    public class AnchorDriver : ControlDriverBase
    {
        public AnchorDriver(IWebElement element) : base(element) { }
        public AnchorDriver(IWebElement element, Action wait = null) : base(element){ Wait = wait; }

        public string Text => Element.Text;
        public Action Wait { get; set; }

        public void Invoke()
        {
            var js = JS;
            Element.Show();
            Element.Focus();
            Element.SendKeys(Keys.Enter);
            try
            {
                js.ExecuteScript("");//sync.
            }
            catch { }
            Wait?.Invoke();
        }

        public static implicit operator AnchorDriver(ElementFinder finder) => new AnchorDriver(finder.Find());
    }
}
