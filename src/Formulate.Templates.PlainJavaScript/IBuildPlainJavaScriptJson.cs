namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.RenderModels;

    public interface IBuildPlainJavaScriptJson
    {
        string Build(FormLayoutRenderModel renderModel, string containerId);
    }
}
