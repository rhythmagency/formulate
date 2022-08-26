namespace Formulate.Core.FormFields.TextConstant
{
    using Formulate.Core.Utilities;
    // Namespaces.
    using System;

    /// <summary>
    /// A text form field definition.
    /// </summary>
    public sealed class TextConstantFieldDefinition : FormFieldDefinition<TextConstantField>
    {
        private readonly IJsonUtility _jsonUtility;

        public TextConstantFieldDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <summary>
        /// Constants related to <see cref="TextConstantFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "D9B1A60A11864440887B93195C760B5E";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Text Constant";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-text-constant";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-text-constant-field";
        }

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Category => FormFieldConstants.Categories.Constants;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override bool IsServerSideOnly => true; 

        /// <inheritdoc />
        public override bool SupportsLabel => false;

        /// <inheritdoc />
        public override bool SupportsValidation => false;

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var configuration = _jsonUtility.Deserialize<TextConstantFieldConfiguration>(settings.Data);

            return new TextConstantField(settings, configuration);
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            return _jsonUtility.Deserialize<TextConstantFieldConfiguration>(settings.Data) ?? new TextConstantFieldConfiguration();
        }
    }
}