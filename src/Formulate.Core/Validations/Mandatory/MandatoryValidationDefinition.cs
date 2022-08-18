using Formulate.Core.Utilities;
using System;

namespace Formulate.Core.Validations.Mandatory
{
    /// <summary>
    /// The validation definition used to create <see cref="MandatoryValidation"/>.
    /// </summary>
    public sealed class MandatoryValidationDefinition : IValidationDefinition
    {
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// Constants related to <see cref="MandatoryValidationDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "93957A02633944A193238E8CD754680B";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Mandatory";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-mandatory-validation";
        }

        public MandatoryValidationDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <inheritdoc />
        public Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public string Name => Constants.Name;

        /// <inheritdoc />
        public string Directive => Constants.Directive;

        /// <inheritdoc />
        public bool IsLegacy => false;

        /// <inheritdoc />
        public Validation CreateValidation(IValidationSettings settings)
        {
            var config = new MandatoryValidationConfiguration();

            return new MandatoryValidation(settings, config);
        }

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(IValidationSettings settings)
        {
            var config = _jsonUtility.Deserialize<MandatoryValidationConfiguration>(settings.Data);

            if (config is null)
            {
                return new MandatoryValidationConfiguration();
            }

            return config;
        }
    }
}
