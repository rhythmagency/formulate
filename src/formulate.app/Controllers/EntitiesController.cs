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
    /// Controller for Formulate entities.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class EntitiesController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetEntityError = @"An error occurred while attempting to get the information for a Formulate entity.";

        #endregion


        #region Properties

        private IEntityPersistence Entities { get; set; }
        private IEntityHelper EntityHelper { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntitiesController(IEntityPersistence entityPersistence, IEntityHelper entityHelper)
        {
            Entities = entityPersistence;
            EntityHelper = entityHelper;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the form info for the specified entity.
        /// </summary>
        /// <param name="request">
        /// The request to get the entity info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetEntity([FromUri] GetEntityRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.EntityId);
                var entity = Entities.Retrieve(id);
                var partialPath = entity.Path
                    .Select(x => GuidHelper.GetString(x));
                var fullPath = new[] { rootId }
                    .Concat(partialPath)
                    .ToArray();


                // Set result.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(entity.Id),
                    Path = fullPath,
                    Name = entity.Name,
                    Icon = entity.Icon,
                    Kind = EntityHelper.GetString(entity.Kind),
                    HasChildren = Entities.RetrieveChildren(entity.Id).Any()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<EntitiesController>(ex, GetEntityError);
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