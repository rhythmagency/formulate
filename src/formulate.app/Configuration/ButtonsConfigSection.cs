namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for Formulate buttons.
    /// </summary>
    public class ButtonsConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The buttons in this configuration section.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ButtonCollection), AddItemName = "button")]
        public ButtonCollection Buttons
        {
            get
            {
                return base[""] as ButtonCollection;
            }
        }

        #endregion

    }

}