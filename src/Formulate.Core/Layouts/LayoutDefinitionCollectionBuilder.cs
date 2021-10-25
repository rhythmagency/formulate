using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Layouts
{
    /// <inheritdoc />
    public sealed class LayoutDefinitionCollectionBuilder : LazyCollectionBuilderBase<LayoutDefinitionCollectionBuilder, LayoutDefinitionCollection, ILayoutDefinition>
    {
        /// <inheritdoc />
        protected override LayoutDefinitionCollectionBuilder This => this;
    }
}
