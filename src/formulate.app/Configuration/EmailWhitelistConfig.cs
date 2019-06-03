namespace formulate.app.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The email whitelist config.
    /// </summary>
    public sealed class EmailWhitelistConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether the whitelist configuration is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the allowed emails configuration.
        /// </summary>
        public IEnumerable<EmailWhitelistConfigItem> AllowedEmails { get; set; }
    }
}
