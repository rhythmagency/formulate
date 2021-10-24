using System;

namespace Formulate.Core.Validations.Regex
{
    /// <summary>
    /// The validation type used to create <see cref="RegexValidation"/>.
    /// </summary>
    public sealed class RegexValidationType : IValidationType
    {
        /// <summary>
        /// Constants related to <see cref="RegexValidationType"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The Type ID.
            /// </summary>
            public const string TypeId = "AC9A464F6F3F4AF9A3B29C85FF0C5580";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Regular Expression";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-validation-regex";
        }

        /// <inheritdoc />
        public Guid TypeId => Guid.Parse(Constants.TypeId);

        /// <inheritdoc />
        public string Name => Constants.Name;

        /// <inheritdoc />
        public string Directive => Constants.Directive;

        /// <inheritdoc />
        public IValidation CreateValidation(IValidationSettings settings)
        {
            return new RegexValidation(settings, new RegexValidationConfiguration());
        }
    }
}
