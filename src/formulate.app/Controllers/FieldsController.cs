namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Helpers;
    using Managers;
    using System;
    using System.Linq;
    using System.Web.Http;

    using formulate.app.CollectionBuilders;
    using formulate.app.Persistence;

    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;


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
        private const string GetFieldCategoriesError = @"An error occurred while attempting to get the Field Categories for a field.";

        #endregion


        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        private IDataValuePersistence DataValues { get; set; }

        private FormFieldTypeCollection FormFieldTypeCollection { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FieldsController(IConfigurationManager configurationManager, IDataValuePersistence dataValuePersistence, FormFieldTypeCollection formFieldTypeCollection)
        {
            Config = configurationManager;
            DataValues = dataValuePersistence;
            FormFieldTypeCollection = formFieldTypeCollection;
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
                // Return results.
                result = new
                {
                    Success = true,
                    FieldTypes = FormFieldTypeCollection.Select(x => new
                    {
                        Icon = x.Icon,
                        TypeLabel = x.TypeLabel,
                        Directive = x.Directive,
                        TypeFullName = x.GetType().AssemblyQualifiedName
                    })
                    .OrderBy(x => x.TypeLabel)
                    .ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FieldsController>(ex, GetFieldTypesError);
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
                Logger.Error<FieldsController>(ex, GetButtonKindsError);
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
        /// Returns the categories of fields that can be selected when adding a field in the form designer.
        /// </summary>
        /// <returns>
        /// The field categories.
        /// </returns>
        public object GetFieldCategories()
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
                    FieldCategories = Config.FieldCategories
                        .Select(x => new
                        {
                            Kind = x.Kind,
                            Group = x.Group
                        })
                        .OrderBy(x => string.IsNullOrWhiteSpace(x.Group) ? 0 : 1)
                        .OrderBy(x => x.Group)
                        .ThenBy(x => x.Kind)
                        .ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FieldsController>(ex, GetFieldCategoriesError);
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