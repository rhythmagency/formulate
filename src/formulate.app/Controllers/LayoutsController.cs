namespace formulate.app.Controllers
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using Models.Requests;
    using Constants = Umbraco.Core.Constants;
    using Umbraco.Core.Logging;


    /// <summary>
    /// Controller for Formulate layouts.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize(Constants.Applications.Users)]
    public class LayoutsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string CreateLayoutError = @"An error occurred while attempting to create a Formulate layout.";
        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";

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

            try
            {

                //TODO: Create layout with persistence service.


                // Success.
                result = new
                {
                    Success = true,
                    Id = "1111",
                    Path = new[] { "", "1111" }
                };

            }
            catch(Exception ex)
            {
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

        #endregion

    }

}