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
    }
}
