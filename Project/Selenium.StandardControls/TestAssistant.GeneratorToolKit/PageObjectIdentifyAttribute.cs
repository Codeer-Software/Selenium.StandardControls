using System;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public enum UrlComapreType
    {
        Contains,
        StartsWith,
        EndsWith,
        Equals,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PageObjectIdentifyAttribute : Attribute
    {
        public string Url { get; set; }
        public UrlComapreType UrlComapreType { get; set; } = UrlComapreType.Contains;
        public PageObjectIdentifyAttribute(string url, UrlComapreType urlCompareType)
        {
            Url = url;
            UrlComapreType = urlCompareType;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CaptureCodeGeneratorAttribute : Attribute
    {
        public string ControlDriverTypeFullName { get; set; }
        public int Priority { get; set; }
    }
}
