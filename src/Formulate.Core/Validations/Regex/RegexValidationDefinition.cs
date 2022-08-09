using Formulate.Core.Utilities;
using System;

namespace Formulate.Core.Validations.Regex
{
    /// <summary>
    /// The validation definition used to create <see cref="RegexValidation"/>.
    /// </summary>
    public sealed class RegexValidationDefinition : IValidationDefinition
    {
        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

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

        public RegexValidationDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
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
            var config = _jsonUtility.Deserialize<RegexValidationConfiguration>(settings.Data);

            if (config is null)
            {
                return new RegexValidationConfiguration();
            }

            return config;
        }
    }
}