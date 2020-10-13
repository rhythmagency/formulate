namespace formulate.app.Validations.Kinds.Mandatory
{
    /// <summary>
    /// Configuration used by <see cref="ValidationMandatory"/>.
    /// </summary>
    public class ValidationMandatoryConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to only validate on the client side (i.e., skip server-side validation)?.
        /// </summary>
        public bool ClientSideOnly { get; set; }

        #endregion
    }
}
