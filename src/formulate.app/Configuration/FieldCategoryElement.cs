namespace formulate.app.Configuration
{

    //  Namespaces.
    using System.Configuration;


    /// <summary>
    /// A "category" configuration element.
    /// </summary>
    public class FieldCategoryElement : ConfigurationElement
    {

        #region Properties

        /// <summary>
        /// The type of the category.
        /// </summary>
        [ConfigurationProperty("kind", IsRequired = true)]
        public string Kind
        {
            get
            {
                return base["kind"] as string;
            }
            set
            {
                base["kind"] = value;
            }
        }

        #endregion

    }

}