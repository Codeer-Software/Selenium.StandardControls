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
        [TestMethod]
        public void FirefoxDriverElementTest()
        {
            using (var _driver = new FirefoxDriver())
            {
                _driver.Url = Path.GetFullPath("../../../Test/index.html");
                var element1 = _driver.FindElement(By.Id("ptest1"));

                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("auto");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");
                driver.FontSize.Is("19.2px");
                driver.Font.Is("\"ＭＳ ゴシック\",sans-serif");
                //driver.BackGroundImage.Is("url(\"file:///C:/Work/Oss/Selenium.StandardControls/Project/Test/test.jpg\")");
               // driver.Width.Is("1388.77px");
                //driver.Height.Is("19.8333px");

                var element2 = _driver.FindElement(By.Id("ptest2"));
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
                driver2.FontSize.Is("86.2px");
                driver2.Font.Is("Impact,Charcoal");
                driver2.BackGroundImage.Is("none");
                //driver2.Width.Is("1388.77px");
                //driver2.Height.Is("105.967px");

                var element3 = _driver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = _driver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }

        [TestMethod]
        public void ChromeDriverElementTest()
        {
            using (var _driver = new ChromeDriver())
            {
                _driver.Url = Path.GetFullPath("../../../Test/index.html");
                var element1 = _driver.FindElement(By.Id("ptest1"));
                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");
                driver.FontSize.Is("19.2px");
                driver.Font.Is("'ＭＳ ゴシック', sans-serif");
                //driver.BackGroundImage.Is("url(\"file:///C:/Work/Oss/Selenium.StandardControls/Project/Test/test.jpg\")");
                //driver.Width.Is("914px");
                //driver.Height.Is("20px");

                var element2 = _driver.FindElement(By.Id("ptest2"));
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
                driver2.FontSize.Is("86.25px");
                driver2.Font.Is("Impact, Charcoal");
                driver2.BackGroundImage.Is("none");
                //driver2.Width.Is("914px");
                //driver2.Height.Is("105px");

                var element3 = _driver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = _driver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }

        [TestMethod]
        public void InternetExplorerDriverElementTest()
        {
            using (var _driver = new InternetExplorerDriver())
            {
                _driver.Url = "http://localhost/ElementInfoTest.html";
                var element1 = _driver.FindElement(By.Id("ptest1"));
                var driver = new ElementInfo(element1);
                driver.FontBold.IsTrue();
                driver.FontItalic.IsTrue();
                driver.TextLineThrough.IsTrue();
                driver.Class.Is("exampleTrue");
                driver.ImeMode.Is("auto");
                driver.Color.Is("rgba(153, 204, 0, 1)");
                driver.BackGroundColor.Is("rgba(0, 0, 0, 1)");
                driver.TextAlign.Is("left");
                driver.FontSize.Is("19.2px");
                driver.Font.Is("\"ＭＳ ゴシック\",sans-serif");
                //driver.BackGroundImage.Is("url(\"http://localhost/test.jpg\")");
                //driver.Width.Is("1806px");
                //driver.Height.Is("19.2px");

                var element2 = _driver.FindElement(By.Id("ptest2"));
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
                driver2.FontSize.Is("85.6px");
                driver2.Font.Is("impact,charcoal");
                driver2.BackGroundImage.Is("none");
                //driver2.Width.Is("1806px");
                //driver2.Height.Is("104.41px");

                var element3 = _driver.FindElement(By.Id("font1"));
                var driver3 = new ElementInfo(element3);
                driver3.Color.Is("rgba(0, 0, 255, 1)");

                var element4 = _driver.FindElement(By.Id("text1"));
                var driver4 = new ElementInfo(element4);
                driver4.MaxLength.Is(10);
                driver4.TabIndex.Is(2);
            }
        }
    }
}