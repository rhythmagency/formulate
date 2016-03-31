namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for Formulate emails.
    /// </summary>
    public class EmailsConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The emails in this configuration section.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(EmailCollection), AddItemName = "email")]
        public EmailCollection Emails
        {
            get
            {
                return base[""] as EmailCollection;
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

}