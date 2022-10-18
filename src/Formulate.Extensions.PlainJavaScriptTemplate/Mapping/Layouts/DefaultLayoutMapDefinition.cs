namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Layouts
{
    using Formulate.Core.Layouts;
    using Formulate.Extensions.PlainJavaScriptTemplate;
    using Umbraco.Cms.Core.Mapping;

    public sealed class DefaultLayoutMapDefinition : LayoutMapDefinition<ILayout>
    {
        protected override PlainJavaScriptLayout? Map(ILayout layout, MapperContext context)
        {
            return default;
        }
    }
}
