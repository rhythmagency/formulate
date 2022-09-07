namespace Formulate.BackOffice.Definitions.Forms
{
    // Namespaces.
    using System;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Composing;

    /// <inheritdoc />
    public sealed class FormDefinitionCollection
        : BuilderCollectionBase<IFormDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormDefinitionCollection(
            Func<IEnumerable<IFormDefinition>> items)
            : base(items)
        {
        }
    }
}