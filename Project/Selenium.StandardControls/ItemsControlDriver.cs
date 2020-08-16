using System;
using System.Collections;
using System.Linq;
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
    [Obsolete]
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

    /// <summary>
    /// Items Control Driver.
    /// Access item by key.
    /// </summary>
    /// <typeparam name="Key">Key type.</typeparam>
    /// <typeparam name="Driver">Driver type.</typeparam>
    public interface IKeyAccessItemsControlDriver<Key, Driver>
    {
        /// <summary>
        /// Keys of visible item.
        /// </summary>
        Key[] VisibleItemKeys { get; }

        /// <summary>
        /// Get item.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>item.</returns>
        Driver GetItem(Key key);

        /// <summary>
        /// Code text of argument passed to GetItem.
        /// Used by TestAssistantPro during capture.
        /// </summary>
        /// <param name="key">key.</param>
        /// <returns>text.</returns>
        string ToArgumentCode(Key key);
    }

    /// <summary>
    /// Items Control Driver
    /// </summary>
    /// <typeparam name="T">Item's type.</typeparam>
    public class ItemsControlDriver<T> : ControlDriverBase, IKeyAccessItemsControlDriver<int, T> where T : class
    {
        /// <summary>
        /// Keys of visible item.
        /// </summary>
        public int[] VisibleItemKeys
        {
            get
            {
                var visibles = (IEnumerable)Element.GetJS().ExecuteScript(@"
var parent = arguments[0];
var visibles = [];
for (var i = 0; i < parent.children.length; i++)
{
    var rect = parent.children[i].getBoundingClientRect();
    if (0 < rect.bottom && rect.top < window.innerHeight) {
        visibles.push(i);
    }
}
return visibles;
", Element);
                return visibles.Cast<long>().Select(e => (int)e).ToArray();
            }
        }

        /// <summary>
        /// Code text of argument passed to GetItem.
        /// Used by TestAssistantPro during capture.
        /// </summary>
        /// <param name="key">key.</param>
        /// <returns>text.</returns>
        public string ToArgumentCode(int key) => key.ToString();

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
