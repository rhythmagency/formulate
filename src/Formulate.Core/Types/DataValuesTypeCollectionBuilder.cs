using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class DataValuesTypeCollectionBuilder : LazyCollectionBuilderBase<DataValuesTypeCollectionBuilder, DataValuesTypeCollection, IDataValuesType>
    {
        /// <inheritdoc />
        protected override DataValuesTypeCollectionBuilder This => this;
    }
}
