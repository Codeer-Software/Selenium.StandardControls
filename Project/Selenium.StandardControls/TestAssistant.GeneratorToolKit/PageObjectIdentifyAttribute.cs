using System;
using System.Linq;

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
        public UrlComapreType UrlComapreType { get; set; } = UrlComapreType.None;
        public TitleComapreType TitleComapreType { get; set; } = TitleComapreType.None;

        public string Url 
        {
            get => Urls?.FirstOrDefault();
            set => Urls = new[] { value };
        }
        
        public string Title
        {
            get => Titles?.FirstOrDefault();
            set => Titles = new[] { value };
        }

        public string[] Urls { get; set; }
        public string[] Titles { get; set; }

        public PageObjectIdentifyAttribute(UrlComapreType urlCompareType, params string[] urls)
        {
            UrlComapreType = urlCompareType;
            Urls = urls;
        }

        public PageObjectIdentifyAttribute(TitleComapreType titleCompareType, params string[] titles)
        {
            TitleComapreType = titleCompareType;
            Titles = titles;
        }

        [Obsolete("Please use PageObjectIdentifyAttribute(UrlComapreType urlCompareType, params string[] urls)")]
        public PageObjectIdentifyAttribute(string url, UrlComapreType urlCompareType)
        {
            Url = url;
            UrlComapreType = urlCompareType;
        }

        [Obsolete("Please use PageObjectIdentifyAttribute(TitleComapreType titleCompareType, params string[] titles)")]
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

    [AttributeUsage(AttributeTargets.Method)]
    public class ComponentObjectIdentifyAttribute : Attribute
    {
    }
}
