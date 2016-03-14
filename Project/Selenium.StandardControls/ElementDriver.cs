namespace Selenium.StandardControls
{
    /// <summary>
    /// JavaScript Element Driver
    /// </summary>
    public class ElementDriver
    {
        private IElementCore Core { get; }

        public string InnerHtml => GetAttribute<string>("innerHTML");
        public string InnerText => GetAttribute<string>("innerText");
        public string Text => GetAttribute<string>("text");
        public string Value => GetAttribute<string>("value");
        public string CssClass => GetCssValue("className");
        public string Width => GetCssValue("width");
        public string Height => GetCssValue("height");
        public string FontSize => GetCssValue("fontSize");
        public string Font => GetCssValue("fontFamily");

        //ToDo: bold = Firefox is 700. Other Browther is un know.
        public bool FontBold => GetCssValue("fontWeight") == "700" || GetCssValue("fontWeight") == "bold";

        public bool FontItalic => GetCssValue("fontStyle") == "italic";
        public bool TextUnderline => GetCssValue("textDecoration").Contains("underline");
        public bool TextLineThrough => GetCssValue("textDecoration").Contains("line-through");
        public string Color => GetCssValue("color");
        public string BackGroundColor => GetCssValue("backgroundColor");
        public string BackGroundImage => GetCssValue("backgroundImage");
        public long TabIndex => GetAttribute<long>("tabIndex");
        public string ImeMode => GetCssValue("imeMode");
        public int? MaxLength => GetAttribute<int?>("maxLength");
        public string TextAlign => GetCssValue("textAlign");


        public ElementDriver(IElementCore core)
        {
            Core = core;
        }

        private T GetAttribute<T>(string name) => Core.GetAttribute<T>(name);

        private string GetCssValue(string name) => Core.GetCssValue(name);

    }
}