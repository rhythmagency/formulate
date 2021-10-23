using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormHandlers
{
    /// <inheritdoc />
    public sealed class FormHandlerTypeCollectionBuilder : LazyCollectionBuilderBase<FormHandlerTypeCollectionBuilder, FormHandlerTypeCollection, IFormHandlerType>
    {
        /// <inheritdoc />
        protected override FormHandlerTypeCollectionBuilder This => this;
    }
}
