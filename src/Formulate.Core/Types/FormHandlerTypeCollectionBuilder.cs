using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class FormHandlerTypeCollectionBuilder : LazyCollectionBuilderBase<FormHandlerTypeCollectionBuilder, FormHandlerTypeCollection, IFormHandlerType>
    {
        /// <inheritdoc />
        protected override FormHandlerTypeCollectionBuilder This => this;
    }
}
