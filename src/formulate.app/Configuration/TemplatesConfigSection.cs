namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for Formulate templates.
    /// </summary>
    public class TemplatesConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The templates in this configuration section.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(TemplateCollection), AddItemName = "template")]
        public TemplateCollection Templates
        {
            get
            {
                return base[""] as TemplateCollection;
            }
        }

        #endregion

    }

}