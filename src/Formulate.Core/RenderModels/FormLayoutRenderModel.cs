using Formulate.Web.RenderModels;

namespace Formulate.Core.RenderModels
{
    using Formulate.Core.Layouts;

    public sealed class FormLayoutRenderModel
    {
        public FormLayoutRenderModel(FormRenderModel form, ILayout layout)
        {
            Form = form;
            Layout = layout;
        }

        public FormRenderModel Form { get; init; }

        public ILayout Layout { get; init; }
    }
}
