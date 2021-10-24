using System;

namespace Formulate.Core.FormFields.Text
{
    /// <summary>
    /// A text form field type.
    /// </summary>
    public sealed class TextFieldType : FormFieldType
    {
        /// <summary>
        /// Constants related to <see cref="TextFieldType"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The type id.
            /// </summary>
            public const string TypeId = "1790658086EA440BBC309E1B099F803B";

            /// <summary>
            /// The type label.
            /// </summary>
            public const string TypeLabel = "Text";

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
        public override string TypeLabel => Constants.TypeLabel;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override Guid TypeId => new Guid(Constants.TypeId);

        /// <inheritdoc />
        public override IFormField CreateField(IFormFieldSettings settings)
        {
            return new TextField(settings);
        }
    }
}
