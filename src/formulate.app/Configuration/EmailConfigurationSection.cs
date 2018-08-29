namespace formulate.app.Configuration
{
    using System;
    // Namespaces.
    using System.Configuration;

    /// <summary>
    /// A configuration section for emails.
    /// </summary>
    public class EmailConfigurationSection : ConfigurationSection
    {
        #region Properties

        /// <summary>
        /// The headers to be applied to the email.
        /// </summary>
        [ConfigurationProperty("headers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(HeaderCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public HeaderCollection Headers
        {
            get
            {
                return (HeaderCollection)base["headers"];
            }
        }

        /// <summary>
        /// The property to enable the email whitelist.
        /// </summary>
        [ConfigurationProperty("enabled", IsRequired = false)]
        public bool Enabled
        {
            get
            {
                return (bool)(this["enabled"]);
            }
            set
            {
                this["enabled"] = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// The header collection
    /// </summary>
    public class HeaderCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Implementation of ConfigurationElementCollection 
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new HeaderConfig();
        }

        /// <summary>
        /// Implementation of ConfigurationElementCollection 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HeaderConfig)element).Name;
        }
    }

    /// <summary>
    /// The Header Configuration Item
    /// </summary>
    public class HeaderConfig: ConfigurationElement
    {
        /// <summary>
        /// Name Attribute
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Value Attribute
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true, IsKey = false)]
        public string Value
        {
            get {
                return ReplaceTokens((string)this["value"]);
            }
            set { this["value"] = value; }
        }

        /// <summary>
        /// Replace a known set of supported tokens for "dynamic" header data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ReplaceTokens(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            return input.Replace("{Guid.NewGuid()}", Guid.NewGuid().ToString());
        }
    }
}