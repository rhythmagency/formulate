namespace formulate.app.Configuration
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using formulate.app.Templates;

    /// <summary>
    /// A configuration section for Formulate templates.
    /// </summary>
    public class TemplatesConfigSection : ConfigurationSection, ITemplatesConfig
    {

        #region Properties

        /// <summary>
        /// The templates in this configuration section.
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(TemplateCollection), AddItemName = "template")]
        public TemplateCollection ConfiguredTemplates
        {
            get
            {
                return base[""] as TemplateCollection;
            }
        }

        #endregion

        public IEnumerable<Template> Templates
        {
            get
            {
                return ConfiguredTemplates.Cast<TemplateElement>().Select(x => new Template()
                {
                    Name = x.Name,
                    Path = x.Path,
                    Id = Guid.Parse(x.Id)
                }).ToArray();
            }
        }
    }
}