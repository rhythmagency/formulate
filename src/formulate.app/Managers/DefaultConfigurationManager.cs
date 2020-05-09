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
        public virtual string JsonBasePath
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
        public virtual string FileStoreBasePath
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
        public virtual IEnumerable<Template> Templates
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
        public virtual IEnumerable<string> ButtonKinds
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
        public virtual bool EnableServerSideValidation
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
        public virtual bool EnableEmailWhitelist
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
        public virtual IEnumerable<AllowEmail> EmailWhitelist
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


        /// <summary>
        /// The emails to blacklist.
        /// </summary>
        public virtual IEnumerable<DisallowEmail> EmailBlacklist
        {
            get
            {
                var emailsSection = ConfigurationManager
                    .GetSection("formulateConfiguration/emailBlacklist") as BlacklistEmailsConfigSection;
                var emailItems = emailsSection?.Emails;
                return emailItems.Cast<EmailElement>().Select(x => new DisallowEmail()
                {
                    Email = x.Email,
                    Domain = x.Domain
                }).ToArray();
            }
        }

        /// <summary>
        /// The headers to use for emails.
        /// </summary>
        public virtual IEnumerable<EmailHeader> EmailHeaders
        {
            get
            {
                var configuration = ConfigurationManager.GetSection("formulateConfiguration/email") as EmailConfigurationSection;
                var headers = new List<EmailHeader>();
                foreach(HeaderConfig header in configuration.Headers)
                {
                    headers.Add(new EmailHeader()
                    {
                        Name = header.Name,
                        Value = header.Value
                    });
                }
                return headers;
            }
        }


        /// <summary>
        /// The field categories
        /// </summary>
        public virtual IEnumerable<FieldCategory> FieldCategories
        {
            get
            {
                var fieldCategoriesSection = ConfigurationManager
                    .GetSection("formulateConfiguration/fieldCategories") as FieldCategoriesConfigSection;
                var FieldCategoryItems = fieldCategoriesSection?.Categories;
                return FieldCategoryItems.Cast<FieldCategoryElement>().Select(x => new FieldCategory()
                {
                    Kind = x.Kind,
                    Group = x.Group
                }).ToArray();
            }
        }

        #endregion

    }

}