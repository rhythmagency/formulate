namespace Formulate.BackOffice.Definitions.Forms
{
    // Namespaces.
    using Umbraco.Cms.Core.Composing;

    /// <inheritdoc />
    public sealed class FormDefinitionCollectionBuilder
        : OrderedCollectionBuilderBase<
            FormDefinitionCollectionBuilder,
            FormDefinitionCollection,
            IFormDefinition>
    {
        /// <inheritdoc />
        protected override FormDefinitionCollectionBuilder This => this;
    }
}