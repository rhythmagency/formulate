namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Helpers;
    using Managers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using ResolverConfig = Resolvers.Configuration;


    /// <summary>
    /// Controller for Formulate fields.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class FieldsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetFieldTypesError = @"An error occurred while attempting to get the field types for a Formulate form.";
        private const string GetButtonKindsError = @"An error occurred while attempting to get the button kinds for a Formulate button field.";

        #endregion


        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return ResolverConfig.Current.Manager;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FieldsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public FieldsController(UmbracoContext context)
            : base(context)
        {
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the field types.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about field types.
        /// </returns>
        [HttpGet]
        public object GetFieldTypes()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var instances = ReflectionHelper
                    .InstantiateInterfaceImplementations<IFormFieldType>();


                // Return results.
                result = new
                {
                    Success = true,
                    FieldTypes = instances.Select(x => new
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
                LogHelper.Error<FieldsController>(GetFieldTypesError, ex);
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
        /// Returns the kinds of buttons that can be selected when creating a button in the form designer.
        /// </summary>
        /// <returns>
        /// The button kinds.
        /// </returns>
        public object GetButtonKinds()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Return results.
                result = new
                {
                    Success = true,
                    ButtonKinds = Config.ButtonKinds
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FieldsController>(GetButtonKindsError, ex);
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