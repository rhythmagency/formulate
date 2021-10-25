using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Validations
{
    /// <inheritdoc />
    public sealed class ValidationDefinitionCollection : BuilderCollectionBase<IValidationDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public ValidationDefinitionCollection(Func<IEnumerable<IValidationDefinition>> items)
            : base(items)
        {
        }
    }
}
