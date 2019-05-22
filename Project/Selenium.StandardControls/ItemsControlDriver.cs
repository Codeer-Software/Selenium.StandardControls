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
            return null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element for generating the driver</param>
        public ItemsControlDriver(IWebElement element) : base(element){}

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="finder">Convert</param>
        public static implicit operator ItemsControlDriver<T>(ElementFinder finder)=> new ItemsControlDriver<T>(finder.Find());
    }
}
