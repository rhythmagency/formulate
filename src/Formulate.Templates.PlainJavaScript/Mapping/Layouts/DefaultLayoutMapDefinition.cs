namespace Formulate.Templates.PlainJavaScript.Mapping.Layouts
{
    using Formulate.Core.Layouts;
    using Formulate.Templates.PlainJavaScript;
    using Umbraco.Cms.Core.Mapping;

    public sealed class DefaultLayoutMapDefinition : LayoutMapDefinition<ILayout>
    {
        protected override PlainJavaScriptLayout? Map(ILayout layout, MapperContext context)
        {
            return default;
        }
    }
}
