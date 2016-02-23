namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for Formulate persistence.
    /// </summary>
    public class PersistenceConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The "json" element in this section.
        /// </summary>
        [ConfigurationProperty("json", IsRequired = false)]
        public JsonElement Json
        {
            get
            {
                return this["json"] as JsonElement;
            }
            set
            {
                this["json"] = value;
            }
        }

        #endregion

    }

}