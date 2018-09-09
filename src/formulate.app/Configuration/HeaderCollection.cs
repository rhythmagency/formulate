namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// The email header collection.
    /// </summary>
    public class HeaderCollection : ConfigurationElementCollection
    {

        #region Methods

        /// <summary>
        /// Implementation of ConfigurationElementCollection 
        /// </summary>
        protected override ConfigurationElement CreateNewElement()
        {
            return new HeaderConfig();
        }


        /// <summary>
        /// Implementation of ConfigurationElementCollection 
        /// </summary>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HeaderConfig)element).Name;
        }

        #endregion

    }

}