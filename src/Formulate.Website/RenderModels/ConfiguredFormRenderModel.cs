namespace Formulate.Website.RenderModels
{
    public sealed class ConfiguredFormRenderModel
    {
        public ConfiguredFormRenderModel(FormRenderModel form, LayoutRenderModel layout)
        {
            Form = form;
            Layout = layout;
        }

        public FormRenderModel Form { get; init; }

        public LayoutRenderModel Layout { get; init; }
    }
}
