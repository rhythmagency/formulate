namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A collection of templates from the configuration.
    /// </summary>
    [ConfigurationCollection(typeof(TemplateElement))]
    public class TemplateCollection : ConfigurationElementCollection
    {

        #region Methods

        /// <summary>
        /// Creates a new template element.
        /// </summary>
        /// <returns>The template element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TemplateElement();
        }


        /// <summary>
        /// Gets the key for an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as TemplateElement).Name;
        }

        #endregion

    }

}