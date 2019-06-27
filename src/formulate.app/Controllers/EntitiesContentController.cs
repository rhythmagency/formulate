namespace formulate.app.Controllers
{

    // Namespaces.
    using Helpers;
    using Models.Requests;
    using Persistence;
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
    /// Controller for Formulate entities. This variation can be used in the content
    /// section (e.g., for the form picker).
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate", CoreConstants.Applications.Content)]
    public class EntitiesContentController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetChildrenError = @"An error occurred while attempting to get the children for a Formulate entity.";

        #endregion


        #region Properties

        private IEntityPersistence Entities { get; set; }
        private IEntityHelper EntityHelper { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntitiesContentController(IEntityPersistence entityPersistence, IEntityHelper entityHelper)
        {
            Entities = entityPersistence;
            EntityHelper = entityHelper;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the children of the specified entity.
        /// </summary>
        /// <param name="request">
        /// The request to get the children.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetEntityChildren(
            [FromUri] GetEntityChildrenRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.EntityId);
                var children = Entities.RetrieveChildren(id);


                // Set result.
                result = new
                {
                    Success = true,
                    Children = children
                        .OrderBy(x => x.Name)
                        .Select(x => new
                        {
                            Id = GuidHelper.GetString(x.Id),
                            x.Name,
                            Icon = x.Icon,
                            Kind = EntityHelper.GetString(x.Kind),
                            HasChildren = Entities.RetrieveChildren(x.Id).Any()
                        }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<EntitiesController>(GetChildrenError, ex);
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