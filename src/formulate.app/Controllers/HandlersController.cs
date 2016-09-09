namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Helpers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;


    /// <summary>
    /// Controller for Formulate forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class HandlersController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetHandlerTypesError = @"An error occurred while attempting to get the handler types for a Formulate form.";

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HandlersController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public HandlersController(UmbracoContext context)
            : base(context)
        {
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the handler types.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about handler types.
        /// </returns>
        [HttpGet]
        public object GetHandlerTypes()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var instances = ReflectionHelper
                    .InstantiateInterfaceImplementations<IFormHandlerType>();


                // Return results.
                result = new
                {
                    Success = true,
                    HandlerTypes = instances.Select(x => new
                    {
                        Icon = x.Icon,
                        TypeLabel = x.TypeLabel,
                        Directive = x.Directive,
                        TypeFullName = x.GetType().AssemblyQualifiedName
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<HandlersController>(GetHandlerTypesError, ex);
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