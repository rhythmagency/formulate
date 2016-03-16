namespace formulate.app.Forms
{

    // Namespaces.
    using Entities;
    using Newtonsoft.Json;
    using Serialization;
    using System;


    /// <summary>
    /// A form.
    /// </summary>
    public class Form : IEntity
    {

        #region Properties

        /// <summary>
        /// The unique ID of this form.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity path to this form.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the form ID.
        /// </remarks>
        public Guid[] Path { get; set; }


        /// <summary>
        /// The alias of this form.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of this form.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The icon for forms.
        /// </summary>
        [JsonIgnore()]
        public string Icon
        {
            get
            {
                return Constants.Trees.Forms.ItemIcon;
            }
        }


        /// <summary>
        /// The kind of this entity.
        /// </summary>
        [JsonIgnore()]
        public EntityKind Kind
        {
            get
            {
                return EntityKind.Form;
            }
        }


        /// <summary>
        /// The fields on this form.
        /// </summary>
        [JsonConverter(typeof(FieldsJsonConverter))]
        public IFormField[] Fields { get; set; }


        /// <summary>
        /// The handlers on this form.
        /// </summary>
        [JsonConverter(typeof(HandlersJsonConverter))]
        public IFormHandler[] Handlers { get; set; }


        /// <summary>
        /// Information about this form.
        /// </summary>
        public IFormMetaInfo[] MetaInfo { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Form()
        {
        }

        #endregion

    }

}