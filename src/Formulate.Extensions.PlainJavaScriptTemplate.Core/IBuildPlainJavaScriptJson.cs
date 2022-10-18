namespace Formulate.Extensions.PlainJavaScriptTemplate.Core
{
    using Formulate.Core.RenderModels;

    public interface IBuildPlainJavaScriptJson
    {
        string Build(FormLayoutRenderModel renderModel, string containerId);
    }
}
