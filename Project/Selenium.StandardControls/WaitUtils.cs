using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
using System;

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

        public static void WaitForUrl(this IWebDriver driver, string url, UrlComapreType comapreType)
            => driver.WaitForUrl(url, comapreType, Settings.DefaultWaitTime);

        public static void WaitForUrl(this IWebDriver driver, string url, UrlComapreType comapreType, TimeSpan timeout)
        {
            new WebDriverWait(driver, timeout).Until(_ => 
            {
                switch (comapreType)
                {
                    case UrlComapreType.Contains:
                        return driver.Url.Contains(url);
                    case UrlComapreType.EndsWith:
                        return driver.Url.EndsWith(url);
                    case UrlComapreType.StartsWith:
                        return driver.Url.StartsWith(url);
                    case UrlComapreType.Equals:
                        return driver.Url.Equals(url);
                    default:
                        throw new NotSupportedException();
                }
            });
        }

        public static void WaitForTitle(this IWebDriver driver, string title, TitleComapreType comapreType)
            => driver.WaitForTitle(title, comapreType, Settings.DefaultWaitTime);

        public static void WaitForTitle(this IWebDriver driver, string title, TitleComapreType comapreType, TimeSpan timeout)
        {
            new WebDriverWait(driver, timeout).Until(_ =>
            {
                switch (comapreType)
                {
                    case TitleComapreType.Contains:
                        return driver.Title.Contains(title);
                    case TitleComapreType.EndsWith:
                        return driver.Title.EndsWith(title);
                    case TitleComapreType.StartsWith:
                        return driver.Title.StartsWith(title);
                    case TitleComapreType.Equals:
                        return driver.Title.Equals(title);
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
