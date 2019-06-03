namespace formulate.app.Configuration
{
    /// <summary>
    /// A "email" configuration element.
    /// </summary>
    public sealed class EmailWhitelistConfigItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email to whitelist.
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Gets or sets the email domain to whitelist.
        /// </summary>
        public string Domain { get; set; }

        #endregion
    }
}
