namespace Formulate.Core.FormFields
{
    // Namespaces.
    using Types;
    using System;

    /// <summary>
    /// The default implementation of <see cref="IFormFieldFactory"/> using the <see cref="FormFieldDefinitionCollection"/>.
    /// </summary>
    internal sealed class FormFieldFactory : IFormFieldFactory
    {
        /// <summary>
        /// The form field definitions.
        /// </summary>
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldFactory"/> class.
        /// </summary>
        /// <param name="formFieldDefinitions">The form field definitions.</param>
        public FormFieldFactory(FormFieldDefinitionCollection formFieldDefinitions)
        {
            _formFieldDefinitions = formFieldDefinitions;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        /// The provided settings are null.
        /// </exception>
        public IFormField Create(IFormFieldSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundFormFieldDefinition = _formFieldDefinitions.FirstOrDefault(settings.KindId);

            if (foundFormFieldDefinition is null)
            {
                return default;
            }

            // Create an instance of the form field.
            var field = foundFormFieldDefinition.CreateField(settings);

            // Set the attributes on the form field that can be obtained from
            // the form field definition (namely, icon and directive).
            field.Icon = foundFormFieldDefinition.Icon;
            field.Directive = foundFormFieldDefinition.Directive;

            // Return the form field.
            return field;
        }
    }
}