using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Selenium.StandardControls
{
    public class ElementDriver
    {
        public IWebDriver Driver { get; protected set; }
        public IJavaScriptExecutor Js => (IJavaScriptExecutor)Driver;

        private enum ElementType
        {
            Script,
            WebElement,
        }
        private string Script { get; }
        private IWebElement Element { get; }
        private ElementType Type { get; }


        public const string VarName = "element";
        //ここに渡すscriptは下記のようにget_element();をVarNameで受けるのが必須条件。
        //$"var {VarName} = ....get_element();"
        public ElementDriver(IWebDriver driver, string script)
        {
            Driver = driver;
            Script = script;
            Type = ElementType.Script;
        }

        public ElementDriver(IWebDriver driver, IWebElement element)
        {
            Driver = driver;
            Element = element;
            Type = ElementType.WebElement;
        }

        public string InnerHtml => GetElement<string>("innerHTML");
        public string InnerText => GetElement<string>("innerText");
        public string Text => GetElement<string>("text");
        public string Value => GetElement<string>("value");
        public object ClassName => GetElementStyle("className");
        public string Width => GetElementStyle("width");
        public string Height => GetElementStyle("height");
        public string FonsSize => GetElementStyle("fontSize");
        public string Font => GetElementStyle("fontFamily");
        //ToDo:太字はFirefoxでは700だったけ全部そうなのかな？boldもありそう。未確認
        public bool FontBold => GetElementStyle("fontWeight") == "700" || GetElementStyle("fontWeight") == "bold";
        public bool FontItalic => GetElementStyle("fontStyle") == "italic";
        public bool TextUnderline => GetElementStyle("textDecoration").Contains("underline");
        public bool TextLineThrough => GetElementStyle("textDecoration").Contains("line-through");
        public string Color => GetElementStyle("color");
        public string BackGroundColor => GetElementStyle("backgroundColor");
        public string BackGroundImage => GetElementStyle("backgroundImage");
        public long TabIndex => GetElement<long>("tabIndex");
        public string ImeMode => GetElementStyle("imeMode");
        public int? MaxLength => GetElement<int?>("maxLength");
        public string TextAlign => GetElementStyle("textAlign");

        public void MouseOver()
        {
            switch (Type)
            {
                case ElementType.WebElement:
                    var action = new Actions(Driver);
                    action.MoveToElement(Element).Perform();
                    break;

                default:
                    throw new ArgumentOutOfRangeException("");
            }
        }


        private T GetElement<T>(string name)
        {
            switch (Type)
            {
                case ElementType.Script:
                    var script = $"{Script}return {VarName}.{name}";
                    return (T)Js.ExecuteScript(script);

                case ElementType.WebElement:
                    var o = Element.GetAttribute(name);
                    if (typeof(T) == typeof(int?)) return (o == null) ? default(T) : (T)(object)int.Parse(o);
                    if (typeof(T) == typeof(string)) return (T)(object)o;
                    if (typeof(T) == typeof(long)) return (T)(object)long.Parse(o);
                    throw new ArgumentOutOfRangeException("");

                default:
                    throw new ArgumentOutOfRangeException("");
            }
        }

        private string GetElementStyle(string name)
        {
            switch (Type)
            {
                case ElementType.Script:
                    var script = $"{Script}return ({VarName}.currentStyle || document.defaultView.getComputedStyle({VarName}, null)).{name};";
                    return (string)Js.ExecuteScript(script);

                case ElementType.WebElement:
                    return Element.GetCssValue(name);

                default:
                    throw new ArgumentOutOfRangeException("");
            }
        }
    }
}
