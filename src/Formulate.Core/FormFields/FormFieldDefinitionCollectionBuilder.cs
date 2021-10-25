using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormFields
{
    /// <inheritdoc />
    public sealed class FormFieldDefinitionCollectionBuilder : LazyCollectionBuilderBase<FormFieldDefinitionCollectionBuilder, FormFieldDefinitionCollection, IFormFieldDefinition>
    {
        /// <inheritdoc />
        protected override FormFieldDefinitionCollectionBuilder This => this;
    }
}
