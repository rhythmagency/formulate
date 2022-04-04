namespace Formulate.Core.Configuration
{
    // Namespaces.
    using System;

    /// <summary>
    /// Details about a template item from configuration.
    /// </summary>
    public class TemplateItem
    {
        /// <summary>
        /// The ID that uniquely identifies this template.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name to display for this template.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The path to this template in the file system.
        /// </summary>
        public string Path { get; set; }
    }
}