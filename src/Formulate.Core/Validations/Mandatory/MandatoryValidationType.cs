using System;

namespace Formulate.Core.Validations.Mandatory
{
    /// <summary>
    /// The validation type used to create <see cref="MandatoryValidation"/>.
    /// </summary>
    public sealed class MandatoryValidationType : IValidationType
    {
        /// <summary>
        /// Constants related to <see cref="MandatoryValidationType"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The type ID.
            /// </summary>
            public const string TypeId = "93957A02633944A193238E8CD754680B";

            /// <summary>
            /// The type label.
            /// </summary>
            public const string TypeLabel = "Mandatory";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-validation-mandatory";
        }

        /// <inheritdoc />
        public Guid TypeId => Guid.Parse(Constants.TypeId);

        /// <inheritdoc />
        public string TypeLabel => Constants.TypeLabel;

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
