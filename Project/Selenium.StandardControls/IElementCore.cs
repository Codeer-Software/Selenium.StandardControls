namespace Selenium.StandardControls.Inside
{
    public interface IElementCore
    {
        T GetAttribute<T>(string name);
        string GetCssValue(string name);
    }
}
