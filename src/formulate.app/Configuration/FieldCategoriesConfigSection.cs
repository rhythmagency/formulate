namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for field categories.
    /// </summary>
    public class FieldCategoriesConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The field categories in this configuration section.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(FieldCategoryCollection), AddItemName = "fieldCategory")]
        public FieldCategoryCollection Categories
        {
            get
            {
                return base[""] as FieldCategoryCollection;
            }
        }

        #endregion
    }
}