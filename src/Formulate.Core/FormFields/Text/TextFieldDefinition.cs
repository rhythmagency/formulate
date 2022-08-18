namespace Formulate.Core.FormFields.Text
{
    // Namespaces.
    using System;

    /// <summary>
    /// A text form field definition.
    /// </summary>
    public sealed class TextFieldDefinition : FormFieldDefinition<TextField>
    {
        /// <summary>
        /// Constants related to <see cref="TextFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "1790658086EA440BBC309E1B099F803B";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Text";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-document-dashed-line";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-text-field";
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
            return new TextField(settings);
        }
    }
}