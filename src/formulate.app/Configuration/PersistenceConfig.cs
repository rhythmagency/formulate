namespace formulate.app.Configuration
{
    using System.ComponentModel;

    /// <summary>
    /// The persistence config.
    /// </summary>
    public sealed class PersistenceConfig
    {
        /// <summary>
        /// Gets or sets the json base path.
        /// </summary>
        [DefaultValue("~/App_Data/Formulate/Json/")]
        public string JsonBasePath { get; set; }

        /// <summary>
        /// Gets or sets the file storage base path.
        /// </summary>
        [DefaultValue("~/App_Data/Formulate/FileStorage/Files/")]
        public string FileStorageBasePath { get; set; }
    }
}