namespace formulate.app.Configuration
{

    //  Namespaces.
    using System.Configuration;


    /// <summary>
    /// A "template" configuration element.
    /// </summary>
    public class TemplateElement : ConfigurationElement
    {

        #region Constants

        private const string DefaultPath = "~/*Replace Me*.cshtml";
        private const string DefaultId = "00000000000000000000000000000000";

        #endregion


        #region Properties

        /// <summary>
        /// The name of the template.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return base["name"] as string;
            }
            set
            {
                base["name"] = value;
            }
        }


        /// <summary>
        /// The path to this template.
        /// </summary>
        /// <remarks>
        /// Should start with "~" and end with ".cshtml".
        /// </remarks>
        [ConfigurationProperty("path", IsRequired = true, DefaultValue = DefaultPath)]
        [RegexStringValidator(@"^~.*\.[cC][sS][hH][tT][mM][lL]$")]
        public string Path
        {
            get
            {
                var result = base["path"] as string;
                return result == DefaultPath ? null : result;
            }
            set
            {
                base["path"] = value;
            }
        }


        /// <summary>
        /// The ID of this template.
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true, DefaultValue = DefaultId)]
        [RegexStringValidator(@"^[0-9a-fA-F]{32}$")]
        public string Id
        {
            get
            {
                var result = base["id"] as string;
                return result == DefaultId ? null : result;
            }
            set
            {
                base["id"] = value;
            }
        }

        #endregion

    }

}