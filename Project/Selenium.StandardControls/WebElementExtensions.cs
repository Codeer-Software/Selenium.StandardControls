using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

namespace Selenium.StandardControls
{
    /// <summary>
    /// WebElement extension methods
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Acquisition of the parent element
        /// </summary>
        /// <param name="element">Target element</param>
        /// <returns>Parent element</returns>
        public static IWebElement GetParent(this IWebElement element) => element.FindElement(By.XPath("parent::node()"));
        /// <summary>
        /// Simple to RemoteWebElement accessor
        /// </summary>
        /// <param name="element"></param>
        /// <returns>RemoteWebElement</returns>
        public static RemoteWebElement GetRemoteWebElement(this IWebElement element) => element as RemoteWebElement;
        /// <summary>
        /// Simple to ILocatable accessor
        /// </summary>
        /// <param name="element"></param>
        /// <returns>ILocatable</returns>
        public static ILocatable GetLocatable(this IWebElement element) => element as ILocatable;
        /// <summary>
        /// Simple to IWrapsDriver accessor
        /// </summary>
        /// <param name="element"></param>
        /// <returns>IWrapsDriver</returns>
        public static IWrapsDriver GetWrapsDriver(this IWebElement element) => element as IWrapsDriver;
        /// <summary>
        /// Simple to IWebDriver accessor
        /// </summary>
        /// <param name="element">Target element</param>
        /// <returns>IWebDriver</returns>
        public static IWebDriver GetWebDriver(this IWebElement element) => GetWrapsDriver(element)?.WrappedDriver;
        /// <summary>
        /// Simple to IJavaScriptExecutor accessor
        /// </summary>
        /// <param name="element">Target element</param>
        /// <returns>IJavaScriptExecutor</returns>
        public static IJavaScriptExecutor GetJS(this IWebElement element) => GetWebDriver(element) as IJavaScriptExecutor;
        /// <summary>
        /// Show Scroll If there is no element in the screen
        /// </summary>
        /// <param name="element">Target element</param>
        public static void Show(this IWebElement element)
        {
            var locatar = element.GetLocatable();
            if (locatar == null) return;
            locatar.LocationOnScreenOnceScrolledIntoView.ToString();
        }
        /// <summary>
         ///Reliably Show(Show failure and other elements at the top)
         /// </summary>
         /// <param name="element">Target element</param>
         /// <param name="alignToTop"></param>
        public static void ScrollIntoView(this IWebElement element, bool alignToTop) => element.GetJS().ExecuteScript($"arguments[0].scrollIntoView({alignToTop.ToString().ToLower()});", element);
        /// <summary>
        /// The focus to Element
        /// </summary>
        public static void Focus(this IWebElement element) => element.GetJS().ExecuteScript("arguments[0].focus();", element);
        /// <summary>
        /// Remove the focus from Element
        /// </summary>
        public static void Blur(this IWebElement element) => element.GetJS().ExecuteScript("arguments[0].blur();", element);
    }
}
