using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class ValidationTypeCollection : BuilderCollectionBase<IValidationType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public ValidationTypeCollection(Func<IEnumerable<IValidationType>> items)
            : base(items)
        {
        }
    }
}
