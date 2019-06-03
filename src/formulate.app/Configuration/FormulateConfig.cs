namespace formulate.app.Configuration
{
    using System.Collections.Generic;

    using formulate.app.Templates;

    /// <summary>
    /// The formulate config.
    /// </summary>
    internal sealed class FormulateConfig : IFormulateConfig
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the persistence configuration.
        /// </summary>
        public PersistenceConfig Persistence { get; set; }

        /// <summary>
        /// Gets or sets the templates configuration.
        /// </summary>
        public IEnumerable<TemplateConfigItem> Templates { get; set; }

        /// <summary>
        /// Gets or sets the buttons configuration.
        /// </summary>
        public IEnumerable<ButtonConfigItem> Buttons { get; set; }

        /// <summary>
        /// Gets or sets the email configuration.
        /// </summary>
        public EmailConfig Email { get; set; }

        /// <summary>
        /// Gets or sets the submissions configuration.
        /// </summary>
        public SubmissionsConfig Submissions { get; set; }

        /// <summary>
        /// Gets or sets the field categories configuration.
        /// </summary>
        /// <summary>
        /// Gets or sets the field categories in this configuration section.
        /// </summary>
        public IEnumerable<FieldCategoryConfigItem> FieldCategories { get; set; }
    }
}
