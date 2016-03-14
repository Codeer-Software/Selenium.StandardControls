namespace Selenium.StandardControls
{
    /// <summary>
    /// JavaScript Element Driver
    /// </summary>
    public class ElementDriver
    {
        #region Properties

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
        public string FontBold => (GetCssValue("fontWeight") == "700" || GetCssValue("fontWeight") == "bold").ToString();

        public string FontItalic => (GetCssValue("fontStyle") == "italic").ToString();
        public string TextUnderline => (GetCssValue("textDecoration").Contains("underline")).ToString();
        public string TextLineThrough => (GetCssValue("textDecoration").Contains("line-through")).ToString();
        public string Color => GetCssValue("color");
        public string BackGroundColor => GetCssValue("backgroundColor");
        public string BackGroundImage => GetCssValue("backgroundImage");
        public long TabIndex => GetAttribute<long>("tabIndex");
        public string ImeMode => GetCssValue("imeMode");
        public int? MaxLength => GetAttribute<int?>("maxLength");
        public string TextAlign => GetCssValue("textAlign");

        #endregion Properties

        #region Constructors

        public ElementDriver(IElementCore core)
        {
            Core = core;
        }

        #endregion Constructors

        #region Methods
        public string SetAttribute(string attribute) => GetAttribute<string>(attribute);
        #endregion Methods

        #region Private Methods

        private T GetAttribute<T>(string name) => Core.GetAttribute<T>(name);

        private string GetCssValue(string name) => Core.GetCssValue(name);

        #endregion Private Methods
    }
}