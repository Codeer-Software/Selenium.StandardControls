using System.IO;
using OpenQA.Selenium;
using Selenium.StandardControls;
using Selenium.StandardControls.PageObjectUtility;
using Selenium.StandardControls.TestAssistant.GeneratorToolKit;

namespace Test.PageObjects
{
    public class FindNextHtmlPage : PageBase
    {
        public TextBoxDriver Email => ById("email").Wait();
        public TextBoxDriver Address => ById("address").Wait();
        public TextBoxDriver Tel => ById("tel").Wait();
        public AnchorDriver Link1 => ById("link1").Wait();
        public AnchorDriver Link2 => ById("link2").Wait();
        public AnchorDriver Link3 => ById("link3").Wait();

        public FindNextHtmlPage(IWebDriver driver) : base(driver) { }

        public static FindNextHtmlPage Open(IWebDriver driver)
        {
            driver.Url = Path.GetFullPath("../../FindNext.html");
            return new FindNextHtmlPage(driver);
        }
    }

    public static class FindNextHtmlPageExtensions
    {
        [PageObjectIdentify(UrlComapreType.Contains, "FindNext.html")]
        public static FindNextHtmlPage AttachControlsHtml(this IWebDriver driver) => new FindNextHtmlPage(driver);
    }
}
