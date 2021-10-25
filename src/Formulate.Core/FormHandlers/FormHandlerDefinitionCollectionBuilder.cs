using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.FormHandlers
{
    /// <inheritdoc />
    public sealed class FormHandlerDefinitionCollectionBuilder : LazyCollectionBuilderBase<FormHandlerDefinitionCollectionBuilder, FormHandlerDefinitionCollection, IFormHandlerDefinition>
    {
        /// <inheritdoc />
        protected override FormHandlerDefinitionCollectionBuilder This => this;
    }
}
