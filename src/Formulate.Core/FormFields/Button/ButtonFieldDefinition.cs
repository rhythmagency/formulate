namespace Formulate.Core.FormFields.Button
{
    // Namespaces.
    using System;

    /// <summary>
    /// The form field definition used to create <see cref="ButtonField"/>.
    /// </summary>
    public sealed class ButtonFieldDefinition : FormFieldDefinition<ButtonField>
    {
        /// <summary>
        /// Constants related to <see cref="ButtonFieldDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind id.
            /// </summary>
            public const string KindId = "CDE8565C5E9241129A1F7FFA1940C53C";

            /// <summary>
            /// The definition label.
            /// </summary>
            public const string DefinitionLabel = "Button";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-button";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-button-field";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override string DefinitionLabel => Constants.DefinitionLabel;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override FormField CreateField(IFormFieldSettings settings)
        {
            var configuration = new ButtonFieldConfiguration()
            {
                ButtonKind = "submit"
            };

            var field = new ButtonField(settings, configuration);
            
            return field;
        }
    }
}