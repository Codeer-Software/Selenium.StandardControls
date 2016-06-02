using OpenQA.Selenium;

namespace Selenium.StandardControls.PageObjectUtility
{
    /// <summary>
    /// Html of information acquisition base
    /// </summary>
    public abstract class PageBase
    {
        /// <summary>
        /// Driver to generate a page
        /// </summary>
        public IWebDriver Driver { get; }
        /// <summary>
        /// Find Element in ClassName
        /// </summary>
        /// <param name="classNameToFind">ClassName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByClassName(string classNameToFind) => new ElementFinder(Driver, By.ClassName(classNameToFind));
        /// <summary>
        /// Find Element in CssSelector
        /// </summary>
        /// <param name="cssSelectorToFind">CssSelector</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByCssSelector(string cssSelectorToFind) => new ElementFinder(Driver, By.CssSelector(cssSelectorToFind));
        /// <summary>
        /// Find Element in Id
        /// </summary>
        /// <param name="idToFind">ElementFinder</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ById(string idToFind) => new ElementFinder(Driver, By.Id(idToFind));
        /// <summary>
        /// Find Element in LinkText
        /// </summary>
        /// <param name="linkTextToFind">LinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByLinkText(string linkTextToFind) => new ElementFinder(Driver, By.LinkText(linkTextToFind));
        /// <summary>
        /// Find Element in ByName
        /// </summary>
        /// <param name="nameToFind">ByName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByName(string nameToFind) => new ElementFinder(Driver, By.Name(nameToFind));
        /// <summary>
        /// Find Element in PartialLinkText
        /// </summary>
        /// <param name="partialLinkTextToFind">PartialLinkText</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByPartialLinkText(string partialLinkTextToFind) => new ElementFinder(Driver, By.PartialLinkText(partialLinkTextToFind));
        /// <summary>
        /// Find Element in TagName
        /// </summary>
        /// <param name="tagNameToFind">TagName</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByTagName(string tagNameToFind) => new ElementFinder(Driver, By.TagName(tagNameToFind));
        /// <summary>
        /// Find Element in XPath
        /// </summary>
        /// <param name="xpathToFind">XPath</param>
        /// <returns>ElementFinder</returns>
        public ElementFinder ByXPath(string xpathToFind) => new ElementFinder(Driver, By.XPath(xpathToFind));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_driver">Driver to generate a page</param>
        protected PageBase(IWebDriver _driver)
        {
            Driver = _driver;
        }
    }
}
