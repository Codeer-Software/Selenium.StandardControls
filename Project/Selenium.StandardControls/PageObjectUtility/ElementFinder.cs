using OpenQA.Selenium;
using System.Collections.ObjectModel;
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
            if (_innerFinder != null) return _innerFinder.FindMany()[_index];
            return _context.FindElement(_by);
        }

        IWebElement[] FindMany()
        {
            if (_innerFinder != null) return new[] { _innerFinder.FindMany()[_index] };
            return _context.FindElements(_by).ToArray();
        }

        public ElementFinder this[int index]
        {
            get
            {
                return new ElementFinder(this, index);
            }
        }
    }
}
