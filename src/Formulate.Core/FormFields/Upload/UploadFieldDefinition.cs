namespace Formulate.Core.FormFields.Upload
{
    // Namespaces.
    using System;

    /// <summary>
    /// An upload field definition.
    /// </summary>
    public sealed class UploadFieldDefinition : FormFieldDefinition<UploadField>
    {
        /// <summary>
        /// Constants related to <see cref="UploadFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "DFEFA5EC02004806A2AB0AB22058021D";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Upload";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-upload";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-upload-field";
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
            return new UploadField(settings);
        }
    }
}