using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Validations
{
    /// <inheritdoc />
    public sealed class ValidationDefinitionCollectionBuilder : LazyCollectionBuilderBase<ValidationDefinitionCollectionBuilder, ValidationDefinitionCollection, IValidationDefinition>
    {
        /// <inheritdoc />
        protected override ValidationDefinitionCollectionBuilder This => this;
    }
}
