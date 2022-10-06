namespace Formulate.Web.Utilities
{
    using Formulate.Core;
    using Formulate.Core.RenderModels;

    public interface IBuildFormLayoutRenderModel
    {
        FormLayoutRenderModel? Build(FormLayout formLayout);
    }
}
