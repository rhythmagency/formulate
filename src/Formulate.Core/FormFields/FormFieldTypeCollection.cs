using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormFields
{
    /// <inheritdoc />
    public sealed class FormFieldTypeCollection : BuilderCollectionBase<IFormFieldType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormFieldTypeCollection(Func<IEnumerable<IFormFieldType>> items)
            : base(items)
        {
        }
    }
}
