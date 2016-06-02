using OpenQA.Selenium;

namespace Selenium.StandardControls.PageObjectUtility
{
    /// <summary>
    /// Find Element
    /// </summary>
    public class ElementFinder
    {
        ISearchContext _context;
        By _by;

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

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method
        /// </summary>
        /// <returns></returns>
        public IWebElement Find() => _context.FindElement(_by);
    }
}
