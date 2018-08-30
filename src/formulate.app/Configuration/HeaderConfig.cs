namespace formulate.app.Configuration
{

    // Namespaces.
    using System;
    using System.Configuration;

    /// <summary>
    /// The configuration item for an email header.
    /// </summary>
    public class HeaderConfig : ConfigurationElement
    {

        #region Properties

        /// <summary>
        /// The name of the email header.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }


        /// <summary>
        /// The value of the email header.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true, IsKey = false)]
        public string Value
        {
            get => ReplaceTokens((string)this["value"]);
            set => this["value"] = value;
        }

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
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return input.Replace("{Guid.NewGuid()}", Guid.NewGuid().ToString());
        }

        #endregion

    }

}