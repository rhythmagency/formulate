namespace formulate.app.Validations.Kinds.Mandatory
{

    /// <summary>
    /// Configuration for a mandatory validation.
    /// </summary>
    public class ValidationMandatoryConfiguration
    {

        #region Properties

        /// <summary>
        /// The error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Only validate on the client side (i.e., skip server-side validation)?
        /// </summary>
        public bool ClientSideOnly { get; set; }

        #endregion

    }

}