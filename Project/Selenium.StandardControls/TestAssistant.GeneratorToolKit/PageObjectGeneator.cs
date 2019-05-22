using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class PageObjectGeneator : IPageObjectGeneator
    {
        public int Priority => 0;
        const string Indent = "    ";

        public Dictionary<string, Action> GetGridMenu(GridSelectType gridSelectType, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo propertyInfo)
        {
            return new Dictionary<string, Action>();
        }

        public PageObjectGenerateResult GeneratePageObjectCode(IWebDriver driver, PageIdentifyInfo pageIdenfityInfo, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        {
            var typeFullNmae = BrowserAnalyzeInfo.SelectedNameSpace + "." + name;

            //using
            var code = new List<string>
            {
                "using OpenQA.Selenium;",
                "using Selenium.StandardControls;",
                "using Selenium.StandardControls.PageObjectUtility;",
                "using Selenium.StandardControls.TestAssistant.GeneratorToolKit;"
            };
            code.AddRange(properties.Select(e => "using " + GetNameSpace(e.Type) + ";"));
            code = code.Distinct().ToList();

            code.Add(string.Empty);
            code.Add($"namespace {BrowserAnalyzeInfo.SelectedNameSpace}");
            code.Add("{");
            code.Add($"{Indent}public class {name} : PageBase");
            code.Add($"{Indent}{{");

            //member
            foreach (var e in properties)
            {
                code.Add($"{Indent}{Indent}public {GetTypeName(e.Type)} {e.Name} => {GetIdentify(e)};");
            }
            code.Add(string.Empty);

            //constructor
            code.Add($"{Indent}{Indent}public {name}(IWebDriver driver) : base(driver) {{ }}");

            code.Add($"{Indent}}}");

            code.Add(string.Empty);

            code.Add($"{Indent}public static class {name}Extensions");
            code.Add($"{Indent}{{");
            code.Add($"{Indent}{Indent}[PageObjectIdentify(PartOfUrl = \"{pageIdenfityInfo.PartOfUrl}\")]");
            code.Add($"{Indent}{Indent}public static {name} Attach{name}(this IWebDriver driver) => new {name}(driver);");
            code.Add($"{Indent}}}");

            code.Add("}");
            return new PageObjectGenerateResult { TypeFullName = typeFullNmae, Code = string.Join(Environment.NewLine, code) };
        }

        public PageObjectGenerateResult GenerateComponetObjectCode(IWebDriver driver, IWebElement componentElement, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        {
            var typeFullNmae = BrowserAnalyzeInfo.SelectedNameSpace + "." + name;

            //using
            var code = new List<string>
            {
                "using OpenQA.Selenium;",
                "using Selenium.StandardControls;",
                "using Selenium.StandardControls.PageObjectUtility;",
                "using Selenium.StandardControls.TestAssistant.GeneratorToolKit;"
            };
            code.AddRange(properties.Select(e => "using " + GetNameSpace(e.Type) + ";"));
            code = code.Distinct().ToList();

            code.Add(string.Empty);
            code.Add($"namespace {BrowserAnalyzeInfo.SelectedNameSpace}");
            code.Add("{");
            code.Add($"{Indent}public class {name} : ComponentBase");
            code.Add($"{Indent}{{");

            //member
            foreach (var e in properties)
            {
                code.Add($"{Indent}{Indent}{GetTypeName(e.Type)} {e.Name} => {GetIdentify(e)};");
            }
            code.Add(string.Empty);

            //constructor
            code.Add($"{Indent}{Indent}public {name}(IWebElement element) : base(element) {{ }}");

            code.Add($"{Indent}}}");
            code.Add("}");

            return new PageObjectGenerateResult { TypeFullName = typeFullNmae, Code = string.Join(Environment.NewLine, code) };
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

        static string GetIdentify(PageObjectPropertyInfo propertyInfo)
        {
            if (propertyInfo.Type == "OpenQA.Selenium.IWebElement") return propertyInfo.Identify + ".Find()";
            return propertyInfo.Identify;
        }

        static string GetTypeName(string type) => type.Split('.').Last();

        static string GetNameSpace(string type)
        {
            var ret = type.Split('.');
            if (ret.Length <= 1) return string.Empty;
            return string.Join(".", ret.Take(ret.Length - 1));
        }
    }
}
