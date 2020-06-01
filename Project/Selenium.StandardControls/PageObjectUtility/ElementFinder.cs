using OpenQA.Selenium;
using System;
using System.Linq;

namespace Selenium.StandardControls.PageObjectUtility
{
    /// <summary>
    /// Find Element
    /// </summary>
    public class ElementFinder
    {
        ISearchContext _context;
        By _by;

        ElementFinder _innerFinder;
        int _index;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Defines the interface used to search for elements</param>
        /// <param name="by">Provides a mechanism by which to find elements within a document</param>
        public ElementFinder(ISearchContext context, By by)
        {
            _context = context;
            _by = by;
        }

        ElementFinder(ElementFinder innerFinder, int index)
        {
            _innerFinder = innerFinder;
            _index = index;
        }

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method
        /// </summary>
        /// <returns></returns>
        public IWebElement Find()
        {
            if (_innerFinder != null)
            {
                var elements = _innerFinder.FindMany();
                if (_index < elements.Length) return elements[_index];
                return null;
            }
            else
            {
                var elements = _context.FindElements(_by);
                if (elements.Count != 1) return null;
                return elements[0];
            }
        }

        IWebElement[] FindMany()
        {
            if (_innerFinder != null) return new[] { _innerFinder.FindMany()[_index] };
            return _context.FindElements(_by).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ElementFinder this[int index]
        {
            get
            {
                return new ElementFinder(this, index);
            }
        }

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method. And Convert to T. T have to have constructor of one IWebElement arugment.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>T</returns>
        public T Find<T>() where T : class
        {
            var element = Find();
            return element == null ? null : (T)Activator.CreateInstance(typeof(T), new object[] { element });
        }
    }
}
