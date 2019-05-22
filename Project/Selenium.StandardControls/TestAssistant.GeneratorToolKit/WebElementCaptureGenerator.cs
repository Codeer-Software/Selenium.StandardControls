using OpenQA.Selenium;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace UnitTest.Lib.Toolkit
{
    public static class WebElementCaptureGenerator
    {
        [CaptureCodeGenerator(ControlDriverTypeFullName = "OpenQA.Selenium.IWebElement")]
        public static string GetWebElementCaptureGenerator()
        {
            return $@"
                    element.addEventListener('change', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Clear();');
                      __codeerTestAssistantPro.pushCode(name + '.SendKeys(""' + this.value + '"");');
                    }}, false);
                    element.addEventListener('click', function() {{ 
                      var name = __codeerTestAssistantPro.getElementName(this);
                      __codeerTestAssistantPro.pushCode(name + '.Click();');
                    }}, false);";
        }
    }
}
