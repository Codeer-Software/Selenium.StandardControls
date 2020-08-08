using OpenQA.Selenium;

namespace Selenium.StandardControls.PageObjectUtility
{
    public abstract class MappingBase
    {
        protected ISearchContext SearchContext { get; }

        /// <summary>
        /// Find Element in ClassName
        /// </summary>
        /// <param name="classNameToFind">ClassName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByClassName(string classNameToFind) => new ElementFinder(SearchContext, By.ClassName(classNameToFind));
        /// <summary>
        /// Find Element in CssSelector
        /// </summary>
        /// <param name="cssSelectorToFind">CssSelector</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByCssSelector(string cssSelectorToFind) => new ElementFinder(SearchContext, By.CssSelector(cssSelectorToFind));
        /// <summary>
        /// Find Element in Id
        /// </summary>
        /// <param name="idToFind">ElementFinder</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ById(string idToFind) => new ElementFinder(SearchContext, By.Id(idToFind));
        /// <summary>
        /// Find Element in LinkText
        /// </summary>
        /// <param name="linkTextToFind">LinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByLinkText(string linkTextToFind) => new ElementFinder(SearchContext, By.LinkText(linkTextToFind));
        /// <summary>
        /// Find Element in ByName
        /// </summary>
        /// <param name="nameToFind">ByName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByName(string nameToFind) => new ElementFinder(SearchContext, By.Name(nameToFind));
        /// <summary>
        /// Find Element in PartialLinkText
        /// </summary>
        /// <param name="partialLinkTextToFind">PartialLinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByPartialLinkText(string partialLinkTextToFind) => new ElementFinder(SearchContext, By.PartialLinkText(partialLinkTextToFind));
        /// <summary>
        /// Find Element in TagName
        /// </summary>
        /// <param name="tagNameToFind">TagName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByTagName(string tagNameToFind) => new ElementFinder(SearchContext, By.TagName(tagNameToFind));

        /// <summary>
        /// Find Element in XPath
        /// </summary>
        /// <param name="xpathToFind">XPath</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByXPath(string xpathToFind) => new ElementFinder(SearchContext, By.XPath(xpathToFind));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchContext">SearchContext.</param>
        public MappingBase(ISearchContext searchContext) => SearchContext = searchContext;
    }

    /// <summary>
    /// Html of information acquisition base
    /// </summary>
    public abstract class PageBase : MappingBase
    {
        /// <summary>
        /// Driver to generate a page
        /// </summary>
        public IWebDriver Driver { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver">Driver to generate a page</param>
        protected PageBase(IWebDriver driver) :base(driver)
        {
            Driver = driver;
        }
    }

    /// <summary>
    /// Html of information acquisition base
    /// </summary>
    public abstract class ComponentBase : MappingBase
    {
        /// <summary>
        /// Driver to generate a page
        /// </summary>
        public IWebElement Element { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="element">Element of Component.</param>
        protected ComponentBase(IWebElement element) : base(element)
        {
            Element = element;
        }
    }
}
