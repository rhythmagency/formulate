namespace formulate.app.CollectionBuilders
{
    using formulate.app.DataValues;

    using Umbraco.Core.Composing;

    internal sealed class DataValueKindCollectionBuilder : LazyCollectionBuilderBase<DataValueKindCollectionBuilder, DataValueKindCollection, IDataValueKind>
    {
        protected override DataValueKindCollectionBuilder This => this;

        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
