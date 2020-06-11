using Selenium.StandardControls;
using System.IO;
using Selenium.StandardControls.PageObjectUtility;
using OpenQA.Selenium;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Test.PageObjects
{
    public class ControlsHtmlPage : PageBase
    {
        public LabelDriver Label_Title => ById("labelTitle");
        public TextBoxDriver TextBox_Name => ById("textBoxName");
        public DateDriver Date => ById("date");
        public RadioButtonDriver Radio_Man => ById("radioMan");
        public RadioButtonDriver Radio_Woman => ById("radioWoman");
        public CheckBoxDriver CheckBox_CellPhone => ById("checkBoxCellPhone");
        public CheckBoxDriver CheckBox_Car => ById("checkBoxCar");
        public CheckBoxDriver CheckBox_Cottage => ById("checkBoxCottage");
        public DropDownListDriver DropDown_Fruit => ById("dropDownFruit");
        public DropDownListDriver DropDownFruitValue => ById("dropDownFruitValue");
        public TextAreaDriver TextArea_Freeans => ById("textareaFreeans");
        public ButtonDriver Button_JS => ById("inputJS");
        public AnchorDriver A_Codeer => ById("codeer");

        public ControlsHtmlPage(IWebDriver driver) : base(driver) { }

        public static ControlsHtmlPage Open(IWebDriver driver)
        {
            driver.Url = Path.GetFullPath("../../Controls.html");
            return new ControlsHtmlPage(driver);
        }
    }

    public static class ControlsHtmlPageExtensions
    {
        [PageObjectIdentify("Controls.html", UrlComapreType.Contains)]
        public static ControlsHtmlPage AttachControlsHtml(this IWebDriver driver) => new ControlsHtmlPage(driver);
    }
}
