namespace formulate.app.Configuration
{

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

        #endregion

    }

}