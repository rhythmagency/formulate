namespace formulate.app.Validations
{

    // Namespaces.
    using Forms;


    /// <summary>
    /// Contextual information to be used while deserializing a validation configuration.
    /// </summary>
    public class ValidationContext
    {
        public IFormField Field { get; set; }
        public Form Form { get; set; }
    }

}