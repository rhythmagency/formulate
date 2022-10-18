namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Layouts
{
    using Formulate.Core.Layouts;
    using Umbraco.Cms.Core.Mapping;

    public abstract class LayoutMapDefinition<TLayout> : IMapDefinition where TLayout : ILayout
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<TLayout, PlainJavaScriptLayout?>((x, context) => Map(x, context));
        }

        protected abstract PlainJavaScriptLayout? Map(TLayout layout, MapperContext context);
    }
}
