namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.DataValues;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    public sealed class DataValueKindCollection : BuilderCollectionBase<IDataValueKind>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValueKindCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public DataValueKindCollection(IEnumerable<IDataValueKind> items)
            : base(items)
        {
        }
    }
}
