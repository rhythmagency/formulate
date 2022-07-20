namespace Formulate.Core.FormFields
{
    // Namespaces.
    using System;
    using System.Linq;
    using Types;
    using Validations;

    /// <summary>
    /// The default implementation of <see cref="IFormFieldFactory"/> using the <see cref="FormFieldDefinitionCollection"/>.
    /// </summary>
    internal sealed class FormFieldFactory : IFormFieldFactory
    {
        /// <summary>
        /// The form field definitions.
        /// </summary>
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        /// <inheritdoc cref="IValidationFactory" />
        private readonly IValidationFactory _validationFactory;

        /// <inheritdoc cref="IValidationEntityRepository" />
        private readonly IValidationEntityRepository _validationEntityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldFactory"/> class.
        /// </summary>
        /// <param name="formFieldDefinitions">
        /// The form field definitions.
        /// </param>
        /// <param name="validationFactory">
        /// Factory for creating validations.
        /// </param>
        /// <param name="validationEntityRepository">
        /// The repository of validations.
        /// </param>
        public FormFieldFactory(FormFieldDefinitionCollection formFieldDefinitions,
            IValidationFactory validationFactory,
            IValidationEntityRepository validationEntityRepository)
        {
            _formFieldDefinitions = formFieldDefinitions;
            _validationFactory = validationFactory;
            _validationEntityRepository = validationEntityRepository;
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
            // the form field definition.
            field.Icon = foundFormFieldDefinition.Icon;
            field.Directive = foundFormFieldDefinition.Directive;
            field.BackOfficeConfiguration = foundFormFieldDefinition
                .GetBackOfficeConfiguration(settings);

            // Set the validations.
            field.Validations = settings.Validations
                .Select(x => _validationEntityRepository.Get(x))
                .Select(x => _validationFactory.Create(x))
                .ToArray();

            // Return the form field.
            return field;
        }
    }
}