using OpenQA.Selenium;
using Selenium.StandardControls;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class ControlDriverTypeSelector : IControlDriverTypeSelector
    {
        public int Priority => 0;

        public string GetControlDriverType(IWebElement element)
        {
            if (element == null) return string.Empty;

            var elementInfo = new ElementInfo(element);
            switch (element.TagName)
            {
                case "a":
                    return typeof(AnchorDriver).FullName;
                case "button":
                    return typeof(ButtonDriver).FullName;
                case "input":
                    switch (element.GetAttribute("type"))
                    {
                        case "submit": return typeof(ButtonDriver).FullName;
                        default: return typeof(TextBoxDriver).FullName;
                    }
                case "textarea": return typeof(TextAreaDriver).FullName;
                case "label":
                    return typeof(LabelDriver).FullName;
                case "tbody":
                    return typeof(ItemsControlDriver<>).FullName.Replace("`1", "<T>");
                case "select":
                    return typeof(DropDownListDriver).FullName;
            }
            return string.Empty;
        }
    }
}
