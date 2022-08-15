namespace Formulate.Core.ConfiguredForms
{
    // Namespaces.
    using Persistence;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A persisted configured form entity.
    /// </summary>
    [DataContract]
    public sealed class PersistedConfiguredForm : PersistedEntity, IConfiguredFormSettings
    {
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        [DataMember]
        public string Alias { get; set; }

        /// <summary>
        /// The ID of the chosen template.
        /// </summary>
        [DataMember]
        public Guid? TemplateId { get; set; }

        /// <summary>
        /// The ID of the chosen layout.
        /// </summary>
        [DataMember]
        public Guid? LayoutId { get; set; }
    }
}