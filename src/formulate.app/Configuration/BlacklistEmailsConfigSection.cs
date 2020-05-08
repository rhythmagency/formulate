namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for blacklisting emails.
    /// </summary>
    public class BlacklistEmailsConfigSection : ConfigurationSection
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

        #endregion

    }

}