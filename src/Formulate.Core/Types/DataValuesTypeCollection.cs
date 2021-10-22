using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class DataValuesTypeCollection : BuilderCollectionBase<IDataValuesType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValuesTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public DataValuesTypeCollection(Func<IEnumerable<IDataValuesType>> items)
            : base(items)
        {
        }
    }
}
