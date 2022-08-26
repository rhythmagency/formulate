namespace Formulate.Core.FormFields.RichText
{
    using Formulate.Core.Utilities;
    // Namespaces.
    using System;

    /// <summary>
    /// A rich text form field definition.
    /// </summary>
    public sealed class RichTextFieldDefinition : FormFieldDefinition<RichTextField>
    {
        private readonly IJsonUtility _jsonUtility;

        public RichTextFieldDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <summary>
        /// Constants related to <see cref="RichTextFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "6FCDFDC9293F4913B762F4BA502216EB";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Rich Text";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-rich-text";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-rich-text-field";
        }

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Category => FormFieldConstants.Categories.Content;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override bool IsStored => false;

        /// <inheritdoc />
        /// <remarks>
        /// This form field does not support validation.
        /// </remarks>
        public override bool SupportsValidation => false;

        /// <inheritdoc />
        /// <remarks>
        /// This form field does not support a label.
        /// </remarks>
        public override bool SupportsLabel => false;


        /// <inheritdoc />
        /// <remarks>
        /// This form field does not support a category.
        /// </remarks>
        public override bool SupportsCategory => false;

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var configuration = _jsonUtility.Deserialize<RichTextFieldConfiguration>(settings.Data);

            return new RichTextField(settings, configuration);
        }

        /// <inheritdoc />
        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {
            var config = _jsonUtility.Deserialize<RichTextFieldConfiguration>(settings.Data);
            
            if (config is null)
            {
                return new RichTextFieldConfiguration();
            }

            return config;
        }
    }
}