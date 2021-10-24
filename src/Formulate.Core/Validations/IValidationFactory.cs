namespace Formulate.Core.Validations
{
    /// <summary>
    /// Creates a <see cref="IValidation"/>.
    /// </summary>
    public interface IValidationFactory
    {
        /// <summary>
        /// Creates a validation for the given settings.
        /// </summary>
        /// <param name="settings">The current settings.</param>
        /// <returns>A <see cref="IValidation"/>.</returns>
        IValidation CreateValidation(IValidationSettings settings);
    }
}
