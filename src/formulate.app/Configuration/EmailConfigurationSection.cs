namespace formulate.app.Configuration
{
    using System;

    // Namespaces.
    using System.Configuration;
    using System.Net.Mail;

    /// <summary>
    /// A configuration section for Formulate emails.
    /// </summary>
    public class EmailConfigurationSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The emails in this configuration section.
        /// </summary>
        [ConfigurationProperty("mailmessage")]
        [ConfigurationCollection(typeof(MailMessage), AddItemName = "email")]
        public MailMessage MailMessage
        {
            get
            {
                return base[""] as MailMessage;
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