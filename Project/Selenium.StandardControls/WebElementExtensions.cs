using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using Selenium.StandardControls.Properties;
using System;

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
            if (HitTestCenter(element)) return;

            var locatar = element.GetLocatable();
            if (locatar == null) return;
            locatar.LocationOnScreenOnceScrolledIntoView.ToString();

            if (HitTestCenter(element)) return;
            element.Blur();
            element.Focus();

            if (HitTestCenter(element)) return;
            element.ScrollIntoView(true);

            if (HitTestCenter(element)) return;

            var scroll = GetYScrollElement(element);
            var amount = Math.Max(element.Size.Height / 2, 50);
            for (var i = 0; i < 20; i++)
            {
                ScrollY(element, scroll, amount);
                if (HitTestCenter(element)) return;
            }
        }

        static void ScrollY(IWebElement element, IWebElement scroll, int amount)
            => element.GetJS().ExecuteScript(@"
var target = arguments[0];
if (target == null) target = window;
target.scrollBy(0, arguments[1]);
", scroll, -amount);

        static IWebElement GetYScrollElement(IWebElement element)
        {
            try
            {
                return element.GetJS().ExecuteScript(@"
var node = arguments[0].parentNode;
while(node != null)
{
    if (window.getComputedStyle(node).overflowY === 'scroll') return node;
    node = node.parentNode;
}
return null;
", element) as IWebElement;
            }
            catch { }
            return null;
        }

        internal static bool HitTestCenter(this IWebElement element)
            => (bool)element.GetJS().ExecuteScript(@"
var element = arguments[0];
var rc = element.getBoundingClientRect();
var hitElement = document.elementFromPoint(rc.x + rc.width / 2, rc.y + rc.height / 2);

while (!!hitElement)
{
    if (hitElement === element) return true;
    hitElement = hitElement.parentElement;
}
return false;
", element);

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

        /// <summary>
        /// Starts the search from the element specified by findStart, and then returns the element that matches the condition specified by by.
        /// </summary>
        /// <param name="findStart">Element to start the search.</param>
        /// <param name="by">condition.</param>
        /// <returns>Element.</returns>
        public static IWebElement FindNextElement(this IWebElement findStart, By by)
        {
            var text = by.ToString();
            var type = string.Empty;
            var param = string.Empty;
            if (text.StartsWith("By.Id:"))
            {
                type = "id";
                param = text.Substring("By.Id:".Length).Trim();;
            }
            if (text.StartsWith("By.Name:"))
            {
                type = "name";
                param = text.Substring("By.Name:".Length).Trim();
            }
            if (text.StartsWith("By.ClassName[Contains]:"))
            {
                type = "className";
                param = text.Substring("By.ClassName[Contains]:".Length).Trim();
            }
            if (text.StartsWith("By.CssSelector:"))
            {
                type = "cssSelector";
                param = text.Substring("By.CssSelector:".Length).Trim();
            }
            if (text.StartsWith("By.TagName:"))
            {
                type = "tagName";
                param = text.Substring("By.TagName:".Length).Trim();
            }
            if (text.StartsWith("By.XPath:"))
            {
                type = "xpath";
                param = text.Substring("By.XPath:".Length).Trim();
            }
            if (text.StartsWith("By.LinkText:"))
            {
                type = "linkText";
                param = text.Substring("By.LinkText:".Length).Trim();
            }
            if (text.StartsWith("By.PartialLinkText:"))
            {
                type = "partialLinkText";
                param = text.Substring("By.PartialLinkText:".Length).Trim();
            }

            var js = Resources.FindNextElement + @"
            return findNextElement(arguments[0], arguments[1], arguments[2]);
";
            return (IWebElement)findStart.GetJS().ExecuteScript(js, type, param, findStart);
        }
    }
}
