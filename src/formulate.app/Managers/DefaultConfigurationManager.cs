namespace formulate.app.Managers
{

    // Namespaces.
    using Configuration;
    using System.Configuration;


    /// <summary>
    /// The default configuration manager.
    /// </summary>
    internal class DefaultConfigurationManager : IConfigurationManager
    {

        #region Properties

        /// <summary>
        /// The base path to store JSON in.
        /// </summary>
        public string JsonBasePath
        {
            get
            {
                var persistence = ConfigurationManager
                    .GetSection("formulateConfiguration/persistence")
                    as PersistenceConfigSection;
                var basePath = persistence.Json.BasePath;
                return basePath;
            }
        }

        #endregion

    }

}