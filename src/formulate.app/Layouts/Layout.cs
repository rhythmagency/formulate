namespace formulate.app.Layouts
{

    // Namespaces.
    using Entities;
    using Newtonsoft.Json;
    using System;


    /// <summary>
    /// A form layout.
    /// </summary>
    public class Layout : IEntity
    {

        #region Properties

        /// <summary>
        /// The ID of the type of layout.
        /// </summary>
        public Guid KindId { get; set; }


        /// <summary>
        /// The unique ID of this layout.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The entity path to this layout.
        /// </summary>
        /// <remarks>
        /// This path excludes the root, but includes the layout ID.
        /// </remarks>
        public Guid[] Path { get; set; }


        /// <summary>
        /// The alias of this layout.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of this layout.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The icon for layouts.
        /// </summary>
        [JsonIgnore()]
        public string Icon
        {
            get
            {
                return Constants.Trees.Layouts.ItemIcon;
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
                return EntityKind.Layout;
            }
        }


        /// <summary>
        /// The data stored by this layout.
        /// </summary>
        public string Data { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Layout()
        {
        }

        #endregion

    }

}