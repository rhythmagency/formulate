namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Extensions;
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
    using FormsConstants = formulate.app.Constants.Trees.Forms;


    /// <summary>
    /// Controller for Formulate forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize(CoreConstants.Applications.Users)]
    public class FormsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string GetFormInfoError = @"An error occurred while attempting to get the form info for a Formulate form.";
        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string PersistFormError = @"An error occurred while attempting to persist the Formulate form.";

        #endregion


        #region Properties

        private IFormPersistence Persistence { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public FormsController(UmbracoContext context)
            : base(context)
        {
            Persistence = FormPersistence.Current.Manager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the form info for the specified form.
        /// </summary>
        /// <param name="request">
        /// The request to get the form info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        public object GetFormInfo([FromUri] GetFormInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.FormId);
                var strFormId = GuidHelper.GetString(id);
                var form = Persistence.Retrieve(id);


                // Return results.
                result = new
                {
                    Success = true,
                    FormId = GuidHelper.GetString(form.Id),
                    Alias = form.Alias,
                    Name = form.Name,
                    //TODO: Once nesting is supported, this will need to account for that.
                    Path = new[] { rootId, FormsConstants.Id, strFormId },
                    Fields = form.Fields.MakeSafe().Select(x => new
                    {
                        Id = GuidHelper.GetString(x.Id),
                        x.Alias,
                        x.Name
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FormsController>(GetFormInfoError, ex);
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
        /// Persists a form.
        /// </summary>
        /// <param name="request">
        /// The request to persist a form.
        /// </param>
        /// <returns>
        /// Information about the persisted form.
        /// </returns>
        public object PersistForm(PersistFormRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Parse or create the form ID.
                var formId = string.IsNullOrWhiteSpace(request.FormId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.FormId);


                // Get the fields.
                //TODO: Will have to figure out the field data type to properly instantiate the field.
                var fields = request.Fields.MakeSafe().Select(x => new FormField<string>()
                {
                    Id = string.IsNullOrWhiteSpace(x.Id)
                        ? Guid.NewGuid()
                        : GuidHelper.GetGuid(x.Id),
                    Alias = x.Alias,
                    Name = x.Name
                });


                // Create the form.
                var form = new Form()
                {
                    Id = formId,
                    Alias = request.Alias,
                    Name = request.Name,
                    Fields = fields.ToArray()
                };


                // Persist the form.
                Persistence.Persist(form);


                // Success.
                result = new
                {
                    Success = true,
                    FormId = GuidHelper.GetString(formId)
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FormsController>(PersistFormError, ex);
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