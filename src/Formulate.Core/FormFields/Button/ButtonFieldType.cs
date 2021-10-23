using System;

namespace Formulate.Core.FormFields.Button
{
    /// <summary>
    /// The form field type used to create <see cref="ButtonField"/>.
    /// </summary>
    public sealed class ButtonFieldType : FormFieldType
    {
        /// <summary>
        /// Constants related to <see cref="ButtonFieldType"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The type id.
            /// </summary>
            public const string TypeId = "CDE8565C5E9241129A1F7FFA1940C53C";

            /// <summary>
            /// The type label.
            /// </summary>
            public const string TypeLabel = "Button";

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
        public override Guid TypeId => Guid.Parse(Constants.TypeId);

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override string TypeLabel => Constants.TypeLabel;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override IFormField CreateField(IFormFieldSettings settings)
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
