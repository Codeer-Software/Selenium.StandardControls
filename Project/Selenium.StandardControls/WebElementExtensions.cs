using OpenQA.Selenium;

namespace Selenium.StandardControls
{
    public static class WebElementExtensions
    {
        public static IWebElement GetParent(this IWebElement element)
        {
            return element.FindElement(By.XPath("parent::node()"));
        }
    }
}
