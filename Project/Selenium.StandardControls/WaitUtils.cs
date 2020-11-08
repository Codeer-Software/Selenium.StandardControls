using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public static void WaitForUrl(this IWebDriver driver, UrlComapreType comapreType, params string[] urls)
            => driver.WaitForUrl(comapreType, Settings.DefaultWaitTime, urls);

        public static void WaitForUrl(this IWebDriver driver, UrlComapreType comapreType, TimeSpan timeout, params string[] urls)
        {
            new WebDriverWait(driver, timeout).Until(_ => 
            {
                switch (comapreType)
                {
                    case UrlComapreType.Contains:
                        return urls.Any(e => driver.Url.Contains(e));
                    case UrlComapreType.EndsWith:
                        return urls.Any(e => driver.Url.EndsWith(e));
                    case UrlComapreType.StartsWith:
                        return urls.Any(e => driver.Url.StartsWith(e));
                    case UrlComapreType.Equals:
                        return urls.Any(e => driver.Url.Equals(e));
                    default:
                        throw new NotSupportedException();
                }
            });
        }

        public static void WaitForTitle(this IWebDriver driver, TitleComapreType comapreType, params string[] titles)
            => driver.WaitForTitle(comapreType, Settings.DefaultWaitTime, titles);

        public static void WaitForTitle(this IWebDriver driver, TitleComapreType comapreType, TimeSpan timeout, params string[] titles)
        {
            new WebDriverWait(driver, timeout).Until(_ =>
            {
                switch (comapreType)
                {
                    case TitleComapreType.Contains:
                        return titles.Any(e => driver.Title.Contains(e));
                    case TitleComapreType.EndsWith:
                        return titles.Any(e => driver.Title.EndsWith(e));
                    case TitleComapreType.StartsWith:
                        return titles.Any(e => driver.Title.StartsWith(e));
                    case TitleComapreType.Equals:
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
    }
}
