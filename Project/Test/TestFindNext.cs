using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Selenium.StandardControls;
using Test.PageObjects;

namespace Test
{
    [TestClass]
    public class TestFindNext
    {
        ChromeDriver _driver;
        ControlsHtmlPage _page;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();
            _page = ControlsHtmlPage.Open(_driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void Test1()
        {
            _page.Label_Title.Text.Is("Title Controls");
            var x = _page.CheckBox_Cottage.Element.FindNextElement(By.Id("codeer"));

        }
    }
}
