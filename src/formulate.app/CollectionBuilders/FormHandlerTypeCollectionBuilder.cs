namespace formulate.app.CollectionBuilders
{
    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    public sealed class FormHandlerTypeCollectionBuilder : LazyCollectionBuilderBase<FormHandlerTypeCollectionBuilder, FormHandlerTypeCollection, IFormHandlerType>
    {
        protected override FormHandlerTypeCollectionBuilder This => this;

        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
