using System;
using OpenQA.Selenium;

namespace Selenium.StandardControls
{
    class ElementWebElement : IElementCore
    {
        private IWebElement Element { get; }

        internal ElementWebElement(IWebElement element)
        {
            Element = element;
        }

        public T GetAttribute<T>(string name)
        {
            var o = Element.GetAttribute(name);
            if (typeof(T) == typeof(int?)) return (o == null) ? default(T) : (T)(object)int.Parse(o);
            if (typeof(T) == typeof(string)) return (T)(object)o;
            if (typeof(T) == typeof(long)) return (T)(object)long.Parse(o);
            throw new ArgumentOutOfRangeException("");
        }

        public string GetCssValue(string name) => Element.GetCssValue(name);
    }
}