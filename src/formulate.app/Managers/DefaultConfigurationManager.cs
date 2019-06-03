namespace formulate.app.Managers
{

    // Namespaces.
    using System.Collections.Generic;
    using System.Linq;

    using Configuration;

    using core.Types;

    using Templates;

    /// <summary>
    /// The default configuration manager.
    /// </summary>
    internal sealed class DefaultConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationManager"/> class.
        /// </summary>
        /// <param name="config">
        /// The Formulate config.
        /// </param>
        public DefaultConfigurationManager(IFormulateConfig config)
        {
            Config = config;
        }

        #region Properties

        /// <summary>
        /// Gets the json base path.
        /// </summary>
        public string JsonBasePath
        {
            get
            {
                return Config.Persistence.JsonBasePath;
            }
        }

        /// <summary>
        /// Gets the file store base path.
        /// </summary>
        public string FileStoreBasePath
        {
            get
            {
                return Config.Persistence.JsonBasePath;
            }
        }

        /// <summary>
        /// Gets the templates.
        /// </summary>
        public IEnumerable<Template> Templates
        {
            get
            {
                return Config.Templates.Select(x => new Template() { Id = x.Id, Name = x.Name, Path = x.Path }).ToArray();
            }
        }

        /// <summary>
        /// Gets the button kinds.
        /// </summary>
        public IEnumerable<string> ButtonKinds
        {
            get
            {
                return Config.Buttons?.Select(x => x.Kind);
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable server side validation.
        /// </summary>
        public bool EnableServerSideValidation
        {
            get
            {
                return Config.Submissions.EnableServerSideValidation;
            }
        }

        /// <summary>
        /// Gets a value indicating whether enable email whitelisting.
        /// </summary>
        public bool EnableEmailWhitelist
        {
            get
            {
                return Config.Email.Whitelist.Enabled;
            }
        }

        /// <summary>
        /// Gets the emails to whitelist.
        /// </summary>
        public IEnumerable<AllowEmail> EmailWhitelist
        {
            get
            {
                return Config.Email.Whitelist.AllowedEmails.Select(x => new AllowEmail()
                {
                    Email = x.Email,
                    Domain = x.Domain
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the headers to use for emails.
        /// </summary>
        public IEnumerable<EmailHeader> EmailHeaders
        {
            get
            {
                return Config.Email.Headers.Select(x => new EmailHeader() { Name = x.Name, Value = x.Value }).ToArray();
            }
        }


        /// <summary>
        /// Gets the field categories.
        /// </summary>
        public IEnumerable<FieldCategory> FieldCategories
        {
            get
            {
                return Config.FieldCategories.Select(x => new FieldCategory()
                {
                    Kind = x.Kind,
                    Group = x.Group
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets or sets the Formulate config.
        /// </summary>
        private IFormulateConfig Config { get; set; }

        #endregion
    }
}
