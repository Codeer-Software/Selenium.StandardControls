using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Code added at capture.
    /// </summary>
    public static class CaptureAdaptor
    {
        internal delegate object GetCaptureCodeGeneratorDelegate(object driver);
        internal static GetCaptureCodeGeneratorDelegate GetCaptureCodeGeneratorCore { get; set; }

        static List<string> _code = new List<string>();
        static List<string> _using = new List<string>();
        static bool _clearCode;

        /// <summary>
        /// Search CaptureCodeGenerator from Driver.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static object GetCaptureCodeGenerator(object driver) => GetCaptureCodeGeneratorCore?.Invoke(driver);

        /// <summary>
        /// Clear code.
        /// </summary>
        public static void ClearCode()
            => _clearCode = true;

        /// <summary>
        /// Add code.
        /// </summary>
        /// <param name="code">code.</param>
        public static void AddCode(string code)
        {
            lock (_code)
            {
                _code.Add(code);
            }
        }

        /// <summary>
        /// Add using.
        /// </summary>
        /// <param name="using">using.</param>
        public static void AddUsing(string @using)
        {
            lock (_using)
            {
                _using.Add(@using);
            }
        }

        /// <summary>
        /// Get code.
        /// </summary>
        /// <returns>code.</returns>
        internal static string[] PopCode()
        {
            lock (_code)
            {
                var ret = _code.ToArray();
                _code.Clear();
                return ret;
            }
        }

        /// <summary>
        /// Get using.
        /// </summary>
        /// <returns>using.</returns>
        internal static string[] PopUsing()
        {
            lock (_using)
            {
                var ret = _using.ToArray();
                _using.Clear();
                return ret;
            }
        }

        /// <summary>
        /// Get is clearing code.
        /// </summary>
        /// <returns>is clearing code</returns>
        internal static bool PopClearCode()
        {
            var ret = _clearCode;
            _clearCode = false;
            return ret;
        }
    }
}
