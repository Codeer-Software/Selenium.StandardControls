using System;
using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Classes that inherit ComponentBase or ControlDriverBase can specify this attribute for the following two types of properties. 
    /// Instructs TestAssistantPro to use this class when it finds the element specified by TargetElementInfo.
    /// public static TargetElementInfo Prop { get; }
    /// public static TargetElementInfo[] Prop { get; }
    /// 
    /// Other classes can have this attribute on the following properties: Instructs TestAssistantPro to use the type of Key when the element specified by TargetElementInfo is found.
    /// public static Dictionary&lt;Type, List&lt;TargetElementInfo>> Prop { get; }
    /// </summary>
    public class TargetElementInfoAttribute : Attribute { }
}
