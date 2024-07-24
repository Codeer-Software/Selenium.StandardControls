using System;
using System.Linq;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public enum UrlCompareType
    {
        None,
        Contains,
        StartsWith,
        EndsWith,
        Equals,
    }

    public enum TitleCompareType
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
        public UrlCompareType UrlCompareType { get; set; } = UrlCompareType.None;
        public TitleCompareType TitleCompareType { get; set; } = TitleCompareType.None;

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

        public PageObjectIdentifyAttribute(UrlCompareType urlCompareType, params string[] urls)
        {
            UrlCompareType = urlCompareType;
            Urls = urls;
        }

        public PageObjectIdentifyAttribute(TitleCompareType titleCompareType, params string[] titles)
        {
            TitleCompareType = titleCompareType;
            Titles = titles;
        }

        [Obsolete("Please use PageObjectIdentifyAttribute(UrlCompareType urlCompareType, params string[] urls)")]
        public PageObjectIdentifyAttribute(string url, UrlCompareType urlCompareType)
        {
            Url = url;
            UrlCompareType = urlCompareType;
        }

        [Obsolete("Please use PageObjectIdentifyAttribute(TitleCompareType titleCompareType, params string[] titles)")]
        public PageObjectIdentifyAttribute(string title, TitleCompareType titleCompareType)
        {
            Title = title;
            TitleCompareType = titleCompareType;
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
