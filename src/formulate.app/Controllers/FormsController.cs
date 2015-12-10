namespace formulate.app.Controllers
{

    // Namespaces.
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
    /// Controller for Formulate forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize(CoreConstants.Applications.Users)]
    public class FormsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string GetFieldsError = @"An error occurred while attempting to get the fields for a Formulate form.";
        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";

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

        public object GetFields([FromUri] GetFormFieldsRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.FormId);
                var form = Persistence.Retrieve(id);


                // Return results.
                result = new
                {
                    Success = true,
                    Fields = form.Fields.Select(x => new
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
                LogHelper.Error<FormsController>(GetFieldsError, ex);
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