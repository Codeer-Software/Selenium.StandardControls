using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Selenium.StandardControls;

namespace Test
{
    [TestClass]
    public class ElementDriverTest
    {
      //  [TestMethod]
        public void FirefoxDriverElementTest()
        {
            using (var firefoxDriver = new FirefoxDriver())
            {
                firefoxDriver.Url = Path.GetFullPath("../../../Test/index.html");
                var element1 = firefoxDriver.FindElement(By.Id("ptest1"));

                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("auto");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");

                var element2 = firefoxDriver.FindElement(By.Id("ptest2"));
                var driver2 = new ElementInfo(element2);
                driver2.FontBold.IsFalse();
                driver2.FontItalic.IsFalse();
                driver2.TextUnderline.IsFalse();
                driver2.TextLineThrough.IsFalse();
                driver2.Class.Is("exampleFalse");
                driver2.ImeMode.Is("disabled");
                driver2.Color.Is("rgba(0, 0, 0, 1)");
                driver2.BackGroundColor.Is("rgba(153, 204, 0, 1)");
                driver2.TextAlign.Is("right");
                driver2.Font.Is("Impact,Charcoal");
                driver2.BackGroundImage.Is("none");

                var element3 = firefoxDriver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = firefoxDriver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }

        [TestMethod]
        public void ChromeDriverElementTest()
        {
            using (var chromeDriver = new ChromeDriver())
            {
                chromeDriver.Url = Path.GetFullPath("../../../Test/index.html");
                var element1 = chromeDriver.FindElement(By.Id("ptest1"));
                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");

                var element2 = chromeDriver.FindElement(By.Id("ptest2"));
                var driver2 = new ElementInfo(element2);
                driver2.FontBold.IsFalse();
                driver2.FontItalic.IsFalse();
                driver2.TextUnderline.IsFalse();
                driver2.TextLineThrough.IsFalse();
                driver2.Class.Is("exampleFalse");
                driver2.ImeMode.Is("");
                driver2.Color.Is("rgba(0, 0, 0, 1)");
                driver2.BackGroundColor.Is("rgba(153, 204, 0, 1)");
                driver2.TextAlign.Is("right");
                driver2.Font.Is("Impact, Charcoal");
                driver2.BackGroundImage.Is("none");

                var element3 = chromeDriver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = chromeDriver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }

        //[TestMethod]
        public void InternetExplorerDriverElementTest()
        {
            var options = new InternetExplorerOptions()
            {
                IgnoreZoomLevel = true,
            };
            using (var internetExplorerDriver = new InternetExplorerDriver(options))
            {
                internetExplorerDriver.Url = Path.GetFullPath("../../../Test/index.html");
                var element1 = internetExplorerDriver.FindElement(By.Id("ptest1"));
                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("auto");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");

                var element2 = internetExplorerDriver.FindElement(By.Id("ptest2"));
                var driver2 = new ElementInfo(element2);
                driver2.FontBold.IsFalse();
                driver2.FontItalic.IsFalse();
                driver2.TextUnderline.IsFalse();
                driver2.TextLineThrough.IsFalse();
                driver2.Class.Is("exampleFalse");
                driver2.ImeMode.Is("disabled");
                driver2.Color.Is("rgba(0, 0, 0, 1)");
                driver2.BackGroundColor.Is("rgba(153, 204, 0, 1)");
                driver2.TextAlign.Is("right");
                driver2.Font.Is("impact,charcoal");
                driver2.BackGroundImage.Is("none");

                var element3 = internetExplorerDriver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = internetExplorerDriver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }
    }
}