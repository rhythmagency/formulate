using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormFields
{
    /// <inheritdoc />
    public sealed class FormFieldTypeCollectionBuilder : LazyCollectionBuilderBase<FormFieldTypeCollectionBuilder, FormFieldTypeCollection, IFormFieldType>
    {
        /// <inheritdoc />
        protected override FormFieldTypeCollectionBuilder This => this;
    }
}
