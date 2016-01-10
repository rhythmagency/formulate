namespace formulate.app.DataValues
{

    // Namespaces.
    using Entities;
    using Newtonsoft.Json;
    using System;


    /// <summary>
    /// A data value for use by a form (typically a form field).
    /// </summary>
    public class DataValue : IEntity
    {

        #region Properties

        /// <summary>
        /// The ID of the type of data value.
        /// </summary>
        public Guid KindId { get; set; }


        /// <summary>
        /// The unique ID of this data value.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity path to this data value.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the data value ID.
        /// </remarks>
        public Guid[] Path { get; set; }


        /// <summary>
        /// The alias of this data value.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of this data value.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The icon for data values.
        /// </summary>
        [JsonIgnore()]
        public string Icon
        {
            get
            {
                return Constants.Trees.DataValues.ItemIcon;
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
                return EntityKind.DataValue;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataValue()
        {
        }

        #endregion

    }

}