namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Collections.Generic;
    using Umbraco.Cms.Core.Composing;

    /// <inheritdoc />
    public sealed class FormFieldDefinitionCollection
        : BuilderCollectionBase<IFormFieldDefinition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldDefinitionCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormFieldDefinitionCollection(
            Func<IEnumerable<IFormFieldDefinition>> items)
            : base(items)
        {
        }
    }
}