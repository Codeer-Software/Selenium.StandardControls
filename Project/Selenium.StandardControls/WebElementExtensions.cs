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
        public static void Show(this IWebElement element) => element.GetRemoteWebElement().LocationOnScreenOnceScrolledIntoView.ToString();
        public static void ScrollIntoView(this IWebElement element, bool alignToTop) => element.GetJS().ExecuteScript($"arguments[0].scrollIntoView({alignToTop.ToString().ToLower()});", element);
        public static void Focus(this IWebElement element) => element.GetJS().ExecuteScript("arguments[0].focus();", element);
        public static void Blur(this IWebElement element) => element.GetJS().ExecuteScript("arguments[0].blur();", element);
    }
}
