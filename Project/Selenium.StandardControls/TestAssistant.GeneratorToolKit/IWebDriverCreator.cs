namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Interface for customizing the creation of WebDriver used by TestAssistantPro.
    /// </summary>
    public interface IWebDriverCreator
    {
        /// <summary>
        /// Create a WebDriver.
        /// </summary>
        /// <returns>IWebDriver</returns>
        object CreateDriver();
    }
}
