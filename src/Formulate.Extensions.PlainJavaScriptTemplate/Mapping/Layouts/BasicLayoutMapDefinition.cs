namespace Formulate.Extensions.PlainJavaScriptTemplate.Mapping.Layouts
{
    using System.Linq;
    using Formulate.Core.Layouts.Basic;
    using Formulate.Extensions.PlainJavaScriptTemplate;
    using Umbraco.Cms.Core.Mapping;

    public sealed class BasicLayoutMapDefinition : LayoutMapDefinition<BasicLayout>
    {
        protected override PlainJavaScriptLayout Map(BasicLayout layout, MapperContext context)
        {
            var layoutConfig = layout.Configuration;

            var rows = layoutConfig.Rows.Select(r => new PlainJavaScriptLayoutRow()
            {
                Cells = r.Cells.Select(c => new PlainJavaScriptLayoutCell()
                {
                    ColumnSpan = c.ColumnSpan,
                    FieldIds = c.Fields.Select(x => x.Id).ToArray()
                }).ToArray()
            }).ToArray();

            return new PlainJavaScriptLayout()
            {
                Rows = rows
            };
        }
    }
}
