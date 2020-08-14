using System;
using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Selenium.StandardControls
{
    /// <summary>
    /// Items Control Driver.
    /// Access item by index.
    /// </summary>
    /// <typeparam name="T">Item's type.</typeparam>
    public interface IIndexAccessItemsControlDriver<T>
    {
        /// <summary>
        /// Item count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Item</returns>
        T GetItem(int index);
    }

    //TODO
    //public interface IKeyItemsControlDriver<T>
    //{
    //    string[] Keys { get; }
    //    T GetItem(string key);
    //}

    /// <summary>
    /// Items Control Driver
    /// </summary>
    /// <typeparam name="T">Item's type.</typeparam>
    public class ItemsControlDriver<T> : ControlDriverBase, IIndexAccessItemsControlDriver<T> where T : class
    {
        /// <summary>
        /// Item count.
        /// </summary>
        public int Count => (int)(long)JS.ExecuteScript("return arguments[0].children.length;", Element);

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="index">index</param>
        /// <returns></returns>
        public T GetItem(int index)
        {
            var indexConstructor = typeof(T).GetConstructor(new[] { typeof(IWebElement), typeof(int) });
            var element = JS.ExecuteScript("return arguments[0].children[arguments[1]];", Element, index);

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
        public static implicit operator ItemsControlDriver<T>(ElementFinder finder) => finder.Find<ItemsControlDriver<T>>();

        /// <summary>
        /// Element Info.
        /// </summary>
        [TargetElementInfo]
        public static TargetElementInfo TargetElement => new TargetElementInfo("tbody");
    }
}
