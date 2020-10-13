namespace formulate.app.Forms
{

    // Namespaces.
    using System;

    using Entities;

    using Newtonsoft.Json;

    /// <summary>
    /// A configured form (i.e., a form / layout / template).
    /// </summary>
    public class ConfiguredForm : IEntity
    {
        /// <summary>
        /// Gets or sets the icon for configured forms.
        /// </summary>
        [JsonIgnore]
        public string Icon => Constants.Trees.ConfiguredForms.ItemIcon;

        /// <summary>
        /// Gets or sets the unique ID of this configured form.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the kind of this entity.
        /// </summary>
        public EntityKind Kind => EntityKind.ConfiguredForm;

        /// <summary>
        /// Gets or sets the name of this configured form.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the entity path to this configured form.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the configured form ID.
        /// </remarks>
        public Guid[] Path { get; set; }

        /// <summary>
        /// Gets or sets the ID of the template.
        /// </summary>
        public Guid? TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the layout.
        /// </summary>
        public Guid? LayoutId { get; set; }
    }
}
