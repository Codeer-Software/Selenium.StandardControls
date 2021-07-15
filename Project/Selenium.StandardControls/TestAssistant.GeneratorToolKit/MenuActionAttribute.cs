using System;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Attributes for expanding the menu
    /// </summary>
    public class MenuActionAttribute : Attribute
    {
        /// <summary>
        /// Item name to be displayed in the menu
        /// </summary>
        public string DisplayName { get; set; }
    }
}
