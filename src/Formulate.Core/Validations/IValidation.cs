namespace Formulate.Core.Validations
{
    // Namespaces.
    using Types;

    /// <summary>
    /// A contract for creating a validation.
    /// </summary>
    public interface IValidation : IEntity
    {
        /// <summary>
        /// Gets or sets the name for this validation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the raw configuration for this validation.
        /// </summary>
        public string RawConfiguration { get; }
    }
}