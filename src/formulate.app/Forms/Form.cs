namespace formulate.app.Forms
{

    // Namespaces.
    using System;

    using Entities;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// A form.
    /// </summary>
    public class Form : IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique ID of this form.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the entity path to this form.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the form ID.
        /// </remarks>
        public Guid[] Path { get; set; }

        /// <summary>
        /// Gets or sets the alias of this form.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of this form.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the icon for forms.
        /// </summary>
        [JsonIgnore]
        public string Icon => Constants.Trees.Forms.ItemIcon;

        /// <summary>
        /// Gets or sets the kind of this entity.
        /// </summary>
        [JsonIgnore]
        public EntityKind Kind => EntityKind.Form;

        /// <summary>
        /// Gets or sets the fields on this form.
        /// </summary>
        [JsonConverter(typeof(FieldsJsonConverter))]
        public IFormField[] Fields { get; set; }

        /// <summary>
        /// Gets or sets the handlers on this form.
        /// </summary>
        [JsonConverter(typeof(HandlersJsonConverter))]
        public IFormHandler[] Handlers { get; set; }

        /// <summary>
        /// Gets or sets meta information about this form.
        /// </summary>
        public IFormMetaInfo[] MetaInfo { get; set; }

        #endregion
    }
}
