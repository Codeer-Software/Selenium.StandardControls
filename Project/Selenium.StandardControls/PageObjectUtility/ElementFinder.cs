﻿using OpenQA.Selenium;
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
        Func<ISearchContext, IWebElement[]> _byExecutor;
        ElementFinder _innerFinder;
        int _index;
        Func<IWebElement, bool> _isValidElement;

        /// <summary>
        /// Waiting time
        /// </summary>
        public TimeSpan? Timeout { get; private set; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Defines the interface used to search for elements</param>
        /// <param name="by">Provides a mechanism by which to find elements within a document</param>
        public ElementFinder(ISearchContext context, By by)
        {
            _context = context;
            _byExecutor = x => x.FindElements(by).ToArray();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Defines the interface used to search for elements</param>
        /// <param name="byExecutor">Provides a mechanism by which to find elements within a document</param>
        public ElementFinder(ISearchContext context, Func<ISearchContext, IWebElement[]> byExecutor)
        {
            _context = context;
            _byExecutor = byExecutor;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">IWebElement</param>
        public ElementFinder(IWebElement element)
            => _context = element;

        ElementFinder(ElementFinder innerFinder, int index)
        {
            _innerFinder = innerFinder;
            _index = index;
        }

        ElementFinder(ElementFinder innerFinder, By by)
        {
            _innerFinder = innerFinder;
            _byExecutor = x => x.FindElements(by).ToArray();
        }

        ElementFinder(ISearchContext context, Func<ISearchContext, IWebElement[]> byExecutor, ElementFinder innerFinder, int index, TimeSpan? timeout, Func<IWebElement, bool> isValidElement)
        {
            _context = context;
            _byExecutor = byExecutor;
            _innerFinder = innerFinder;
            _index = index;
            Timeout = timeout;
            _isValidElement = isValidElement;
        }

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method
        /// </summary>
        /// <returns>IWebElement</returns>
        public IWebElement Find()
        {
            if (!TestAssistantMode.IsCreatingMode)
            {
                if (Timeout.HasValue)
                {
                    var driver = GetWebDriver();
                    if (driver == null) return null;
                    return new WebDriverWait(driver, Timeout.Value).Until(_ => CheckValid(FindCore()));
                }
            }
            return FindCore();
        }

        IWebElement CheckValid(IWebElement element)
        {
            if (element == null) return null;
            if (_isValidElement == null) return element;
            return _isValidElement(element) ? element : null;
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
        /// <returns>ElementFinder.</returns>
        public ElementFinder Wait()
            => new ElementFinder(_context, _byExecutor, _innerFinder, _index, Settings.DefaultWaitTime, null);

        /// <summary>
        /// Add wait.
        /// </summary>
        /// <returns>ElementFinder.</returns>
        public ElementFinder Wait(TimeSpan timeout)
            => new ElementFinder(_context, _byExecutor, _innerFinder, _index, timeout, null);

        /// <summary>
        /// Add wait.
        /// </summary>
        /// <param name="isValidElement">Is it a valid element?</param>
        /// <returns>ElementFinder.</returns>
        public ElementFinder Wait(Func<IWebElement, bool> isValidElement)
            => new ElementFinder(_context, _byExecutor, _innerFinder, _index, Settings.DefaultWaitTime, isValidElement);

        /// <summary>
        /// Add wait.
        /// </summary>
        /// <param name="timeout">Waiting time.</param>
        /// <param name="isValidElement">Is it a valid element?</param>
        /// <returns>ElementFinder.</returns>
        public ElementFinder Wait(TimeSpan timeout, Func<IWebElement, bool> isValidElement)
            => new ElementFinder(_context, _byExecutor, _innerFinder, _index, timeout, isValidElement);

        IWebElement FindCore()
        {
            if (_innerFinder != null)
            {
                if (_byExecutor == null)
                {
                    var elements = _innerFinder.FindMany();
                    if (_index < elements.Length) return elements[_index];
                    return null;
                }
                else
                {
                    var innerElement = _innerFinder.Find();
                    if (innerElement == null) return null;
                    var elements = _byExecutor(innerElement);
                    if (elements.Length != 1) return null;
                    return elements[0];
                }
            }
            else
            {
                if (_byExecutor == null) return (IWebElement)_context;
                var elements = _byExecutor(_context);
                if (elements.Length != 1) return null;
                return elements[0];
            }
        }

        IWebElement[] FindMany()
        {
            if (_innerFinder != null)
            {
                if (_byExecutor == null)
                {
                    return new[] { _innerFinder.FindMany()[_index] };
                }
                else
                {
                    return _byExecutor(_innerFinder.Find()).ToArray();
                }
            }
            if (_byExecutor == null) return new[] { (IWebElement)_context };
            return _byExecutor(_context);
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

        /// <summary>
        /// Find Element by Content Text
        /// </summary>
        /// <param name="containerTagName">TagName</param>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public ElementFinder ByText(string containerTagName, string text) =>
            ByXPath($".//{containerTagName}[normalize-space(text())='{text.Trim()}']");

        /// <summary>
        /// Find Element by Content Text
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns></returns>
        public ElementFinder ByText(string text) => ByText("*", text);
    }
}
