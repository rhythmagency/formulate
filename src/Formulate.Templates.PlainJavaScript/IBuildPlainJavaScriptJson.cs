namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.RenderModels;

    public interface IBuildPlainJavaScriptJson
    {
        string Build(ConfiguredFormRenderModel renderModel, string containerId);
    }
}
