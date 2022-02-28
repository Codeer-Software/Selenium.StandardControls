using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium.StandardControls;
using System.IO;
using Test.PageObjects;

namespace Test
{

    [TestClass]
    public class TestFindByText
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
        public void ByText()
        {
            LabelDriver label = _page.ByText("Name：");
            CheckBoxDriver checkBox = _page.ByText("Cell phone");
            ButtonDriver button = _page.ByText("JS");
        }
    }
}
