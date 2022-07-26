namespace Formulate.Core.FormFields.Button
{
    using Formulate.Core.Utilities;
    // Namespaces.
    using System;

    /// <summary>
    /// The form field definition used to create <see cref="ButtonField"/>.
    /// </summary>
    public sealed class ButtonFieldDefinition : FormFieldDefinition<ButtonField>
    {
        /// <summary>
        /// The json utility.
        /// </summary>
        private readonly IJsonUtility _jsonUtility;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonFieldDefinition"/> class. 
        /// </summary>
        /// <param name="jsonUtility">
        /// The json utility.
        /// </param>
        /// <remarks>
        /// Default constructor.
        /// </remarks>
        public ButtonFieldDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
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

        public override object GetBackOfficeConfiguration(IFormFieldSettings settings)
        {            
            return _jsonUtility.Deserialize<ButtonFieldConfiguration>(settings.Data) ?? new ButtonFieldConfiguration();
        }
    }
}