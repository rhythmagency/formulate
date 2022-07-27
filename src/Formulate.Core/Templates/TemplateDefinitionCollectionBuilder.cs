using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Templates
{
    /// <inheritdoc />
    public sealed class TemplateDefinitionCollectionBuilder : LazyCollectionBuilderBase<TemplateDefinitionCollectionBuilder, TemplateDefinitionCollection, ITemplateDefinition>
    {
        /// <inheritdoc />
        protected override TemplateDefinitionCollectionBuilder This => this;
    }
}
