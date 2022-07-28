namespace Formulate.Website.Utilities
{
    using Formulate.Core.ConfiguredForms;
    using Formulate.Core.RenderModels;

    public interface IBuildConfiguredFormRenderModel
    {
        ConfiguredFormRenderModel? Build(ConfiguredForm configuredForm);
    }
}
