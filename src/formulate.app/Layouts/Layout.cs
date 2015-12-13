namespace formulate.app.Layouts
{

    // Namespaces.
    using System;


    /// <summary>
    /// A form layout.
    /// </summary>
    public class Layout
    {

        #region Properties

        /// <summary>
        /// The ID of the type of layout.
        /// </summary>
        public Guid TypeId { get; set; }


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