using OpenQA.Selenium;
using System;
using System.CodeDom.Compiler;
using System.Collections;
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

        [Obsolete]
        public PageObjectGenerateResult GeneratePageObjectCode(IWebDriver driver, PageIdentifyInfo pageIdenfityInfo, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        => GeneratePageObjectCode(new PageObjectCodeInfo
        {
            Driver = driver,
            PageIdentifyInfo = pageIdenfityInfo,
            Name = name,
            Properties = properties
        });

        public PageObjectGenerateResult GeneratePageObjectCode(PageObjectCodeInfo info)
        {
            var typeFullNmae = BrowserAnalyzeInfo.SelectedNameSpace + "." + info.Name;

            //using
            var code = new List<string>
            {
                "using OpenQA.Selenium;",
                "using Selenium.StandardControls;",
                "using Selenium.StandardControls.PageObjectUtility;",
                "using Selenium.StandardControls.TestAssistant.GeneratorToolKit;"
            };
            code.AddRange(info.Properties.SelectMany(e => GetNameSpace(e.Type)).Where(e => !string.IsNullOrEmpty(e)).Select(e => "using " + e + ";"));
            code = code.Distinct().ToList();

            code.Add(string.Empty);
            code.Add($"namespace {BrowserAnalyzeInfo.SelectedNameSpace}");
            code.Add("{");
            code.Add($"{Indent}public class {info.Name} : PageBase");
            code.Add($"{Indent}{{");

            //member
            foreach (var e in info.Properties)
            {
                code.Add($"{Indent}{Indent}public {GetTypeName(e.Type)} {e.Name} => {GetIdentify(e)};");
            }
            code.Add(string.Empty);

            //constructor
            code.Add($"{Indent}{Indent}public {info.Name}(IWebDriver driver) : base(driver) {{ }}");

            code.Add($"{Indent}}}");

            code.Add(string.Empty);

            code.Add($"{Indent}public static class {info.Name}Extensions");
            code.Add($"{Indent}{{");

            if (info.PageIdentifyInfo.TitleComapreType != "None")
            {
                code.Add($"{Indent}{Indent}[PageObjectIdentify(\"{info.PageIdentifyInfo.Title}\", TitleComapreType.{info.PageIdentifyInfo.TitleComapreType})]");
            }
            else if (info.PageIdentifyInfo.UrlComapreType != "None")
            {
                code.Add($"{Indent}{Indent}[PageObjectIdentify(\"{info.PageIdentifyInfo.Url}\", UrlComapreType.{info.PageIdentifyInfo.UrlComapreType})]");
            }

            code.Add($"{Indent}{Indent}public static {info.Name} Attach{info.Name}(this IWebDriver driver) => new {info.Name}(driver);");

            code.Add($"{Indent}}}");

            code.Add("}");
            return new PageObjectGenerateResult { TypeFullName = typeFullNmae, Code = string.Join(Environment.NewLine, code) };
        }

        [Obsolete]
        public PageObjectGenerateResult GenerateComponetObjectCode(IWebDriver driver, IWebElement componentElement, string name, Selenium.StandardControls.TestAssistant.GeneratorToolKit.PageObjectPropertyInfo[] properties)
        => GenerateComponetObjectCode(new ComponetObjectCodeInfo
        {
            Driver = driver,
            ComponentElement = componentElement,
            Name = name,
            Properties = properties
        });

        public PageObjectGenerateResult GenerateComponetObjectCode(ComponetObjectCodeInfo info)
        {
            var typeFullNmae = BrowserAnalyzeInfo.SelectedNameSpace + "." + info.Name;

            //using
            var code = new List<string>
            {
                "using OpenQA.Selenium;",
                "using Selenium.StandardControls;",
                "using Selenium.StandardControls.PageObjectUtility;",
                "using Selenium.StandardControls.TestAssistant.GeneratorToolKit;"
            };
            code.AddRange(info.Properties.SelectMany(e => GetNameSpace(e.Type)).Where(e => !string.IsNullOrEmpty(e)).Select(e => "using " + e + ";"));
            code = code.Distinct().ToList();

            code.Add(string.Empty);
            code.Add($"namespace {BrowserAnalyzeInfo.SelectedNameSpace}");
            code.Add("{");
            code.Add($"{Indent}public class {info.Name} : ComponentBase");
            code.Add($"{Indent}{{");

            //member
            foreach (var e in info.Properties)
            {
                code.Add($"{Indent}{Indent}public {GetTypeName(e.Type)} {e.Name} => {GetIdentify(e)};");
            }
            code.Add(string.Empty);

            //constructor
            code.Add($"{Indent}{Indent}public {info.Name}(IWebElement element) : base(element) {{ }}");

            //converter
            code.Add($"{Indent}{Indent}public static implicit operator {info.Name}(ElementFinder finder) => finder.Find<{info.Name}>();");

            //TargetElementInfo
            if (info.TargetElementInfo != null)
            {
                code.Add(string.Empty);
                code.Add($"{Indent}{Indent}[TargetElementInfo];");
                if (info.TargetElementInfo.Attrributes.Count == 0)
                {
                    code.Add($"{Indent}{Indent}public TargetElementInfo TargetElementInfo => new TargetElementInfo(\"{info.TargetElementInfo.Tag}\");");
                }
                else
                {
                    var attrName = info.TargetElementInfo.Attrributes.Keys.First();
                    var attrValue = info.TargetElementInfo.Attrributes[attrName];
                    code.Add($"{Indent}{Indent}public TargetElementInfo TargetElementInfo => new TargetElementInfo(\"{info.TargetElementInfo.Tag}\", \"{attrName}\", \"{attrValue}\");");
                }
            }

            code.Add($"{Indent}}}");
            code.Add("}");

            return new PageObjectGenerateResult { TypeFullName = typeFullNmae, Code = string.Join(Environment.NewLine, code) };
        }

        public IdentifyInfo[] GetIdentifyingCandidates(ISearchContext serachContext, IWebElement element)
        {
            var js = element.GetJS();
            var isSpecialPerfect = false;

            var candidate = new List<IdentifyInfo>();
            var elementInfo = new ElementInfo(element);

            //id
            try
            {
                if (!string.IsNullOrEmpty(elementInfo.Id) && serachContext.FindElements(By.Id(elementInfo.Id)).Count == 1)
                {
                    candidate.Add(new IdentifyInfo { Identify = $"ById(\"{elementInfo.Id}\")", IsPerfect = true, DefaultName = elementInfo.Id });
                    isSpecialPerfect = true;
                }
            }
            catch { }

            //name
            try
            {
                if (!string.IsNullOrEmpty(elementInfo.Name) && serachContext.FindElements(By.Name(elementInfo.Name)).Count == 1)
                {
                    candidate.Add(new IdentifyInfo { Identify = $"ByName(\"{elementInfo.Name}\")", IsPerfect = true, DefaultName = elementInfo.Name });
                    isSpecialPerfect = true;
                }
            }
            catch { }

            //tag
            try
            {
                if (serachContext.FindElements(By.TagName(element.TagName)).Count == 1)
                {
                    candidate.Add(new IdentifyInfo { Identify = $"ByTagName(\"{element.TagName}\")", IsPerfect = true, DefaultName = element.TagName });
                    isSpecialPerfect = true;
                }
            }
            catch { }

            //css selector (attribute)
            try
            {
                var attrs = GetAttributes(element);
                foreach (var e in attrs.Where(e => e.Key != "id" && e.Key != "name"))
                {
                    var selector = $"{element.TagName}[{e.Key}='{e.Value}']";
                    var finded = serachContext.FindElements(By.CssSelector(selector));
                    if (finded.Count == 1)
                    {
                        candidate.Add(new IdentifyInfo { Identify = $"ByCssSelector(\"{selector}\")", IsPerfect = true, DefaultName = e.Value });
                        isSpecialPerfect = true;
                    }
                }

                //multi attributes
                if (!isSpecialPerfect && 1 < attrs.Count)
                {
                    var selector = element.TagName + string.Join(string.Empty, attrs.Select(e => $"[{e.Key}='{e.Value}']"));
                    var finded = serachContext.FindElements(By.CssSelector(selector));
                    if (finded.Count == 1)
                    {
                        candidate.Add(new IdentifyInfo { Identify = $"ByCssSelector(\"{selector}\")", IsPerfect = true, DefaultName = element.TagName });
                        isSpecialPerfect = true;
                    }
                }
            }
            catch { }

            var isRoot = element.GetJS().ExecuteScript("return document;").Equals(serachContext);

            //css selector
            if (!isSpecialPerfect)
            {
                try
                {
                    var identifyInfo = MakeCssPath(serachContext, element);
                    if (identifyInfo != null)
                    {
                        candidate.Add(identifyInfo);
                        isSpecialPerfect = identifyInfo.IsPerfect;
                    }
                }
                catch { }
            }

            //full xpath.
            try
            {
                candidate.Add(MakeFullXPath(isRoot, serachContext, element));
            }
            catch { }

            //adjust name.
            candidate.ForEach(e => e.DefaultName = AdjustName(e.DefaultName, element.TagName));

            return candidate.ToArray();
        }

        static string AdjustName(string defaultName, string tagName)
        {
            var adjusted = defaultName.Replace(" ", "").Replace("-", "_");
            return _cspDom.IsValidIdentifier(adjusted) ? adjusted : tagName;
        }

        static IdentifyInfo MakeFullXPath(bool isRoot, ISearchContext rootSerachContext, IWebElement element)
        {
            var ancestors = GetAncestors(rootSerachContext, element);

            var js = element.GetJS();
            var fullXPath = "";
            var serachContext = rootSerachContext;
            foreach (var e in ancestors)
            {
                var tags = new List<IWebElement>();
                var parent = js.ExecuteScript("return arguments[0].parentElement;", e);
                if (parent != null)
                {
                    var children = js.ExecuteScript("return arguments[0].children;", parent) as IEnumerable;
                    if (children != null)
                    {
                        tags.AddRange(children.OfType<IWebElement>().Where(x => x.TagName == e.TagName));
                    }
                }

                if (tags.Count <= 1)
                {
                    fullXPath += "/" + e.TagName;
                }
                else
                {
                    //by attribute
                    var hit = false;
                    var attrs = GetAttributes(e);
                    foreach (var x in attrs)
                    {
                        var selector = $"{e.TagName}[@{x.Key}='{x.Value}']";
                        var finded = serachContext.FindElements(By.XPath(selector));
                        if (finded.Count == 1)
                        {
                            hit = true;
                            fullXPath += "/" + selector;
                            break;
                        }
                    }

                    //multi attributes
                    if (!hit && 1 < attrs.Count)
                    {
                        var selector = e.TagName + string.Join(string.Empty, attrs.Select(x => $"[@{x.Key}='{x.Value}']"));
                        var finded = serachContext.FindElements(By.XPath(selector));
                        if (finded.Count == 1)
                        {
                            hit = true;
                            fullXPath += "/" + selector;
                        }
                    }

                    if (!hit)
                    {
                        for (int i = 0; i < tags.Count; i++)
                        {
                            if (e.Equals(tags[i]))
                            {
                                fullXPath += "/" + e.TagName + "[" + (i + 1) + "]";
                                break;
                            }
                        }
                    }
                }
                serachContext = e;
            }

            if (!isRoot && 0 < fullXPath.Length)
            {
                fullXPath = fullXPath.Substring(1);
            }

            //check
            var lastCheck = rootSerachContext.FindElements(By.XPath(fullXPath));
            if (lastCheck.Count != 1 || !lastCheck[0].Equals(element)) return null;

            return new IdentifyInfo { Identify = $"ByXPath(\"{fullXPath}\")", IsPerfect = true, DefaultName = element.TagName };
        }

        static IdentifyInfo MakeCssPath(ISearchContext rootSerachContext, IWebElement element)
        {
            var ancestors = GetAncestors(rootSerachContext, element);

            string cssPath = string.Empty;
            var js = element.GetJS();
            var serachContext = rootSerachContext;

            //TODO It will be supported once the scope specifications are finalized.
            //:scope > div[class=bbb]


            while (0 < ancestors.Count)
            {
                int identifyIndex = -1;

                for (int i = ancestors.Count - 1; 0 <= i; i--)
                {
                    var target = ancestors[i];

                    //by tag
                    if (i != 0 && serachContext.FindElements(By.TagName(target.TagName)).Count == 1)
                    {
                        identifyIndex = i;
                        cssPath += " ";
                        cssPath += target.TagName;
                        break;
                    }

                    //by attribute
                    var attrs = GetAttributes(target);
                    foreach (var e in attrs)
                    {
                        var selector = $"{target.TagName}[{e.Key}='{e.Value}']";
                        var finded = serachContext.FindElements(By.CssSelector(selector));
                        if (finded.Count == 1)
                        {
                            identifyIndex = i;
                            cssPath += " ";
                            cssPath += selector;
                            break;
                        }
                    }
                    if (identifyIndex != -1)
                    {
                        break;
                    }

                    //multi attributes
                    if (1 < attrs.Count)
                    {
                        var selector = target.TagName + string.Join(string.Empty, attrs.Select(e => $"[{e.Key}='{e.Value}']"));
                        var finded = serachContext.FindElements(By.CssSelector(selector));
                        if (finded.Count == 1)
                        {
                            identifyIndex = i;
                            cssPath += " ";
                            cssPath += selector;
                            break;
                        }
                    }
                }

                if (identifyIndex == -1) return null;

                serachContext = ancestors[identifyIndex];
                ancestors = ancestors.Skip(identifyIndex + 1).ToList();
            }

            if (0 < cssPath.Length)
            {
                cssPath = cssPath.Substring(1);
            }

            //check
            var lastCheck = rootSerachContext.FindElements(By.CssSelector(cssPath));
            if (lastCheck.Count != 1 || !lastCheck[0].Equals(element)) return null;

            return new IdentifyInfo { Identify = $"ByCssSelector(\"{cssPath}\")", IsPerfect = true, DefaultName = element.TagName };
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

        static List<IWebElement> GetAncestors(ISearchContext rootSerachContext, IWebElement element)
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

            return list;
        }

        static Dictionary<string, string> GetAttributes(IWebElement element)
        {
            var js = element.GetJS();
            var attrs = js.ExecuteScript(@"
var element = arguments[0];
var src = element.attributes;
var attrs = {};
for (var key in src) {
    var val = src[key];
    if (element.getAttribute(val.name)) {
        attrs[val.name] =  val.value;
    }
}
return attrs;
", element) as IDictionary;

            var dic = new Dictionary<string, string>();
            if (attrs == null) return dic;

            foreach (var e in attrs.Keys)
            {
                var key = e?.ToString();
                if (string.IsNullOrEmpty(key)) continue;
                var value = attrs[e]?.ToString();
                if (string.IsNullOrEmpty(value)) continue;

                dic[key] = value;
            }
            return dic;
        }
    }
}
