namespace formulate.app.Configuration
{

    //  Namespaces.
    using System.Configuration;


    /// <summary>
    /// A "email" configuration element.
    /// </summary>
    public class EmailElement : ConfigurationElement
    {

        #region Properties

        /// <summary>
        /// The email to whitelist/blacklist.
        /// </summary>
        [ConfigurationProperty("email", IsRequired = false)]
        public string Email
        {
            get
            {
                return base["email"] as string;
            }
            set
            {
                base["email"] = value;
            }
        }


        /// <summary>
        /// The email domain to whitelist/blacklist.
        /// </summary>
        [ConfigurationProperty("domain", IsRequired = false)]
        public string Domain
        {
            get
            {
                return base["domain"] as string;
            }
            set
            {
                base["domain"] = value;
            }
        }

        #endregion

    }

}