using System;
using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
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
        public IValidation Create(IValidationSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundValidationDefinition = _validationDefinitions.FirstOrDefault(settings.DefinitionId);

            return foundValidationDefinition?.CreateValidation(settings);
        }
    }
}
