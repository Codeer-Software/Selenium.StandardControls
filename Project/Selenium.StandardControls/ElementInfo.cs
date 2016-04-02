using OpenQA.Selenium;
using System;

namespace Selenium.StandardControls
{
    //TODO need test.
    /// <summary>
    /// JavaScript Element Driver
    /// </summary>
    public class ElementInfo
    {
        private IWebElement Core { get; }

        public string Id => GetAttribute<string>("id");
        public string InnerHtml => GetAttribute<string>("innerHTML");
        public string InnerText => GetAttribute<string>("innerText");
        public string Text => GetAttribute<string>("text");
        public string Value => GetAttribute<string>("value");
        public string CssClass => GetCssValue("className");
        public string Width => GetCssValue("width");
        public string Height => GetCssValue("height");
        public string FontSize => GetCssValue("fontSize");
        public string Font => GetCssValue("fontFamily");

        //ToDo: bold = Firefox is 700. Other Browther is un know.
        public bool FontBold => GetCssValue("fontWeight") == "700" || GetCssValue("fontWeight") == "bold";

        public bool FontItalic => GetCssValue("fontStyle") == "italic";
        public bool TextUnderline => GetCssValue("textDecoration").Contains("underline");
        public bool TextLineThrough => GetCssValue("textDecoration").Contains("line-through");
        public string Color => GetCssValue("color");
        public string BackGroundColor => GetCssValue("backgroundColor");
        public string BackGroundImage => GetCssValue("backgroundImage");
        public long TabIndex => GetAttribute<long>("tabIndex");
        public string ImeMode => GetCssValue("imeMode");
        public int? MaxLength => GetAttribute<int?>("maxLength");
        public string TextAlign => GetCssValue("textAlign");

        public ElementInfo(IWebElement core)
        {
            Core = core;
        }
        
        public T GetAttribute<T>(string name)
        {
            var o = Core.GetAttribute(name);
            if (typeof(T) == typeof(int?)) return (o == null) ? default(T) : (T)(object)int.Parse(o);
            if (typeof(T) == typeof(string)) return (T)(object)o;
            if (typeof(T) == typeof(long)) return (T)(object)long.Parse(o);
            throw new ArgumentOutOfRangeException("");
        }

        public string GetCssValue(string name) => Core.GetCssValue(name);
    }
}