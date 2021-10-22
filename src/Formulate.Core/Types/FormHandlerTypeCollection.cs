using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class FormHandlerTypeCollection : BuilderCollectionBase<IFormHandlerType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormHandlerTypeCollection(Func<IEnumerable<IFormHandlerType>> items)
            : base(items)
        {
        }
    }
}
