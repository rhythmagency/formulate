namespace formulate.app.CollectionBuilders
{
    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    internal sealed class FormFieldTypeCollectionBuilder : LazyCollectionBuilderBase<FormFieldTypeCollectionBuilder, FormFieldTypeCollection, IFormFieldType>
    {
        protected override FormFieldTypeCollectionBuilder This => this;
        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
