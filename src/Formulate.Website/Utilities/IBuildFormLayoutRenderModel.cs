namespace Formulate.Website.Utilities
{
    using Formulate.Core;
    using Formulate.Core.RenderModels;

    public interface IBuildFormLayoutRenderModel
    {
        FormLayoutRenderModel? Build(FormLayout formLayout);
    }
}
