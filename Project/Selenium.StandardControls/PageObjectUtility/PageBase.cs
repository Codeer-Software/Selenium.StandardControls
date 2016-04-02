using OpenQA.Selenium;

namespace Selenium.StandardControls.PageObjectUtility
{
    public abstract class PageBase
    {
        public IWebDriver Driver { get; }
        public ElementFinder ByClassName(string classNameToFind) => new ElementFinder(Driver, By.ClassName(classNameToFind));
        public ElementFinder ByCssSelector(string cssSelectorToFind) => new ElementFinder(Driver, By.CssSelector(cssSelectorToFind));
        public ElementFinder ById(string idToFind) => new ElementFinder(Driver, By.Id(idToFind));
        public ElementFinder ByLinkText(string linkTextToFind) => new ElementFinder(Driver, By.LinkText(linkTextToFind));
        public ElementFinder ByName(string nameToFind) => new ElementFinder(Driver, By.Name(nameToFind));
        public ElementFinder ByPartialLinkText(string partialLinkTextToFind) => new ElementFinder(Driver, By.PartialLinkText(partialLinkTextToFind));
        public ElementFinder ByTagName(string tagNameToFind) => new ElementFinder(Driver, By.TagName(tagNameToFind));
        public ElementFinder ByXPath(string xpathToFind) => new ElementFinder(Driver, By.XPath(xpathToFind));

        protected PageBase(IWebDriver _driver)
        {
            Driver = _driver;
        }
    }
}
