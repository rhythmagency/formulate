namespace Formulate.Website.Utilities
{
    using Formulate.Website.RenderModels;

    public interface IPlainJavaScriptJsonUtility
    {
        string GetJson(ConfiguredFormRenderModel renderModel, string containerId);
    }
}
