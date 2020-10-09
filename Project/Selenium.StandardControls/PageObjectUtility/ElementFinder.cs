using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;
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
        int _waitMilliseconds = -1;

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

        ElementFinder(ElementFinder innerFinder, By by)
        {
            _innerFinder = innerFinder;
            _by = by;
        }

        ElementFinder(ISearchContext context, By by, ElementFinder innerFinder, int index, int waitMilliseconds)
        {
            _context = context;
            _by = by;
            _innerFinder = innerFinder;
            _index = index;
            _waitMilliseconds = waitMilliseconds;
        }

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement Find()
        {
            if (!TestAssistantMode.IsCreatingMode)
            {
                if (_waitMilliseconds != -1)
                {
                    var driver = GetWebDriver();
                    if (driver == null) return null;
                    return new WebDriverWait(driver, TimeSpan.FromMilliseconds(_waitMilliseconds)).Until(_ => FindCore());
                }
            }
            return FindCore();
        }

        IWebDriver GetWebDriver()
        {
            var driver = _context as IWebDriver;
            if (driver != null) return driver;

            driver = (_context as IWrapsDriver)?.WrappedDriver;
            if (driver != null) return driver;

            if (_innerFinder != null) return _innerFinder.GetWebDriver();

            return null;
        }

        /// <summary>
        /// Add wait.
        /// </summary>
        /// <param name="waitMilliseconds">Waiting time. default value is int.MaxValue</param>
        /// <returns>ElementFinder.</returns>
        public ElementFinder Wait(int waitMilliseconds = int.MaxValue)
            => new ElementFinder(_context, _by, _innerFinder, _index, waitMilliseconds);

        IWebElement FindCore()
        {
            if (_innerFinder != null)
            {
                if (_by == null)
                {
                    var elements = _innerFinder.FindMany();
                    if (_index < elements.Length) return elements[_index];
                    return null;
                }
                else
                {
                    return _innerFinder.Find().FindElement(_by);
                }
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
            if (_innerFinder != null)
            {
                if (_by == null)
                {
                    return new[] { _innerFinder.FindMany()[_index] };
                }
                else
                {
                    return _innerFinder.Find().FindElements(_by).ToArray();
                }
            }
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

        /// <summary>
        /// Find Element in ClassName
        /// </summary>
        /// <param name="classNameToFind">ClassName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByClassName(string classNameToFind) => new ElementFinder(this, By.ClassName(classNameToFind));
        /// <summary>
        /// Find Element in CssSelector
        /// </summary>
        /// <param name="cssSelectorToFind">CssSelector</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByCssSelector(string cssSelectorToFind) => new ElementFinder(this, By.CssSelector(cssSelectorToFind));
        /// <summary>
        /// Find Element in Id
        /// </summary>
        /// <param name="idToFind">ElementFinder</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ById(string idToFind) => new ElementFinder(this, By.Id(idToFind));
        /// <summary>
        /// Find Element in LinkText
        /// </summary>
        /// <param name="linkTextToFind">LinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByLinkText(string linkTextToFind) => new ElementFinder(this, By.LinkText(linkTextToFind));
        /// <summary>
        /// Find Element in ByName
        /// </summary>
        /// <param name="nameToFind">ByName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByName(string nameToFind) => new ElementFinder(this, By.Name(nameToFind));
        /// <summary>
        /// Find Element in PartialLinkText
        /// </summary>
        /// <param name="partialLinkTextToFind">PartialLinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByPartialLinkText(string partialLinkTextToFind) => new ElementFinder(this, By.PartialLinkText(partialLinkTextToFind));
        /// <summary>
        /// Find Element in TagName
        /// </summary>
        /// <param name="tagNameToFind">TagName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByTagName(string tagNameToFind) => new ElementFinder(this, By.TagName(tagNameToFind));

        /// <summary>
        /// Find Element in XPath
        /// </summary>
        /// <param name="xpathToFind">XPath</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByXPath(string xpathToFind) => new ElementFinder(this, By.XPath(xpathToFind));
    }
}
