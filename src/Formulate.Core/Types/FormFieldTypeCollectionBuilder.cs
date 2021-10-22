using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class FormFieldTypeCollectionBuilder : LazyCollectionBuilderBase<FormFieldTypeCollectionBuilder, FormFieldTypeCollection, IFormFieldType>
    {
        /// <inheritdoc />
        protected override FormFieldTypeCollectionBuilder This => this;
    }
}
