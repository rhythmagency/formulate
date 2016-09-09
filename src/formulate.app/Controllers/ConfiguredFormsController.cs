namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Helpers;
    using Models.Requests;
    using Persistence;
    using Resolvers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;


    /// <summary>
    /// Controller for Formulate configured forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class ConfiguredFormsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string PersistConFormError = @"An error occurred while attempting to persist a Formulate configured form.";
        private const string GetConFormInfoError = @"An error occurred while attempting to get the configured form info for a Formulate configured form.";
        private const string DeleteConFormError = @"An error occurred while attempting to delete the Formulate configured form.";
        private const string FormNotFoundError = @"The configured Formulate form requested could not be found.";

        #endregion


        #region Properties

        private IConfiguredFormPersistence Persistence { get; set; }
        private IEntityPersistence Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfiguredFormsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public ConfiguredFormsController(UmbracoContext context)
            : base(context)
        {
            Persistence = ConfiguredFormPersistence.Current.Manager;
            Entities = EntityPersistence.Current.Manager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Creates a configured form.
        /// </summary>
        /// <param name="request">
        /// The request to create the configured form.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost]
        public object PersistConfiguredForm(PersistConfiguredFormRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var parentId = GuidHelper.GetGuid(request.ParentId);


            // Catch all errors.
            try
            {

                // Parse or create the configured form ID.
                var conFormId = string.IsNullOrWhiteSpace(request.ConFormId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.ConFormId);


                // Get the ID path.
                var parent = Entities.Retrieve(parentId);
                var path = parent.Path.Concat(new[] { conFormId }).ToArray();


                // Create configured form.
                var configuredForm = new ConfiguredForm()
                {
                    Id = conFormId,
                    Path = path,
                    Name = request.Name,
                    TemplateId = string.IsNullOrWhiteSpace(request.TemplateId)
                        ? null as Guid?
                        : GuidHelper.GetGuid(request.TemplateId),
                    LayoutId = string.IsNullOrWhiteSpace(request.LayoutId)
                        ? null as Guid?
                        : GuidHelper.GetGuid(request.LayoutId)
                };


                // Persist configured form.
                Persistence.Persist(configuredForm);


                // Variables.
                var fullPath = new[] { rootId }
                    .Concat(path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(conFormId),
                    Path = fullPath
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ConfiguredFormsController>(PersistConFormError, ex);
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
        /// Returns info about the configured form with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the configured form info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetConfiguredFormInfo([FromUri] GetConfiguredFormInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.ConFormId);
                var configuredForm = Persistence.Retrieve(id);


                // Check for a null configured form.
                if (configuredForm == null)
                {
                    result = new
                    {
                        Success = false,
                        Reason = FormNotFoundError
                    };
                    return result;
                }


                // Variables.
                var partialPath = configuredForm.Path
                    .Select(x => GuidHelper.GetString(x));
                var fullPath = new[] { rootId }
                    .Concat(partialPath)
                    .ToArray();


                // Set result.
                result = new
                {
                    Success = true,
                    ConFormId = GuidHelper.GetString(configuredForm.Id),
                    Path = fullPath,
                    Name = configuredForm.Name,
                    LayoutId = configuredForm.LayoutId.HasValue
                        ? GuidHelper.GetString(configuredForm.LayoutId.Value)
                        : null,
                    TemplateId = configuredForm.TemplateId.HasValue
                        ? GuidHelper.GetString(configuredForm.TemplateId.Value)
                        : null
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ConfiguredFormsController>(GetConFormInfoError, ex);
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
        /// Deletes the configured form with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to delete the configured form.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost()]
        public object DeleteConfiguredForm(DeleteConfiguredFormRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var conFormId = GuidHelper.GetGuid(request.ConFormId);


                // Delete the configured form.
                Persistence.Delete(conFormId);


                // Success.
                result = new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ConfiguredFormsController>(DeleteConFormError, ex);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return the result.
            return result;

        }

        #endregion

    }

}