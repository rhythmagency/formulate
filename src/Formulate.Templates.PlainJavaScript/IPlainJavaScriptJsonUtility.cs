namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.RenderModels;

    public interface IPlainJavaScriptJsonUtility
    {
        string GetJson(ConfiguredFormRenderModel renderModel, string containerId);
    }
}
