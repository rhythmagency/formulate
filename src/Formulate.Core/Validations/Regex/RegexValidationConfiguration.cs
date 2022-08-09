namespace Formulate.Core.Validations.Regex
{
    /// <summary>
    /// Configuration used by <see cref="RegexValidation"/>.
    /// </summary>
    public sealed class RegexValidationConfiguration
    {
        /// <summary>
        /// Gets or sets the regular expression pattern.
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }
    }
}
