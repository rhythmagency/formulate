namespace formulate.app.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The email config.
    /// </summary>
    public sealed class EmailConfig
    {
        /// <summary>
        /// Gets or sets the headers configuration.
        /// </summary>
        public IEnumerable<EmailHeaderConfig> Headers { get; set; }

        /// <summary>
        /// Gets or sets the whitelist configuration.
        /// </summary>
        public EmailWhitelistConfig Whitelist { get; set; }
    }
}
