using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Button Driver
    /// </summary>
    public class ItemsControlDriver<T> : ControlDriverBase where T : class
    {
        public int Count { get; }

        public T GetItem(int index)
        {
            var indexConstructor = typeof(T).GetConstructor(new[] { typeof(IWebElement), typeof(int) });
            var element = JS.ExecuteScript("arguments[0].children[arguments[1]];", Element, index);

            if (indexConstructor != null) return (T)Activator.CreateInstance(typeof(T), new object[] { element, index });
            return (T)Activator.CreateInstance(typeof(T), new object[] { element });
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public ItemsControlDriver(IWebElement element) : base(element) { }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="finder">Convert</param>
        public static implicit operator ItemsControlDriver<T>(ElementFinder finder) => new ItemsControlDriver<T>(finder.Find());
    }
}
