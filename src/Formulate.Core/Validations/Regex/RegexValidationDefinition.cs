using System;

namespace Formulate.Core.Validations.Regex
{
    /// <summary>
    /// The validation definition used to create <see cref="RegexValidation"/>.
    /// </summary>
    public sealed class RegexValidationDefinition : IValidationDefinition
    {
        /// <summary>
        /// Constants related to <see cref="RegexValidationDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "AC9A464F6F3F4AF9A3B29C85FF0C5580";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Regular Expression";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-regex-validation";
        }

        /// <inheritdoc />
        public Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public string Directive => Constants.Directive;

        /// <inheritdoc />
        public Validation CreateValidation(IValidationSettings settings)
        {
            return new RegexValidation(settings, new RegexValidationConfiguration());
        }

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(IValidationSettings settings)
        {
            return null;
        }
    }
}