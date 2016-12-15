namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A collection of field categories from the configuration.
    /// </summary>
    [ConfigurationCollection(typeof(FieldCategoryElement))]
    public class FieldCategoryCollection : ConfigurationElementCollection
    {

        #region Methods

        /// <summary>
        /// Creates a new field category element.
        /// </summary>
        /// <returns>The field category element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FieldCategoryElement();
        }


        /// <summary>
        /// Gets the key for an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as FieldCategoryElement).Kind;
        }

        #endregion

    }

}