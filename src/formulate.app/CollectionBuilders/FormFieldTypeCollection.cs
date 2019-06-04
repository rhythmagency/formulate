namespace formulate.app.CollectionBuilders
{
    using System.Collections.Generic;

    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    public sealed class FormFieldTypeCollection : BuilderCollectionBase<IFormFieldType>
    {
        public FormFieldTypeCollection(IEnumerable<IFormFieldType> items)
            : base(items)
        {
        }
    }
}