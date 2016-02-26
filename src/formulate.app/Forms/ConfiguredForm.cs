namespace formulate.app.Forms
{

    // Namespaces.
    using Entities;
    using Newtonsoft.Json;
    using System;


    /// <summary>
    /// A configured form (i.e., a form / layout / template).
    /// </summary>
    public class ConfiguredForm : IEntity
    {

        /// <summary>
        /// The icon for configured forms.
        /// </summary>
        [JsonIgnore()]
        public string Icon
        {
            get
            {
                return Constants.Trees.ConfiguredForms.ItemIcon;
            }
        }


        /// <summary>
        /// The unique ID of this configured form.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The kind of this entity.
        /// </summary>
        public EntityKind Kind
        {
            get
            {
                return EntityKind.ConfiguredForm;
            }
        }


        /// <summary>
        /// The name of this configured form.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The entity path to this configured form.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the configured form ID.
        /// </remarks>
        public Guid[] Path { get; set; }


        /// <summary>
        /// The ID of the template.
        /// </summary>
        public Guid? TemplateId { get; set; }


        /// <summary>
        /// The ID of the layout.
        /// </summary>
        public Guid? LayoutId { get; set; }

    }

}