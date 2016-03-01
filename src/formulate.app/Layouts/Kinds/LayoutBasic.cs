namespace formulate.app.Layouts.Kinds
{

    // Namespaces.
    using Helpers;
    using System;
    using Constants = formulate.app.Constants.Layouts.LayoutBasic;


    /// <summary>
    /// A layout kind that gives basic layout options.
    /// </summary>
    public class LayoutBasic : ILayoutKind
    {

        #region Properties

        /// <summary>
        /// The kind ID.
        /// </summary>
        public Guid Id
        {
            get
            {
                return GuidHelper.GetGuid(Constants.Id);
            }
        }


        /// <summary>
        /// The kind name.
        /// </summary>
        public string Name
        {
            get
            {
                return Constants.Name;
            }
        }


        /// <summary>
        /// The kind directive.
        /// </summary>
        public string Directive
        {
            get
            {
                return Constants.Directive;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Deserializes the layout.
        /// </summary>
        /// <param name="configuration">
        /// The serialized layout data.
        /// </param>
        /// <returns>
        /// The deserialized layout.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {
            //TODO: ...
            return null;
        }

        #endregion

    }

}