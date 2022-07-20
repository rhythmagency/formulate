namespace Formulate.Core.FormFields
{
    // Namespaces.
    using Umbraco.Cms.Core.Composing;

    /// <inheritdoc />
    public sealed class FormFieldDefinitionCollectionBuilder
        : LazyCollectionBuilderBase<
            FormFieldDefinitionCollectionBuilder,
            FormFieldDefinitionCollection,
            IFormFieldDefinition>
    {
        /// <inheritdoc />
        protected override FormFieldDefinitionCollectionBuilder This => this;
    }
}