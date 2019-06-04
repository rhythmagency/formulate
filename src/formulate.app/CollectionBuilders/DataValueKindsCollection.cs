namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.DataValues;

    using Umbraco.Core.Composing;

    public sealed class DataValueKindCollection : BuilderCollectionBase<IDataValueKind>
    {
        public DataValueKindCollection(IEnumerable<IDataValueKind> items)
            : base(items)
        {
        }
    }
}