namespace formulate.app.Validations.Kinds.Regex
{

    /// <summary>
    /// Configuration for a regex validation.
    /// </summary>
    public class ValidationRegexConfiguration
    {

        #region Properties

        /// <summary>
        /// The regular expression pattern.
        /// </summary>
        public string Pattern { get; set; }


        /// <summary>
        /// The error message to show when the validation fails.
        /// </summary>
        public string Message { get; set; }

        #endregion

    }

}