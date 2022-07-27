namespace Formulate.Website.Utilities
{
    using Formulate.Core.ConfiguredForms;
    using Formulate.Website.RenderModels;

    public interface IBuildConfiguredFormRenderModel
    {
        ConfiguredFormRenderModel? Build(ConfiguredForm configuredForm);

    }
}
