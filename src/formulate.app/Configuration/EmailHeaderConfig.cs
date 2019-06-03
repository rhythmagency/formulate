namespace formulate.app.Configuration
{

    // Namespaces.
    using System;

    /// <summary>
    /// The configuration for an email header.
    /// </summary>
    public sealed class EmailHeaderConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the email header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the email header.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Replace a known set of supported tokens for "dynamic" header data.
        /// </summary>
        /// <param name="input">
        /// The value that may contain a token to be replaced.
        /// </param>
        /// <returns>
        /// The value after having the tokens replaced.
        /// </returns>
        private string ReplaceTokens(string input)
        {
            // TODO: Apply this logic to JSON deserialize Value Get
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return input.Replace("{Guid.NewGuid()}", Guid.NewGuid().ToString());
        }

        #endregion
    }
}
