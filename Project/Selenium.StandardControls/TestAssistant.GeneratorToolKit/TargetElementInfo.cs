using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Element Info
    /// </summary>
    public class TargetElementInfo
    {
        /// <summary>
        /// Priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Attributes.
        /// Key is attribute name.
        /// Value is attribute value.
        /// If you use attribute name only, set null to value.
        /// </summary>
        public Dictionary<string, string> Attrributes { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public TargetElementInfo() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tag">tag.</param>
        public TargetElementInfo(string tag)
        {
            Tag = tag;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tag">tag.</param>
        /// <param name="attribueName">atribute name.</param>
        public TargetElementInfo(string tag, string attribueName)
        {
            Tag = tag;
            Attrributes[attribueName] = null;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tag">tag.</param>
        /// <param name="attribueName">atribute name.</param>
        /// <param name="attributeVallue">atribute vallue.</param>
        public TargetElementInfo(string tag, string attribueName, string attributeVallue)
        {
            Tag = tag;
            Attrributes[attribueName] = attributeVallue;
        }
    }
}
