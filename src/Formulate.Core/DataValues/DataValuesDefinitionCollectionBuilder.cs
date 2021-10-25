using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.DataValues
{
    /// <inheritdoc />
    public sealed class DataValuesDefinitionCollectionBuilder : LazyCollectionBuilderBase<DataValuesDefinitionCollectionBuilder, DataValuesDefinitionCollection, IDataValuesDefinition>
    {
        /// <inheritdoc />
        protected override DataValuesDefinitionCollectionBuilder This => this;
    }
}
