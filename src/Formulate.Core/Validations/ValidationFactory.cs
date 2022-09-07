namespace Formulate.Core.Validations
{
    // Namespaces.
    using System;
    using Types;

    /// <summary>
    /// The default implementation of <see cref="IValidationFactory"/> using the <see cref="ValidationDefinitionCollection"/>.
    /// </summary>
    internal sealed class ValidationFactory : IValidationFactory
    {
        /// <summary>
        /// The validation definitions.
        /// </summary>
        private readonly ValidationDefinitionCollection _validationDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFactory"/> class.
        /// </summary>
        /// <param name="validationDefinitions">The validation definitions.</param>
        public ValidationFactory(ValidationDefinitionCollection validationDefinitions)
        {
            _validationDefinitions = validationDefinitions;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        public IValidation Create(PersistedValidation settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundValidationDefinition = _validationDefinitions.FirstOrDefault(settings.KindId);

            var validation = foundValidationDefinition?.CreateValidation(settings);

            // Set the attributes on the validation that can be obtained from
            // the validation definition.
            validation.Name = settings.Name;
            validation.BackOfficeConfiguration = foundValidationDefinition
                .GetBackOfficeConfiguration(settings);

            return validation;
        }
    }
}