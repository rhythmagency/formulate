namespace formulate.app.Controllers
{

    // Namespaces.
    using Helpers;
    using Layouts;
    using Models.Requests;
    using Persistence;
    using Persistence.Internal;
    using System;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;
    using LayoutsConstants = formulate.app.Constants.Trees.Layouts;


    /// <summary>
    /// Controller for Formulate layouts.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize(CoreConstants.Applications.Users)]
    public class LayoutsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string CreateLayoutError = @"An error occurred while attempting to create a Formulate layout.";
        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";

        #endregion


        #region Properties

        private ILayoutPersistence Persistence { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LayoutsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public LayoutsController(UmbracoContext context)
            : base(context)
        {
            //TODO: Should not be creating an instance of this here (implementation should be swappable).
            Persistence = new JsonLayoutPersistence();
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Creates a layout.
        /// </summary>
        /// <param name="request">
        /// The request to create the layout.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        public object CreateLayout(CreateLayoutRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Create layout.
                var typeId = GuidHelper.GetGuid(request.LayoutId);
                var layoutId = Guid.NewGuid();
                var strLayoutId = GuidHelper.GetString(layoutId);
                var layout = new Layout()
                {
                    TypeId = typeId,
                    Id = layoutId,
                    Name = request.LayoutName
                };


                // Persist layout.
                Persistence.Persist(layout);


                // Success.
                result = new
                {
                    Success = true,
                    Id = strLayoutId,
                    //TODO: Once nesting is supported, this will need to account for that.
                    Path = new[] { rootId, LayoutsConstants.Id, strLayoutId }
                };

            }
            catch(Exception ex)
            {

                // Error.
                LogHelper.Error<LayoutsController>(CreateLayoutError, ex);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return result.
            return result;

        }


        /// <summary>
        /// Returns the path of the layout with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the path.
        /// </param>
        /// <returns>
        /// The path.
        /// </returns>
        public object GetPath([FromUri] GetLayoutPathRequest request)
        {

            // Variables.
            var id = request.LayoutId;
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Return result.
            return new
            {
                Success = true,
                //TODO: Once nesting is supported, this will need to account for that.
                Path = new[] { rootId, LayoutsConstants.Id, id }
            };

        }

        #endregion

    }

}