using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium.StandardControls;
using System;
using OpenQA.Selenium.Chrome;

namespace Test
{
    [TestClass]
    public class TestControlsWait
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
        public void TextBoxShowWait()
        {
            var textbox = new TextBoxDriver(_driver.FindElement(By.Id("textBoxName")), () => WaitForSuccess(() => _page.Button_JS.Show()));
            textbox.Edit("abc");
            textbox.Text.Is("abc");
        }

        [TestMethod]
        public void Radio()
        {
            var radioMan = new RadioButtonDriver(_driver.FindElement(By.Id("radioMan")), () => WaitForSuccess(() => _page.Button_JS.Show()));
            var radioWoman = new RadioButtonDriver(_driver.FindElement(By.Id("radioWoman")), () => WaitForSuccess(() => _page.Button_JS.Show()));

            radioMan.Checked.IsTrue();
            radioWoman.Edit();
            radioMan.Checked.IsFalse();
            radioWoman.Checked.IsTrue();
        }

        [TestMethod]
        public void CheckBox()
        {
            var check = new CheckBoxDriver(_driver.FindElement(By.Id("checkBoxCellPhone")), () => WaitForSuccess(() => _page.Button_JS.Show()));
            check.Edit(true);
            check.Checked.IsTrue();
            check.Edit(false);
            check.Checked.IsFalse();
            check.Edit(true);
            check.Checked.IsTrue();
        }

        [TestMethod]
        public void DropDownList()
        {
            var dropDown = new DropDownListDriver(_driver.FindElement(By.Id("dropDownFruit")), () => WaitForSuccess(() => _page.Button_JS.Show()));
            dropDown.Edit("Apple");
            dropDown.SelectedIndex.Is(0);
            dropDown.Edit("Orange");
            dropDown.SelectedIndex.Is(1);
            dropDown.Edit(3);
            dropDown.SelectedIndex.Is(3);
            dropDown.Text.Is("Pinapple");
        }

        [TestMethod]
        public void Button()
        {
            var button = new ButtonDriver(_driver.FindElement(By.Id("inputJS")), () => WaitForSuccess(() => _page.DropDown_Fruit.Show()));
            button.Invoke();
            _page.TextBox_Name.Text.Is("JS");
        }

        [TestMethod]
        public void Anchor()
        {
            var anchor = new AnchorDriver(_driver.FindElement(By.Id("codeer")), () => WaitForSuccess(()=>_driver.Url.Is("http://www.codeer.co.jp/")));
            anchor.Invoke();
        }

        void WaitForSuccess(Action a)
        {
            while (true)
            {
                try
                {
                    a();
                    break;
                }
                catch { }
            }
        }
    }
}
