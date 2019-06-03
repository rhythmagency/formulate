namespace formulate.app.Helpers
{
    public interface ILocalizationHelper
    {
        string GetDataValueName(string name);
        string GetLayoutName(string name);
        string GetMenuItemName(string name);
        string GetTreeName(string tree);
    }
}