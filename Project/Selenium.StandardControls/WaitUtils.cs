using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
using System;
using System.Linq;

namespace Selenium.StandardControls
{
    public static class WaitUtils
    {
        public static TResult Until<TResult>(this IWebDriver driver, Func<TResult> condition)
            => driver.Until(condition, Settings.DefaultWaitTime);

        public static TResult Until<TResult>(this IWebDriver driver, Func<TResult> condition, TimeSpan timeout)
            => new WebDriverWait(driver, timeout).Until(_ => condition());

        public static TResult Until<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition)
            => driver.Until(condition, Settings.DefaultWaitTime);

        public static TResult Until<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> condition, TimeSpan timeout)
            => new WebDriverWait(driver, timeout).Until(e => condition(e));

        public static void WaitForUrl(this IWebDriver driver, UrlCompareType compareType, params string[] urls)
            => driver.WaitForUrl(compareType, Settings.DefaultWaitTime, urls);

        public static void WaitForUrl(this IWebDriver driver, UrlCompareType compareType, TimeSpan timeout, params string[] urls)
        {
            new WebDriverWait(driver, timeout).Until(_ => 
            {
                switch (compareType)
                {
                    case UrlCompareType.Contains:
                        return urls.Any(e => driver.Url.Contains(e));
                    case UrlCompareType.EndsWith:
                        return urls.Any(e => driver.Url.EndsWith(e));
                    case UrlCompareType.StartsWith:
                        return urls.Any(e => driver.Url.StartsWith(e));
                    case UrlCompareType.Equals:
                        return urls.Any(e => driver.Url.Equals(e));
                    default:
                        throw new NotSupportedException();
                }
            });
        }

        public static void WaitForTitle(this IWebDriver driver, TitleCompareType compareType, params string[] titles)
            => driver.WaitForTitle(compareType, Settings.DefaultWaitTime, titles);

        public static void WaitForTitle(this IWebDriver driver, TitleCompareType compareType, TimeSpan timeout, params string[] titles)
        {
            new WebDriverWait(driver, timeout).Until(_ =>
            {
                switch (compareType)
                {
                    case TitleCompareType.Contains:
                        return titles.Any(e => driver.Title.Contains(e));
                    case TitleCompareType.EndsWith:
                        return titles.Any(e => driver.Title.EndsWith(e));
                    case TitleCompareType.StartsWith:
                        return titles.Any(e => driver.Title.StartsWith(e));
                    case TitleCompareType.Equals:
                        return titles.Any(e => driver.Title.Equals(e));
                    default:
                        throw new NotSupportedException();
                }
            });
        }

        /// <summary>
        /// Wait for Alert.
        /// </summary>
        /// <param name="driver">WebDriver.</param>
        /// <param name="waitMilliseconds">wait time. default value is int.MaxValue.</param>
        /// <returns>IAlert.</returns>
        public static IAlert WaitForAlert(this IWebDriver driver, int waitMilliseconds = int.MaxValue)
            => new WebDriverWait(driver, TimeSpan.FromMilliseconds(waitMilliseconds)).Until(_ => driver.SwitchTo().Alert());

        [ElementFinderWait]
        public static ElementFinder WaitForDisplayed(this ElementFinder src) => src.Wait(e => e.Displayed);

        [ElementFinderWait]
        public static ElementFinder WaitForClickable(this ElementFinder src) => src.Wait(e => 
        {
            if (!e.Displayed || !e.Enabled) return false;

            e.Show();
            return e.HitTestCenter();
        });
    }
}
