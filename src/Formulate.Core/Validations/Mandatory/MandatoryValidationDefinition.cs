using System;

namespace Formulate.Core.Validations.Mandatory
{
    /// <summary>
    /// The validation definition used to create <see cref="MandatoryValidation"/>.
    /// </summary>
    public sealed class MandatoryValidationDefinition : IValidationDefinition
    {
        /// <summary>
        /// Constants related to <see cref="MandatoryValidationDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The definition ID.
            /// </summary>
            public const string DefinitionId = "93957A02633944A193238E8CD754680B";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Mandatory";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-validation-mandatory";
        }

        /// <inheritdoc />
        public Guid DefinitionId => Guid.Parse(Constants.DefinitionId);

        /// <inheritdoc />
        public string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public string Directive => Constants.Directive;

        /// <inheritdoc />
        public IValidation CreateValidation(IValidationSettings settings)
        {
            var config = new MandatoryValidationConfiguration();

            return new MandatoryValidation(settings, config);
        }
    }
}
