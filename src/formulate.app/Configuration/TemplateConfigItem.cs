namespace formulate.app.Configuration
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The template config item.
    /// </summary>
    public sealed class TemplateConfigItem
    {
        /// <summary>
        /// Gets or sets the template name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        [Required]

        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the template id.
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}
