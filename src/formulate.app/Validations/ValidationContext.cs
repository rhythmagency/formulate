namespace formulate.app.Validations
{

    // Namespaces.
    using Forms;

    /// <summary>
    /// Contextual information to be used while deserializing a validation configuration.
    /// </summary>
    public class ValidationContext
    {
        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        public IFormField Field { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        public Form Form { get; set; }
    }
}
