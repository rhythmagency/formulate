namespace formulate.app.CollectionBuilders
{
    using formulate.app.Forms;

    using Umbraco.Core.Composing;

    /// <inheritdoc />
    public sealed class FormHandlerTypeCollectionBuilder : LazyCollectionBuilderBase<FormHandlerTypeCollectionBuilder, FormHandlerTypeCollection, IFormHandlerType>
    {
        /// <inheritdoc />
        protected override FormHandlerTypeCollectionBuilder This => this;

        /// <inheritdoc />
        protected override Lifetime CollectionLifetime => Lifetime.Transient;
    }
}
