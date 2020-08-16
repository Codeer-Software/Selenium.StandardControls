namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Customize key action at capture window.
    /// </summary>
    public interface ICaptureKeyActionCustomizer
    {
        /// <summary>
        /// Preview key.
        /// </summary>
        /// <param name="key">System.Windows.Forms.Keys</param>
        /// <param name="modifyKeys">System.Windows.Forms.Keys</param>
        /// <returns>Is target key ?</returns>
        bool PreviewKey(int key, int modifyKeys);

        /// <summary>
        /// Customize key action.
        /// </summary>
        /// <param name="webDriver">web driver.</param>
        /// <param name="driver">selected driver at attach tree.</param>
        /// <param name="key">System.Windows.Forms.Keys</param>
        /// <param name="modifyKeys">System.Windows.Forms.Keys</param>
        /// <returns>Is handled ?</returns>
        bool Invoke(object webDriver, object driver, int key, int modifyKeys);
    }
}
