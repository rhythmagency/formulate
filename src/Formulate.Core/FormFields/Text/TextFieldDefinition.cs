using System;

namespace Formulate.Core.FormFields.Text
{
    /// <summary>
    /// A text form field definition.
    /// </summary>
    public sealed class TextFieldDefinition : FormFieldDefinition
    {
        /// <summary>
        /// Constants related to <see cref="TextFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The definition id.
            /// </summary>
            public const string DefinitionId = "1790658086EA440BBC309E1B099F803B";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Text";

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
        public override string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid DefinitionId => Guid.Parse(Constants.DefinitionId);

        /// <inheritdoc />
        public override IFormField CreateField(IFormFieldSettings settings)
        {
            return new TextField(settings);
        }
    }
}
