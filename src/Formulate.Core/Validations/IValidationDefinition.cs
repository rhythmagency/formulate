namespace Formulate.Core.Validations
{
    // Namespaces.
    using Types;

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
        Validation CreateValidation(IValidationSettings settings);

        /// <summary>
        /// Creates an instance of the configuration needed by the back
        /// office.
        /// </summary>
        /// <param name="settings">
        /// The current validation settings.
        /// </param>
        /// <returns>
        /// The configuration.
        /// </returns>
        object GetBackOfficeConfiguration(IValidationSettings settings);
    }
}