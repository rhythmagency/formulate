namespace Formulate.Core.ConfiguredForms
{
    // Namespaces.
    using Persistence;
    using System;

    /// <summary>
    /// A persisted configured form entity.
    /// </summary>
    public sealed class PersistedConfiguredForm : PersistedEntity, IConfiguredFormSettings
    {
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The ID of the chosen template.
        /// </summary>
        public Guid? TemplateId { get; set; }

        /// <summary>
        /// The ID of the chosen layout.
        /// </summary>
        public Guid? LayoutId { get; set; }
    }
}