namespace Formulate.Core.FormFields.Header
{
    // Namespaces.
    using System;

    /// <summary>
    /// A text form field definition.
    /// </summary>
    public sealed class HeaderFieldDefinition : FormFieldDefinition<HeaderField>
    {
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
        public override FormField CreateField(IFormFieldSettings settings)
        {
            return new HeaderField(settings);
        }
    }
}