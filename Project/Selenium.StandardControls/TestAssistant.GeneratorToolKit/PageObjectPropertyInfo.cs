using OpenQA.Selenium;
using System.Reflection;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class PageObjectPropertyInfo
    {
        public IWebElement Element { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Identify { get; set; }
        public MethodInfo Wait { get; set; }
    }
}
