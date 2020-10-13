namespace formulate.app.CollectionBuilders
{
    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    internal sealed class FormFieldTypeCollectionBuilder : LazyCollectionBuilderBase<FormFieldTypeCollectionBuilder, FormFieldTypeCollection, IFormFieldType>
    {
        /// <inheritdoc />
        protected override FormFieldTypeCollectionBuilder This => this;
    
        /// <inheritdoc />
        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
