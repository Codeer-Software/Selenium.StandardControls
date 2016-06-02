using OpenQA.Selenium;
using System;

namespace Selenium.StandardControls
{
    //TODO need test.
    /// <summary>
    /// More information of Element
    /// </summary>
    public class ElementInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="core">Element for generating the ElementInfo</param>
        public ElementInfo(IWebElement core)
        {
            Core = core;
        }

        private IWebElement Core { get; }
        /// <summary>
        /// To get the id attributes of
        /// </summary>
        public string Id => GetAttribute<string>("id");
        /// <summary>
        /// To get the innerHTML attributes of
        /// </summary>
        public string InnerHtml => GetAttribute<string>("innerHTML");
        /// <summary>
        /// To get the innerText attributes of
        /// </summary>
        public string InnerText => GetAttribute<string>("innerText");
        /// <summary>
        /// To get the text attributes of
        /// </summary>
        public string Text => GetAttribute<string>("text");
        /// <summary>
        /// To get the value attributes of
        /// </summary>
        public string Value => GetAttribute<string>("value");
        /// <summary>
        /// To get the class attributes of
        /// </summary>
        public string Class => GetAttribute<string>("class");
        /// <summary>
        /// To get the width css value of
        /// </summary>
        public string Width => GetCssValue("width");
        /// <summary>
        /// To get the height css value of
        /// </summary>
        public string Height => GetCssValue("height");
        /// <summary>
        /// To get the fontSize css value of
        /// </summary>
        public string FontSize => GetCssValue("fontSize");
        /// <summary>
        /// To get the fontFamily css value of
        /// </summary>
        public string Font => GetCssValue("fontFamily");
        /// <summary>
        /// To get the fontWeight css value of
        /// </summary>
        public bool FontBold => GetCssValue("fontWeight") == "700" || GetCssValue("fontWeight") == "bold";
        /// <summary>
        /// To get the fontStyle css value of
        /// </summary>
        public bool FontItalic => GetCssValue("fontStyle") == "italic";
        /// <summary>
        /// To get the textDecoration css value of contains underline
        /// </summary>
        public bool TextUnderline => GetCssValue("textDecoration").Contains("underline");
        /// <summary>
        /// To get the textDecoration css value of contains line-through
        /// </summary>
        public bool TextLineThrough => GetCssValue("textDecoration").Contains("line-through");
        /// <summary>
        /// To get the color css value of
        /// </summary>
        public string Color => GetCssValue("color");
        /// <summary>
        /// To get the backgroundColor css value of
        /// </summary>
        public string BackGroundColor => GetCssValue("backgroundColor");
        /// <summary>
        /// To get the backgroundImage css value of
        /// </summary>
        public string BackGroundImage => GetCssValue("backgroundImage");
        /// <summary>
        /// To get the tabIndex attributes of
        /// </summary>
        public long TabIndex => GetAttribute<long>("tabIndex");
        /// <summary>
        /// To get the imeMode css value of
        /// </summary>
        public string ImeMode => GetCssValue("imeMode");
        /// <summary>
        /// To get the maxLength attributes of
        /// </summary>
        public int? MaxLength => GetAttribute<int?>("maxLength");
        /// <summary>
        /// To get the textAlign css value of
        /// </summary>
        public string TextAlign => GetCssValue("textAlign");

        /// <summary>
        /// Gets the value of the specified attribute for this element
        /// </summary>
        /// <typeparam name="T">Type of attributes(ex. text:int tabIndex:long)</typeparam>
        /// <param name="name">The name of the attribute</param>
        /// <returns>The attribute's current value. Returns a null if the value is not set</returns>
        public T GetAttribute<T>(string name)
        {
            var o = Core.GetAttribute(name);
            if (typeof(T) == typeof(int?)) return (o == null) ? default(T) : (T)(object)int.Parse(o);
            if (typeof(T) == typeof(string)) return (T)(object)o;
            if (typeof(T) == typeof(long)) return (T)(object)long.Parse(o);
            throw new ArgumentOutOfRangeException("");
        }

        /// <summary>
        /// Gets the value of a CSS property of this element
        /// </summary>
        /// <param name="name">The name of the CSS property to get the value of</param>
        /// <returns>The value of the specified CSS property</returns>
        public string GetCssValue(string name) => Core.GetCssValue(name);
    }
}