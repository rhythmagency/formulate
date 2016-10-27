namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A collection of buttons from the configuration.
    /// </summary>
    [ConfigurationCollection(typeof(ButtonElement))]
    public class ButtonCollection : ConfigurationElementCollection
    {

        #region Methods

        /// <summary>
        /// Creates a new button element.
        /// </summary>
        /// <returns>The button element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ButtonElement();
        }


        /// <summary>
        /// Gets the key for an element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ButtonElement).Kind;
        }

        #endregion

    }

}