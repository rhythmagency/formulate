using System;
using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// The default implementation of <see cref="IValidationFactory"/> using the <see cref="ValidationTypeCollection"/>.
    /// </summary>
    public sealed class ValidationFactory : IValidationFactory
    {
        /// <summary>
        /// The validation types.
        /// </summary>
        private readonly ValidationTypeCollection validationTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFactory"/> class.
        /// </summary>
        /// <param name="validationTypes">The validation types.</param>
        public ValidationFactory(ValidationTypeCollection validationTypes)
        {
            this.validationTypes = validationTypes;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">The provided settings are null.</exception>
        public IValidation CreateValidation(IValidationSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var foundValidationType = validationTypes.FirstOrDefault(settings.TypeId);

            return foundValidationType?.CreateValidation(settings);
        }
    }
}