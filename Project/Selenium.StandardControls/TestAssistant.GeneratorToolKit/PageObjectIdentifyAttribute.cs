using System;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PageObjectIdentifyAttribute : Attribute
    {
        public string PartOfUrl { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CaptureCodeGeneratorAttribute : Attribute
    {
        public string ControlDriverTypeFullName { get; set; }
        public int Priority { get; set; }
    }
}
