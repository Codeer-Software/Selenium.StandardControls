using System;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public enum UrlComapreType
    {
        None,
        Contains,
        StartsWith,
        EndsWith,
        Equals,
    }

    public enum TitleComapreType
    {
        None,
        Contains,
        StartsWith,
        EndsWith,
        Equals,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PageObjectIdentifyAttribute : Attribute
    {
        public string Url { get; set; }
        public UrlComapreType UrlComapreType { get; set; } = UrlComapreType.None;
        public string Title { get; set; }
        public TitleComapreType TitleComapreType { get; set; } = TitleComapreType.None;

        public PageObjectIdentifyAttribute(string url, UrlComapreType urlCompareType)
        {
            Url = url;
            UrlComapreType = urlCompareType;
        }

        public PageObjectIdentifyAttribute(string title, TitleComapreType titleCompareType)
        {
            Title = title;
            TitleComapreType = titleCompareType;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CaptureCodeGeneratorAttribute : Attribute
    {
        public string ControlDriverTypeFullName { get; set; }
        public int Priority { get; set; }
    }
}
