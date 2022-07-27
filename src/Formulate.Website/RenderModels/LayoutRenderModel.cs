namespace Formulate.Website.RenderModels
{
    using Formulate.Core.Layouts;

    public sealed class LayoutRenderModel
    {
        public LayoutRenderModel(ILayoutDefinition definition, ILayout layout)
        {
            Definition = definition;
            Layout = layout;
        }

        public ILayoutDefinition Definition { get; init; }

        public ILayout Layout { get; init; }
    }
}