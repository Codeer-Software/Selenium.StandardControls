using OpenQA.Selenium;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public interface IControlDriverTypeSelector
    {
        int Priority { get; }
        string GetControlDriverType(IWebElement element);
    }
}
