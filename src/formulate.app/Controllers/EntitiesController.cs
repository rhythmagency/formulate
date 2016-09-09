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
    /// Controller for Formulate entities.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class EntitiesController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetChildrenError = @"An error occurred while attempting to get the children for a Formulate entity.";
        private const string GetEntityError = @"An error occurred while attempting to get the information for a Formulate entity.";

        #endregion


        #region Properties

        private IEntityPersistence Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntitiesController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public EntitiesController(UmbracoContext context)
            : base(context)
        {
            Entities = EntityPersistence.Current.Manager;
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
                    Children = children.Select(x => new
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
                LogHelper.Error<EntitiesController>(GetChildrenError, ex);
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
                LogHelper.Error<EntitiesController>(GetEntityError, ex);
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