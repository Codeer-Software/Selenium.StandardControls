using OpenQA.Selenium;

namespace Selenium.StandardControls.PageObjectUtility
{
    public class ElementFinder
    {
        ISearchContext _context;
        By _by;
        public ElementFinder(ISearchContext context, By by)
        {
            _context = context;
            _by = by;
        }
        public IWebElement Find() => _context.FindElement(_by);
    }
}
