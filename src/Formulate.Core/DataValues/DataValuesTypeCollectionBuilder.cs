using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.DataValues
{
    /// <inheritdoc />
    public sealed class DataValuesTypeCollectionBuilder : LazyCollectionBuilderBase<DataValuesTypeCollectionBuilder, DataValuesTypeCollection, IDataValuesType>
    {
        /// <inheritdoc />
        protected override DataValuesTypeCollectionBuilder This => this;
    }
}
