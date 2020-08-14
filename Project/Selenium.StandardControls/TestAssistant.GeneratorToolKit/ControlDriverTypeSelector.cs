using OpenQA.Selenium;
using Selenium.StandardControls.PageObjectUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public class ControlDriverTypeSelector : IControlDriverTypeSelector
    {
        static Dictionary<string, List<TargetElementInfo>> _assignInfo = new Dictionary<string, List<TargetElementInfo>>();

        public int Priority => 0;

        static ControlDriverTypeSelector()
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < asms.Length; i++)
            {
                try
                {
                    var types = asms[i].GetTypes();
                    for (int j = 0; j < types.Length; j++)
                    {
                        try
                        {
                            var type = types[j];
                            if (typeof(ComponentBase).IsAssignableFrom(type) || typeof(ControlDriverBase).IsAssignableFrom(type))
                            {
                                var targetProps = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(e => 0 < e.GetCustomAttributes(typeof(TargetElementInfoAttribute), false).Length);
                                foreach (var x in targetProps.Where(e=> e.PropertyType == typeof(TargetElementInfo)))
                                {
                                    var info = (TargetElementInfo)x.GetValue(null, new object[0]);
                                    if (info != null)
                                    {
                                        if (!_assignInfo.TryGetValue(type.FullName, out var list))
                                        {
                                            list = new List<TargetElementInfo>();
                                            _assignInfo[type.FullName] = list;
                                        }
                                        list.Add(info);
                                    }
                                }

                                foreach (var x in targetProps.Where(e => e.PropertyType == typeof(TargetElementInfo[])))
                                {
                                    var info = (TargetElementInfo[])x.GetValue(null, new object[0]);
                                    if (info != null)
                                    {
                                        if (!_assignInfo.TryGetValue(type.FullName, out var list))
                                        {
                                            list = new List<TargetElementInfo>();
                                            _assignInfo[type.FullName] = list;
                                        }
                                        list.AddRange(info);
                                    }
                                }
                            }
                            else
                            {
                                var targetProps = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(e => 0 < e.GetCustomAttributes(typeof(TargetElementInfoAttribute), false).Length);
                                foreach (var x in targetProps.Where(e => e.PropertyType == typeof(Dictionary<Type, List<TargetElementInfo>>)))
                                {
                                    var info = (Dictionary<Type, List<TargetElementInfo>>)x.GetValue(null, new object[0]);
                                    if (info != null)
                                    {
                                        foreach (var e in info)
                                        {
                                            if (!_assignInfo.TryGetValue(e.Key.FullName, out var list))
                                            {
                                                list = new List<TargetElementInfo>();
                                                _assignInfo[e.Key.FullName] = list;
                                            }
                                            list.AddRange(e.Value);
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
        }

        internal static void AddTargetElementInfo(string typeFullName, TargetElementInfo info)
        {
            lock (_assignInfo)
            {
                if (!_assignInfo.TryGetValue(typeFullName, out var list))
                {
                    list = new List<TargetElementInfo>();
                    _assignInfo[typeFullName] = list;
                }
                list.Add(info);
            }
        }

        public string GetControlDriverType(IWebElement element)
        {
            if (element == null) return string.Empty;

            var hits = new List<KeyValuePair<string, TargetElementInfo>>();

            lock (_assignInfo)
            {
                foreach (var x in _assignInfo)
                {
                    foreach (var targetElementInfo in x.Value)
                    {
                        if (element.TagName.ToLower() == targetElementInfo.Tag.ToLower())
                        {
                            bool hit = true;
                            foreach (var attrAndvalue in targetElementInfo.Attrributes)
                            {
                                var value = element.GetAttribute(attrAndvalue.Key);
                                if (string.IsNullOrEmpty(value))
                                {
                                    hit = false;
                                    break;
                                }

                                if (attrAndvalue.Value != null && attrAndvalue.Value != value)
                                {
                                    hit = false;
                                    break;
                                }
                            }
                            if (hit)
                            {
                                hits.Add(new KeyValuePair<string, TargetElementInfo>(x.Key, targetElementInfo));
                            }
                        }
                    }
                }
            }

            var selectedTypeInfo = hits.OrderByDescending(e => e.Value.Attrributes.Count).OrderByDescending(e => e.Value.Priority).FirstOrDefault();
            return selectedTypeInfo.Value == null ? typeof(IWebElement).FullName : selectedTypeInfo.Key;
        }
    }
}
