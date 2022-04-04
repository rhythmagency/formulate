namespace Formulate.Core.Configuration
{
    // Namespaces.
    using System.Collections.Generic;

    /// <summary>
    /// The configuration options for form templates.
    /// </summary>
    public class TemplatesOptions
    {
        /// <summary>
        /// The name of the section to load this configuration from.
        /// </summary>
        public const string SectionName = "Formulate:Templates";

        /// <summary>
        /// The template items.
        /// </summary>
        public List<TemplateItem> Items { get; set; }
    }
}