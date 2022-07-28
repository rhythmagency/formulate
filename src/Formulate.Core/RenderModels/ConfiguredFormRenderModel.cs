using Formulate.Website.RenderModels;

namespace Formulate.Core.RenderModels
{
    using Formulate.Core.Layouts;

    public sealed class ConfiguredFormRenderModel
    {
        public ConfiguredFormRenderModel(FormRenderModel form, ILayout layout)
        {
            Form = form;
            Layout = layout;
        }

        public FormRenderModel Form { get; init; }

        public ILayout Layout { get; init; }
    }
}
