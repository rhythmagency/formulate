namespace Formulate.Website.Utilities
{
    using Formulate.Core.Layouts;
    using Formulate.Core.RenderModels;

    public interface IBuildFormLayoutRenderModel
    {
        FormLayoutRenderModel? Build(FormLayout formLayout);
    }
}
