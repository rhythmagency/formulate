namespace Formulate.Core.Validations
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for creating a validation definition.
    /// </summary>
    public interface IValidationDefinition : IDefinition, IHaveDirective
    {
        /// <summary>
        /// Creates a new instance of a <see cref="IValidation"/>.
        /// </summary>
        /// <param name="entity">The current entity.</param>
        /// <returns>A <see cref="IValidation"/>.</returns>
        Validation CreateValidation(PersistedValidation entity);

        /// <summary>
        /// Creates an instance of the configuration needed by the back
        /// office.
        /// </summary>
        /// <param name="entity">
        /// The current entity.
        /// </param>
        /// <returns>
        /// The configuration.
        /// </returns>
        object GetBackOfficeConfiguration(PersistedValidation entity);
    }
}