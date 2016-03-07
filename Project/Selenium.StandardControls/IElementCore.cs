namespace Selenium.StandardControls
{
    /// <summary>
    /// How to Obtain Element of Attribute and CSSProperties
    /// </summary>
    public interface IElementCore
    {
        #region Methods

        /// <summary>
        /// Get Element Attribute
        /// </summary>
        /// <typeparam name="T">Attribute Type</typeparam>
        /// <param name="name">Attribute Name</param>
        /// <returns>Attribute Value</returns>
        T GetAttribute<T>(string name);

        /// <summary>
        /// Get CSS Properties
        /// </summary>
        /// <param name="name">CSS Properties Name</param>
        /// <returns>CSS Properties</returns>
        string GetCssValue(string name);

        #endregion Methods
    }
}