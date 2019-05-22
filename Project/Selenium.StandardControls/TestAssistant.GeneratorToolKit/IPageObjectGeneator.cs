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

        PageObjectGenerateResult GeneratePageObjectCode(IWebDriver driver, PageIdentifyInfo pageIdenfityInfo, string name, PageObjectPropertyInfo[] properties);

        PageObjectGenerateResult GenerateComponetObjectCode(IWebDriver driver, IWebElement componentElement, string name, PageObjectPropertyInfo[] properties);
    }
}
