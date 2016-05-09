using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Selenium.StandardControls.AdjustBrowser
{
    public static class AdjustBrowserExtensions
    {
        public static void ClickEx(this IWebElement element)
        {
            if (element.GetWebDriver() is InternetExplorerDriver)
            {
                var js = element.GetJS();
                element.Show();
                element.Focus();
                js.ExecuteScript("arguments[0].click();", element);
                try
                {
                    js.ExecuteScript("");//sync.
                }
                catch { }
            }
            else
            {
                element.Click();
            }
        }
    }
}
