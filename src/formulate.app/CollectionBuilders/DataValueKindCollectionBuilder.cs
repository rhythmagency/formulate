namespace formulate.app.CollectionBuilders
{
    using formulate.app.DataValues;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    internal sealed class DataValueKindCollectionBuilder : LazyCollectionBuilderBase<DataValueKindCollectionBuilder, DataValueKindCollection, IDataValueKind>
    {
        /// <inheritdoc />
        protected override DataValueKindCollectionBuilder This => this;

        /// <inheritdoc />
        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
