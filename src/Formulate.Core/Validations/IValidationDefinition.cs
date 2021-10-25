using Formulate.Core.Types;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// A contract for creating a validation definition.
    /// </summary>
    public interface IValidationDefinition : IDefinition
    {
        /// <summary>
        /// Creates a new instance of a <see cref="IValidation"/>.
        /// </summary>
        /// <param name="settings">The current validation settings.</param>
        /// <returns>A <see cref="IValidation"/>.</returns>
        IValidation CreateValidation(IValidationSettings settings);
    }
}
