namespace formulate.app.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// The FormulateConfig interface.
    /// </summary>
    public interface IFormulateConfig
    {
        /// <summary>
        /// Gets the version of Formulate.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the persistence configuration.
        /// </summary>
        PersistenceConfig Persistence { get; }

        /// <summary>
        /// Gets the templates configuration.
        /// </summary>
        IEnumerable<TemplateConfigItem> Templates { get; }

        /// <summary>
        /// Gets the buttons configuration.
        /// </summary>
        IEnumerable<ButtonConfigItem> Buttons { get; }

        /// <summary>
        /// Gets the email configuration.
        /// </summary>
        EmailConfig Email { get; }

        /// <summary>
        /// Gets the submissions configuration.
        /// </summary>
        SubmissionsConfig Submissions { get; }

        /// <summary>
        /// Gets the field categories configuration.
        /// </summary>
        IEnumerable<FieldCategoryConfigItem> FieldCategories { get; }
    }
}
