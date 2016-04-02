using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;

namespace Test
{
    [TestClass]
    public class TestControls
    {
        FirefoxDriver _driver;
        Page_Controls _page;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new FirefoxDriver();
            _page = Page_Controls.Open(_driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void Label()
        {
            _page.Label_Title.Text.Is("Title Controls");
        }

        [TestMethod]
        public void TextBox()
        {
            _page.TextBox_Name.Edit("abc");
            _page.TextBox_Name.Text.Is("abc");
        }

        [TestMethod]
        public void Radio()
        {
            _page.Radio_Man.Checked.IsTrue();
            _page.Radio_Woman.Edit();
            _page.Radio_Man.Checked.IsFalse();
            _page.Radio_Woman.Checked.IsTrue();
        }

        [TestMethod]
        public void CheckBox()
        {
            _page.CheckBox_CellPhone.Edit(true);
            _page.CheckBox_CellPhone.Checked.IsTrue();
            _page.CheckBox_CellPhone.Edit(false);
            _page.CheckBox_CellPhone.Checked.IsFalse();
            _page.CheckBox_CellPhone.Edit(true);
            _page.CheckBox_CellPhone.Checked.IsTrue();
        }

        [TestMethod]
        public void DropDownList()
        {
            _page.DropDown_Fruit.Edit("Apple");
            _page.DropDown_Fruit.SelectedIndex.Is(0);
            _page.DropDown_Fruit.Edit("Orange");
            _page.DropDown_Fruit.SelectedIndex.Is(1);
            _page.DropDown_Fruit.Edit(3);
            _page.DropDown_Fruit.SelectedIndex.Is(3);
            _page.DropDown_Fruit.Text.Is("Pinapple");
        }

        [TestMethod]
        public void TextArea()
        {
            _page.TextArea_Freeans.Edit("abc");
            _page.TextArea_Freeans.Text.Is("abc");
        }

        [TestMethod]
        public void Button()
        {
            _page.Button_JS.Click();
            _page.TextBox_Name.Text.Is("JS");
        }

        [TestMethod]
        public void Anchor()
        {
            _page.A_Codeer.Text.Is("codeer");
            _page.A_Codeer.Click();
            _page.Driver.Url.Is("http://www.codeer.co.jp/");
        }
    }
}
