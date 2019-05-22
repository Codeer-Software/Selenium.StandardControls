using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class PageObjectGeneator : IPageObjectGeneator
    {
        public int Priority => 0;

        public Dictionary<string, Action> GetGridMenu(GridSelectType gridSelectType, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo propertyInfo)
        {
            return new Dictionary<string, Action>();
        }

        public PageObjectGenerateResult GenerateComponetObjectCode(IWebDriver driver, IWebElement componentElement, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        {
            return new PageObjectGenerateResult { TypeFullName = "xxx.PPP", Code = "code" };
        }

        public PageObjectGenerateResult GeneratePageObjectCode(IWebDriver driver, PageIdentifyInfo pageIdenfityInfo, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        {
            return new PageObjectGenerateResult { TypeFullName = "xxx.CCC", Code = "code" };
        }

        public IdentifyInfo[] GetIdentifyingCandidates(ISearchContext serachContext, IWebElement element)
        {
            var elementInfo = new ElementInfo(element);
            var candidate = new List<IdentifyInfo>();
            if (!string.IsNullOrEmpty(elementInfo.Id))
            {
                candidate.Add(new IdentifyInfo { Identify = $"ById(\"{elementInfo.Id}\")", IsPerfect = true, DefaultName = elementInfo.Id });
            }

            try
            {
                if (!string.IsNullOrEmpty(elementInfo.Class))
                {
                    var items = serachContext.FindElements(By.ClassName(elementInfo.Class));
                    if (items.Count == 1)
                    {
                        candidate.Add(new IdentifyInfo { Identify = $"ByClassName(\"{elementInfo.Class}\")", IsPerfect = true, DefaultName = elementInfo.Class });
                    }
                    else
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (element.Equals(items[i]))
                            {
                                candidate.Add(new IdentifyInfo { Identify = $"ByClassName(\"{elementInfo.Class}\")[{i}]", DefaultName = elementInfo.Class + i });
                                break;
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(elementInfo.Name))
                {
                    var items = serachContext.FindElements(By.Name(elementInfo.Name));
                    if (items.Count == 1)
                    {
                        candidate.Add(new IdentifyInfo { Identify = $"ByName(\"{elementInfo.Name}\")", IsPerfect = true, DefaultName = elementInfo.Name });
                    }
                    else
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (element.Equals(items[i]))
                            {
                                candidate.Add(new IdentifyInfo { Identify = $"ByName(\"{elementInfo.Name}\")[{i}]", DefaultName = elementInfo.Name + i });
                                break;
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                if (!string.IsNullOrEmpty(element.TagName))
                {
                    var items = serachContext.FindElements(By.TagName(element.TagName));
                    if (items.Count == 1)
                    {
                        candidate.Add(new IdentifyInfo { Identify = $"ByTagName(\"{element.TagName}\")", IsPerfect = true, DefaultName = element.TagName });
                    }
                    else
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (element.Equals(items[i]))
                            {
                                candidate.Add(new IdentifyInfo { Identify = $"ByTagName(\"{element.TagName}\")[{i}]", DefaultName = element.TagName + i });
                                break;
                            }
                        }
                    }
                }

            }
            catch { }

            return candidate.ToArray();
        }
    }
}
