using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    public static class BrowserAnalyzeInfo
    {
        internal static string SelectedNameSpaceCore { get; set; }
        public static string SelectedNameSpace => SelectedNameSpaceCore;

        internal static List<string> PageObjectTypesCore { get; } = new List<string>();
        public static string[] PageObjectTypes => PageObjectTypesCore.ToArray();

        internal static List<string> ComponentObjectTypesCore { get; } = new List<string>();
        public static string[] ComponentTypes => ComponentObjectTypesCore.ToArray();

        internal static List<string> ControlDriverTypesCore { get; } = new List<string>();
        public static string[] ControlDriverTypes => ControlDriverTypesCore.ToArray();
    }
}
