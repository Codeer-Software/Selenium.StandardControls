using System;
using System.Collections.Generic;

namespace Selenium.StandardControls.TestAssistant.GeneratorToolKit
{
    /// <summary>
    /// Menu processing at Capture Attach Tree.
    /// </summary>
    public interface ICaptureAttachTreeMenuAction
    {
        /// <summary>
        /// Get Menu processing at Capture Attach Tree.
        /// </summary>
        /// <param name="accessPath">Access path to driver.</param>
        /// <param name="driver">PageObject or ComponentObject or ControlDriver.</param>
        /// <returns>Menu processing at Capture Attach Tree.</returns>
        Dictionary<string, Action> GetAction(string accessPath, object driver);
    }
}
