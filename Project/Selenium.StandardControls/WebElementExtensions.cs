using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.StandardControls
{
    public static class WebElementExtensions
    {
        public static IWebElement GetParent(this IWebElement element) => element.FindElement(By.XPath("parent::node()"));
        public static RemoteWebElement GetRemoteWebElement(this IWebElement element) => element as RemoteWebElement;
        public static IWebDriver GetWebDriver(this IWebElement element) => GetRemoteWebElement(element)?.WrappedDriver;
        public static IJavaScriptExecutor GetJS(this IWebElement element) => GetWebDriver(element) as IJavaScriptExecutor;
    }
}
