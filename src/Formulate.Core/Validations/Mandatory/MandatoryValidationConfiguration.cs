namespace Formulate.Core.Validations.Mandatory
{
    /// <summary>
    /// Configuration used by <see cref="MandatoryValidation"/>.
    /// </summary>
    public sealed class MandatoryValidationConfiguration
    {
        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to only validate on the client side (i.e., skip server-side validation)?.
        /// </summary>
        public bool ClientSideOnly { get; set; }
    }
}
