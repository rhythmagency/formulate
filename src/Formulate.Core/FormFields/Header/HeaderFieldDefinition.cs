namespace Formulate.Core.FormFields.Header
{
    using Formulate.Core.Utilities;
    // Namespaces.
    using System;

    /// <summary>
    /// A text form field definition.
    /// </summary>
    public sealed class HeaderFieldDefinition : FormFieldDefinition<HeaderField>
    {
        private readonly IJsonUtility _jsonUtility;

        public HeaderFieldDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <summary>
        /// Constants related to <see cref="HeaderFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "6383DD2C68BD482B95DB811D09D01BC8";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Header";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-font";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-header-field";
        }

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public override string Category => FormFieldConstants.Categories.Content;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        /// <remarks>
        /// This form field does not support validation.
        /// </remarks>
        public override bool SupportsValidation => false;

        /// <inheritdoc />
        /// <remarks>
        /// This form field does not support a field label.
        /// </remarks>
        public override bool SupportsFieldLabel => false;

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var configuration = _jsonUtility.Deserialize<HeaderFieldConfiguration>(settings.Data);

            return new HeaderField(settings, configuration);
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            var config = _jsonUtility.Deserialize<HeaderFieldConfiguration>(settings.Data);
            
            if (config is null)
            {
                return new HeaderFieldConfiguration();
            }

            return config;
        }
    }
}