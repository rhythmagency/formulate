namespace formulate.app.Managers
{

    // Namespaces.
    using Configuration;
    using core.Types;
    using System;
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
        /// The base path toe store submitted files in.
        /// </summary>
        public string FileStoreBasePath
        {
            get
            {
                var persistence = ConfigurationManager
                    .GetSection("formulateConfiguration/persistence")
                    as PersistenceConfigSection;
                var basePath = persistence.FileStorage.BasePath;
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
                    Path = x.Path,
                    Id = Guid.Parse(x.Id)
                }).ToArray();
            }
        }


        /// <summary>
        /// The button kinds used when creating button field types.
        /// </summary>
        public IEnumerable<string> ButtonKinds
        {
            get
            {
                var buttonsSection = ConfigurationManager
                    .GetSection("formulateConfiguration/buttons") as ButtonsConfigSection;
                var templateItems = buttonsSection?.Buttons;
                return templateItems.Cast<ButtonElement>().Select(x => x.Kind).ToArray();
            }
        }


        /// <summary>
        /// Enable server side validation of form submissions?
        /// </summary>
        public bool EnableServerSideValidation
        {
            get
            {
                var persistence = ConfigurationManager
                    .GetSection("formulateConfiguration/submissions")
                    as SubmissionsConfigSection;
                var enable = persistence.EnableServerSideValidation;
                return enable;
            }
        }


        /// <summary>
        /// Is the email whitelist enabled?
        /// </summary>
        public bool EnableEmailWhitelist
        {
            get
            {
                var whitelist = ConfigurationManager
                    .GetSection("formulateConfiguration/emailWhitelist") as EmailsConfigSection;
                var enable = whitelist.Enabled;
                return enable;
            }
        }


        /// <summary>
        /// The emails to whitelist.
        /// </summary>
        public IEnumerable<AllowEmail> EmailWhitelist
        {
            get
            {
                var emailsSection = ConfigurationManager
                    .GetSection("formulateConfiguration/emailWhitelist") as EmailsConfigSection;
                var emailItems = emailsSection?.Emails;
                return emailItems.Cast<EmailElement>().Select(x => new AllowEmail()
                {
                    Email = x.Email,
                    Domain = x.Domain
                }).ToArray();
            }
        }

        #endregion

    }

}