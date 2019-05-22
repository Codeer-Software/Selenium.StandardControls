using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.IO;
using Test;

namespace Test
{
    [TestClass]
    public class CaptureTest
    {
        OpenQA.Selenium.Chrome.ChromeDriver _driver;

        void Test()
        {
            _driver.Url = "file:///C:/tfs/codeer/Codeer.TestAssistantPro/Product_201805/Product/UnitTest/Controls.html";
            var controlsHtmlPage = _driver.AttachControlsHtml();
            controlsHtmlPage.TextBox_Name.Edit("aaa");
        }
    }
}
