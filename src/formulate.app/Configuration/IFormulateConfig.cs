namespace formulate.app.Configuration
{
    using System.Collections.Generic;
    using formulate.app.Templates;

    /// <summary>
    /// The FormulateConfig interface.
    /// </summary>
    public interface IFormulateConfig
    {
        /// <summary>
        /// Gets the persistence configuration.
        /// </summary>
        PersistenceConfig Persistence { get; }

        /// <summary>
        /// Gets the templates configuration.
        /// </summary>
        IEnumerable<Template> Templates { get; }

        /// <summary>
        /// Gets the buttons configuration.
        /// </summary>
        IEnumerable<Button> Buttons { get; }

        /// <summary>
        /// Gets the email configuration.
        /// </summary>
        EmailConfig Email { get; }
    }
}
