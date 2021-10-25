using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormHandlers
{
    /// <inheritdoc />
    public sealed class FormHandlerDefinitionCollection : BuilderCollectionBase<IFormHandlerDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandlerDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormHandlerDefinitionCollection(Func<IEnumerable<IFormHandlerDefinition>> items)
            : base(items)
        {
        }
    }
}
