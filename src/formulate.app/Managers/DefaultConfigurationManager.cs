namespace formulate.app.Managers
{

    // Namespaces.
    using Configuration;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Templates;


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


        /// <summary>
        /// The templates used to render forms.
        /// </summary>
        public IEnumerable<Template> Templates
        {
            get
            {
                var templatesSection = ConfigurationManager
                    .GetSection("formulateConfiguration/templates") as TemplatesConfigSection;
                var templateItems = templatesSection?.Templates;
                return templateItems.Cast<TemplateElement>().Select(x => new Template()
                {
                    Name = x.Name,
                    Path = x.Path
                }).ToArray();
            }
        }

        #endregion

    }

}