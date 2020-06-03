using OpenQA.Selenium;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class PageObjectGeneator : IPageObjectGeneator
    {
        public int Priority => 0;
        const string Indent = "    ";

        static CodeDomProvider _cspDom = CodeDomProvider.CreateProvider("CSharp");

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
            code.AddRange(properties.SelectMany(e => GetNameSpace(e.Type)).Where(e=>!string.IsNullOrEmpty(e)).Select(e => "using " + e + ";"));
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
            code.Add($"{Indent}{Indent}[PageObjectIdentify(\"{pageIdenfityInfo.Url}\", UrlComapreType.{pageIdenfityInfo.UrlComapreType})]");
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
            code.AddRange(properties.SelectMany(e => GetNameSpace(e.Type)).Where(e => !string.IsNullOrEmpty(e)).Select(e => "using " + e + ";"));
            code = code.Distinct().ToList();

            code.Add(string.Empty);
            code.Add($"namespace {BrowserAnalyzeInfo.SelectedNameSpace}");
            code.Add("{");
            code.Add($"{Indent}public class {name} : ComponentBase");
            code.Add($"{Indent}{{");

            //member
            foreach (var e in properties)
            {
                code.Add($"{Indent}{Indent}public {GetTypeName(e.Type)} {e.Name} => {GetIdentify(e)};");
            }
            code.Add(string.Empty);

            //constructor
            code.Add($"{Indent}{Indent}public {name}(IWebElement element) : base(element) {{ }}");

            //converter
            code.Add($"{Indent}{Indent}public static implicit operator {name}(ElementFinder finder) => finder.Find<{name}>();");

            code.Add($"{Indent}}}");
            code.Add("}");

            return new PageObjectGenerateResult { TypeFullName = typeFullNmae, Code = string.Join(Environment.NewLine, code) };
        }

        public IdentifyInfo[] GetIdentifyingCandidates(ISearchContext serachContext, IWebElement element)
        {
            var isSpecialPerfect = false;

            //id
            var elementInfo = new ElementInfo(element);
            var candidate = new List<IdentifyInfo>();
            if (!string.IsNullOrEmpty(elementInfo.Id))
            {
                candidate.Add(new IdentifyInfo { Identify = $"ById(\"{elementInfo.Id}\")", IsPerfect = true, DefaultName = elementInfo.Id });
                isSpecialPerfect = true;
            }

            //name
            var infoByName = GetIdentifyInfo(serachContext, element, elementInfo.Name, "ByName", By.Name);
            if (infoByName != null)
            {
                candidate.Add(infoByName);
                if (infoByName.IsPerfect) isSpecialPerfect = true;
            }

            var isRoot = element.GetJS().ExecuteScript("return document;").Equals(serachContext);

            //perfect xpath
            if (isRoot && !isSpecialPerfect)
            {
                //TODO Heavy
                var identifyInfo = MakeShortcutXPath(serachContext, element);
                if (identifyInfo != null)
                {
                    candidate.Add(identifyInfo);
                    isSpecialPerfect = true;
                }
            }

            //class name
            GetIdentifyInfo(serachContext, element, elementInfo.Class, "ByClassName", By.ClassName, candidate);

            //tag name
            GetIdentifyInfo(serachContext, element, element.TagName, "ByTagName", By.TagName, candidate);

            //simple xpath.
            if (isRoot)
            {
                candidate.Add(MakeSimpleXPath(serachContext, element));
            }

            //adjust name.
            candidate.ForEach(e => e.DefaultName = AdjustName(e.DefaultName, element.TagName));

            return candidate.ToArray();
        }

        static string AdjustName(string defaultName, string tagName)
        {
            var adjusted = defaultName.Replace(" ", "").Replace("-", "_");
            return _cspDom.IsValidIdentifier(adjusted) ? adjusted : tagName;
        }

        static void GetIdentifyInfo(ISearchContext serachContext, IWebElement element, string key, string func, Func<string, By> by, List<IdentifyInfo> candidate)
        {
            var info = GetIdentifyInfo(serachContext, element, key, func, by);
            if (info != null) candidate.Add(info);
        }

        static IdentifyInfo GetIdentifyInfo(ISearchContext serachContext, IWebElement element, string key, string func, Func<string, By> by)
        {
            IdentifyInfo info = null;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var items = serachContext.FindElements(by(key));
                    if (items.Count == 1)
                    {
                        info = new IdentifyInfo { Identify = $"{func}(\"{key}\")", IsPerfect = true, DefaultName = key };
                    }
                    else
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (element.Equals(items[i]))
                            {
                                info = new IdentifyInfo { Identify = $"{func}(\"{key}\")[{i}]", DefaultName = key + i };
                                break;
                            }
                        }
                    }
                }
            }
            catch { }

            return info;
        }

        static IdentifyInfo MakeSimpleXPath(ISearchContext rootSerachContext, IWebElement element)
        {
            var list = new List<IWebElement>();
            var checkElement = element;
            while (checkElement != null && !rootSerachContext.Equals(checkElement))
            {
                list.Insert(0, checkElement);
                try
                {
                    checkElement = checkElement.GetParent();
                }
                catch
                {
                    break;
                }
            }

            var path = "";
            var serachContext = rootSerachContext;
            foreach (var e in list)
            {
                var tags = serachContext.FindElements(By.TagName(e.TagName));
                if (tags.Count == 1)
                {
                    path += "/" + e.TagName;
                }
                else
                {
                    for (int i = 0; i < tags.Count; i++)
                    {
                        if (e.Equals(tags[i]))
                        {
                            path += "/" + e.TagName + "[" + (i + 1) + "]";
                            break;
                        }
                    }
                }
                serachContext = e;
            }

            return new IdentifyInfo { Identify = $"ByXPath(\"{path}\")", IsPerfect = true, DefaultName = element.TagName };
        }

        static IdentifyInfo MakeShortcutXPath(ISearchContext rootSerachContext, IWebElement element)
        {
            var list = new List<IWebElement>();
            var checkElement = element;
            var shortcut = false;
            while (checkElement != null && !rootSerachContext.Equals(checkElement))
            {
                list.Insert(0, checkElement);

                var elementInfo = new ElementInfo(checkElement);
                if (!string.IsNullOrEmpty(elementInfo.Id))
                {
                    shortcut = true;
                    break;
                }

                if (!string.IsNullOrEmpty(elementInfo.Name))
                {
                    var names = rootSerachContext.FindElements(By.Name(elementInfo.Name));
                    if (names.Count == 1)
                    {
                        shortcut = true;
                        break;
                    }
                }

                try
                {
                    checkElement = checkElement.GetParent();
                }
                catch
                {
                    break;
                }
            }
            if (!shortcut) return null;

            var path = "/";
            var serachContext = rootSerachContext;
            foreach (var e in list)
            {
                var eInfo = new ElementInfo(e);
                var names = string.IsNullOrEmpty(eInfo.Name) ? new IWebElement[0] : serachContext.FindElements(By.TagName(eInfo.Name)).ToArray();
                var tags = serachContext.FindElements(By.TagName(e.TagName)).ToArray();
                var classes = string.IsNullOrEmpty(eInfo.Class) ? new IWebElement[0] : serachContext.FindElements(By.TagName(eInfo.Class)).ToArray();

                if (!string.IsNullOrEmpty(eInfo.Id))
                {
                    path += $"/{e.TagName}[@id='{eInfo.Id}']";
                }
                else if (names.Length == 1)
                {
                    path += $"/{e.TagName}[@name='{eInfo.Name}']";
                }
                else if (tags.Length == 1)
                {
                    path += "/" + e.TagName;
                }
                else if (classes.Length == 1)
                {
                    path += $"/{e.TagName}[@class='{eInfo.Class}']";
                }
                else
                {
                    for (int i = 0; i < tags.Length; i++)
                    {
                        if (e.Equals(tags[i]))
                        {
                            path += "/" + e.TagName + "[" + (i + 1) + "]";
                            break;
                        }
                    }

                }
                serachContext = e;
            }

            return new IdentifyInfo { Identify = $"ByXPath(\"{path}\")", IsPerfect = true, DefaultName = "" };
        }

        static string GetIdentify(PageObjectPropertyInfo propertyInfo)
        {
            if (propertyInfo.Type == "OpenQA.Selenium.IWebElement") return propertyInfo.Identify + ".Find()";
            return propertyInfo.Identify;
        }

        static string GetTypeName(string typeFullName)
        {
            var sp = typeFullName.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (sp.Length == 2)
            {
                return sp[0].Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last() + "<" + sp[1].Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last() + ">";
            }
            return typeFullName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        static string[] GetNameSpace(string typeFullName)
        {
            var sp = typeFullName.Split(new[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (sp.Length == 2)
            {
                var sp0 = sp[0].Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var sp1 = sp[1].Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                return new[] { string.Join(".", sp0.Take(sp0.Length - 1)), string.Join(".", sp1.Take(sp1.Length - 1)) };
            }

            var spByDot = typeFullName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return new[] { string.Join(".", spByDot.Take(spByDot.Length - 1)) };
        }
    }
}
