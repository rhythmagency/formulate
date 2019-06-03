namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Forms.Handlers.SendData;
    using Helpers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;


    /// <summary>
    /// Controller for Formulate form submission handlers.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class HandlersController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetHandlerTypesError = @"An error occurred while attempting to get the handler types for a Formulate form.";
        private const string GetResultHandlersError = @"An error occurred while attempting to get the result handler functions.";

        #endregion

        private  ILogger Logger { get; set; }


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HandlersController(ILogger logger)
        {
            Logger = logger;
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
                Logger.Error<HandlersController>(GetHandlerTypesError, ex);
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
        /// Returns the result handler functions.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information about result
        /// handler functions.
        /// </returns>
        [HttpGet]
        public object GetResultHandlers()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var handlers = ReflectionHelper
                    .InstantiateInterfaceImplementations<IHandleSendDataResult>()
                    .OrderBy(x => x.Name);


                // Return results.
                result = new
                {
                    Success = true,
                    Kinds = handlers.Select(x => new
                    {
                        Name = x.Name,
                        ClassName = x.GetType().AssemblyQualifiedName
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<HandlersController>(GetResultHandlersError, ex);
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