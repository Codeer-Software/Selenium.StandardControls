using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.StandardControls
{
    /// <summary>
    /// WebDriver extension methods
    /// </summary>
    public static class WebDriverExtensions
    {
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
