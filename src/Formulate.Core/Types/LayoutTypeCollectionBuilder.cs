using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class LayoutTypeCollectionBuilder : LazyCollectionBuilderBase<LayoutTypeCollectionBuilder, LayoutTypeCollection, ILayoutType>
    {
        /// <inheritdoc />
        protected override LayoutTypeCollectionBuilder This => this;
    }
}