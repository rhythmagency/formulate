namespace formulate.app.Configuration
{
    /// <summary>
    /// A configuration section for Formulate submissions.
    /// </summary>
    public sealed class SubmissionsConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to enable server side validation for submissions.
        /// </summary>
        public bool EnableServerSideValidation { get; set; }

        #endregion
    }
}
