namespace Formulate.Core.FormFields.TextArea
{
    // Namespaces.
    using System;

    /// <summary>
    /// A text area form field definition.
    /// </summary>
    public sealed class TextAreaFieldDefinition : FormFieldDefinition<TextAreaField>
    {
        /// <summary>
        /// Constants related to <see cref="TextFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "9DA843594D0B494491449F8CCAE7A4DA";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Text Area";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-textarea";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-text-area-field";
        }

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Category => FormFieldConstants.Categories.Inputs;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            return new TextAreaField(settings);
        }
    }
}