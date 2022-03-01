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
            LabelDriver checkBoxLabel = _page.ByText("Cell phone");
            CheckBoxDriver checkBox = checkBoxLabel.Element.FindNextElement(By.TagName("input")).Convert();
            checkBox.Edit(true);
        }

        [TestMethod]
        public void ByTextFromElement()
        {
            var body = _page.ByTagName("body").Wait();

            LabelDriver label = body.ByText("Name：");
            LabelDriver checkBoxLabel = body.ByText("Cell phone");
            CheckBoxDriver checkBox = checkBoxLabel.Element.FindNextElement(By.TagName("input")).Convert();
            checkBox.Edit(true);
        }
    }
}
