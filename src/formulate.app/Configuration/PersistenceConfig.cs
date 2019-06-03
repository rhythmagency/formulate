namespace formulate.app.Configuration
{
    /// <summary>
    /// The persistence config.
    /// </summary>
    public sealed class PersistenceConfig
    {
        /// <summary>
        /// Gets or sets the json base path.
        /// </summary>
        public string JsonBasePath { get; set; }

        /// <summary>
        /// Gets or sets the file storage base path.
        /// </summary>
        public string FileStorageBasePath { get; set; }
    }
}