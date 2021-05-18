using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.StandardControls;
using Test.PageObjects;

namespace Test
{
    [TestClass]
    public class TestFindNext
    {
        ChromeDriver _driver;
        FindNextHtmlPage _page;
        private IWebElement _anchorElement;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();
            _page = FindNextHtmlPage.Open(_driver);
            _anchorElement = _page.ById("td_address").Find();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void ById()
        {
            var e1 = _anchorElement.FindNextElement(By.Id("email"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.Id("tel"));
            Assert.IsNotNull(e2);
            new TextBoxDriver(e2).Edit("09012345678");
            Assert.AreEqual("09012345678", _page.Tel.Text);
        }
        [TestMethod]
        public void ByName()
        {
            var e1 = _anchorElement.FindNextElement(By.Name("email"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.Name("tel"));
            Assert.IsNotNull(e2);
            new TextBoxDriver(e2).Edit("09012345678");
            Assert.AreEqual("09012345678", _page.Tel.Text);
        }
        [TestMethod]
        public void ByClassName()
        {
            var e1 = _anchorElement.FindNextElement(By.ClassName("element-before"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.ClassName("element-after"));
            new TextBoxDriver(e2).Edit("09012345678");
            Assert.AreEqual("09012345678", _page.Tel.Text);
        }
        [TestMethod]
        public void ByCssSelector()
        {
            var e1 = _anchorElement.FindNextElement(By.CssSelector("input[type='email']"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.CssSelector("input[type='tel']"));
            new TextBoxDriver(e2).Edit("09012345678");
            Assert.AreEqual("09012345678", _page.Tel.Text);
        }
        [TestMethod]
        public void ByTagName()
        {
            var e1 = _anchorElement.FindNextElement(By.TagName("x-element"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.TagName("input"));
            Assert.IsNotNull(e2);
            new TextBoxDriver(e2).Edit("Tokyo, Shinjuku, 1-1");
            Assert.AreEqual("Tokyo, Shinjuku, 1-1", _page.Address.Text);
        }
        [TestMethod]
        public void XPath()
        {
            var e1 = _anchorElement.FindNextElement(By.XPath("//input[@type='email']"));
            Assert.IsNull(e1);
            var e2 = _anchorElement.FindNextElement(By.XPath("//input[@type='tel']"));
            new TextBoxDriver(e2).Edit("09012345678");
            Assert.AreEqual("09012345678", _page.Tel.Text);
        }
        [TestMethod]
        public void LinkText()
        {
            var anchorElement = _page.ById("td_link2").Find();
            var e1 = anchorElement.FindNextElement(By.LinkText("Link1"));
            Assert.IsNull(e1);
            var e2 = anchorElement.FindNextElement(By.LinkText("Link3"));
            Assert.IsNotNull(e2);
            Assert.AreEqual(_page.Link3.Element, e2);
        }
        [TestMethod]
        public void PartialLinkText()
        {
            var anchorElement = _page.ById("td_link2").Find();
            var e1 = anchorElement.FindNextElement(By.PartialLinkText("1"));
            Assert.IsNull(e1);
            var e2 = anchorElement.FindNextElement(By.PartialLinkText("3"));
            Assert.IsNotNull(e2);
            Assert.AreEqual(_page.Link3.Element, e2);
        }
    }
}
