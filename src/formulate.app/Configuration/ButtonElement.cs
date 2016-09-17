namespace formulate.app.Configuration
{

    //  Namespaces.
    using System.Configuration;


    /// <summary>
    /// A "button" configuration element.
    /// </summary>
    public class ButtonElement : ConfigurationElement
    {

        #region Properties

        /// <summary>
        /// The kind of the button.
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