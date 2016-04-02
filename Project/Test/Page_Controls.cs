using Selenium.StandardControls;
using System.IO;
using Selenium.StandardControls.PageObjectUtility;
using OpenQA.Selenium;

namespace Test
{
    class Page_Controls : PageBase
    {
        public LabelDriver Label_Title => ById("labelTitle");
        public TextBoxDriver TextBox_Name => ById("textBoxName");
        public RadioButtonDriver Radio_Man => ById("radioMan");
        public RadioButtonDriver Radio_Woman => ById("radioWoman");
        public CheckBoxDriver CheckBox_CellPhone => ById("checkBoxCellPhone");
        public CheckBoxDriver CheckBox_Car => ById("checkBoxCar");
        public CheckBoxDriver CheckBox_Cottage => ById("checkBoxCottage");
        public DropDownListDriver DropDown_Fruit => ById("dropDownFruit");
        public TextAreaDriver TextArea_Freeans => ById("textareaFreeans");
        public ButtonDriver Button_JS => ById("inputJS");

        public Page_Controls(IWebDriver driver) : base(driver) { }

        public static Page_Controls Open(IWebDriver driver)
        {
            driver.Url = Path.GetFullPath("../../Controls.html");
            return new Page_Controls(driver);
        }
    }
}
