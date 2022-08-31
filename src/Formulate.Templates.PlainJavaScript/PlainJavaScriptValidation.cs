namespace Formulate.Templates.PlainJavaScript
{
    using Formulate.Core.Validations;

    /// <summary>
    /// An object that controls how a <see cref="IValidation"/> is displayed in the Plain JS template.
    /// </summary>
    public sealed class PlainJavaScriptValidation
    {
        public PlainJavaScriptValidation(string validationType) : this(new {}, validationType)
        {
        }

        public PlainJavaScriptValidation(object configuration, string validationType)
        {
            Configuration = configuration;
            ValidationType = validationType;
        }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <remarks>This is optional.</remarks>
        public object Configuration { get; init; }

        /// <summary>
        /// Gets or sets the valiation type.
        /// </summary>
        /// <remarks>This value determines which registered Plain JS validation to use.</remarks>
        public string ValidationType { get; init; }
    }
}
