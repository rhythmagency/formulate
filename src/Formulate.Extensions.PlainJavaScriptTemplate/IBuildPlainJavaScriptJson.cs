namespace Formulate.Extensions.PlainJavaScriptTemplate
{
    using Formulate.Core.RenderModels;

    public interface IBuildPlainJavaScriptJson
    {
        string Build(FormLayoutRenderModel renderModel, string containerId);
    }
}
