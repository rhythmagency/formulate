namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A collection of emails from the configuration.
    /// </summary>
    [ConfigurationCollection(typeof(EmailElement))]
    public class EmailCollection : ConfigurationElementCollection
    {

        #region Methods

        /// <summary>
        /// Creates a new email element.
        /// </summary>
        /// <returns>The email element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EmailElement();
        }


        /// <summary>
        /// Gets the key for an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var typedElement = element as EmailElement;
            return typedElement.Email + typedElement.Domain;
        }

        #endregion

    }

}