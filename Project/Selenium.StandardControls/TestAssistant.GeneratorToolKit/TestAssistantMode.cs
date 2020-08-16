namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Test Assistant Pro Mode.
    /// </summary>
    public static class TestAssistantMode
    {
        /// <summary>
        /// Mode
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Non.
            /// </summary>
            Non,

            /// <summary>
            /// Analyze.
            /// </summary>
            Analyze,

            /// <summary>
            /// Capture.
            /// </summary>
            Capture,

            /// <summary>
            /// Execute.
            /// </summary>
            Execute,
        }

        /// <summary>
        /// Current mode.
        /// </summary>
        public static Mode CurrentMode { get; internal set; } = Mode.Non;

        /// <summary>
        /// If CurrentMode is Analyze or Capture, it return true.
        /// </summary>
        public static bool IsCreatingMode => CurrentMode == Mode.Analyze || CurrentMode == Mode.Capture;
    }
}
