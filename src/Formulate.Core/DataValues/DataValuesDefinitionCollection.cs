using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.DataValues
{
    /// <inheritdoc />
    public sealed class DataValuesDefinitionCollection : BuilderCollectionBase<IDataValuesDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataValuesDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public DataValuesDefinitionCollection(Func<IEnumerable<IDataValuesDefinition>> items)
            : base(items)
        {
        }
    }
}
