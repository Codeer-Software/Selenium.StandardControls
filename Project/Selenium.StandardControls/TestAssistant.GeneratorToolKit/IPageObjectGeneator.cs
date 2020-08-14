using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public interface IPageObjectGeneator
    {
        int Priority { get; }

        Dictionary<string, Action> GetGridMenu(GridSelectType gridSelectType, PageObjectPropertyInfo propertyInfo);

        IdentifyInfo[] GetIdentifyingCandidates(ISearchContext serachContext, IWebElement element);

        PageObjectGenerateResult GeneratePageObjectCode(PageObjectCodeInfo info);

        PageObjectGenerateResult GenerateComponetObjectCode(ComponetObjectCodeInfo info);
    }

    public class PageObjectCodeInfo
    {
        public IWebDriver Driver { get; set; }
        public PageIdentifyInfo PageIdentifyInfo { get; set; }
        public string Name { get; set; }
        public PageObjectPropertyInfo[] Properties { get; set; }
    }
    public class ComponetObjectCodeInfo
    {
        public IWebDriver Driver { get; set; }
        public TargetElementInfo TargetElementInfo { get; set; }
        public IWebElement ComponentElement { get; set; }
        public string Name { get; set; }
        public PageObjectPropertyInfo[] Properties { get; set; }
    }
}
