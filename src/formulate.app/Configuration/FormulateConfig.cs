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
        /// Gets or sets the persistence configuration.
        /// </summary>
        public PersistenceConfig Persistence { get; set; }

        /// <summary>
        /// Gets or sets the templates configuration.
        /// </summary>
        public IEnumerable<Template> Templates { get; set; }
    }
}
