namespace formulate.app.Configuration
{

    // Namespaces.
    using System.Configuration;


    /// <summary>
    /// A configuration section for Formulate submissions.
    /// </summary>
    public class SubmissionsConfigSection : ConfigurationSection
    {

        #region Properties

        /// <summary>
        /// The property to enable server side validation.
        /// </summary>
        [ConfigurationProperty("enableServerSideValidation", IsRequired = false)]
        public bool EnableServerSideValidation
        {
            get
            {
                return (bool)(this["enableServerSideValidation"]);
            }
            set
            {
                this["enableServerSideValidation"] = value;
            }
        }

        #endregion

    }

}