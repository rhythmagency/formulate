namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    public sealed class FormFieldTypeCollection : BuilderCollectionBase<IFormFieldType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldTypeCollection"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public FormFieldTypeCollection(IEnumerable<IFormFieldType> items)
            : base(items)
        {
        }
    }
}
