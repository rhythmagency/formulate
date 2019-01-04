namespace formulate.deploy.Controllers
{

    // Namespaces.
    using app.Entities;
    using app.Helpers;
    using app.Persistence;
    using app.Resolvers;
    using Models;
    using System;
    using System.IO;
    using System.Web.Hosting;
    using System.Web.Http;
    using Umbraco.Deploy;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;


    /// <summary>
    /// Controller for working with Umbraco Cloud.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class CloudController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnknownEntityError = @"The specified type of entity is either not supported or was unknown at the time of the initial implementation.";

        #endregion


        #region Properties

        private IEntityPersistence Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CloudController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public CloudController(UmbracoContext context)
            : base(context)
        {
            Entities = EntityPersistence.Current.Manager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Stores the specified entity to Umbraco Cloud.
        /// </summary>
        /// <param name="request">
        /// The request to store the entity.
        /// </param>
        /// <returns>
        /// An object indicating success or failure.
        /// </returns>
        [HttpPost]
        public object StoreEntityToCloud(StoreEntityToCloudRequest request)
        {

            // Variables.
            var id = GuidHelper.GetGuid(request.EntityId);
            var entity = GetEntityCloudInfo(id);


            // Unknown entity type?
            if (!entity.HasValue)
            {
                return new
                {
                    Success = false,
                    Reasons = UnknownEntityError
                };
            }


            // Add file for entity to Umbraco Cloud.
            var entityInfo = entity.Value;
            var service = DeployComponent.SourceControlService;
            service.AddFiles(entityInfo.Item1, entityInfo.Item2, new[] { entityInfo.Item3 });


            // Return result.
            return new
            {
                Success = true
            };

        }

        /// <summary>
        /// Removes the specified entity from Umbraco Cloud.
        /// </summary>
        /// <param name="request">
        /// The request to remove the entity.
        /// </param>
        /// <returns>
        /// An object indicating success or failure.
        /// </returns>
        [HttpPost]
        public object RemoveEntityFromCloud(RemoveEntityFromCloudRequest request)
        {

            // Variables.
            var id = GuidHelper.GetGuid(request.EntityId);
            var entity = GetEntityCloudInfo(id);


            // Unknown entity type?
            if (!entity.HasValue)
            {
                return new
                {
                    Success = false,
                    Reasons = UnknownEntityError
                };
            }


            // Remove file for entity from Umbraco Cloud.
            var entityInfo = entity.Value;
            var service = DeployComponent.SourceControlService;
            service.RemoveFiles(entityInfo.Item1, entityInfo.Item2, new[] { entityInfo.Item3 });


            // Return result.
            return new
            {
                Success = true
            };

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Returns the information necessary to store an entity to Umbraco Cloud.
        /// </summary>
        /// <param name="id">
        /// The GUID ID of the entity.
        /// </param>
        /// <returns>
        /// A tuple containing three pieces of information about the entity:
        ///   * The entity filename.
        ///   * The entitye type.
        ///   * The full path to the entity, including the filename.
        /// </returns>
        private (string, string, string)? GetEntityCloudInfo(Guid id)
        {

            // Variables.
            var entity = Entities.Retrieve(id);
            var config = app.Resolvers.Configuration.Current.Manager;
            var basePath = HostingEnvironment.MapPath(config.JsonBasePath);
            var subfolder = default(string);
            var extension = default(string);


            // Get info about the entity kind.
            switch (entity.Kind)
            {
                case EntityKind.Folder:
                    subfolder = "Folders\\";
                    extension = ".form";
                    break;
                case EntityKind.ConfiguredForm:
                    subfolder = "ConfiguredForms\\";
                    extension = ".conform";
                    break;
                case EntityKind.Layout:
                    subfolder = "Layouts\\";
                    extension = ".layout";
                    break;
                case EntityKind.Validation:
                    subfolder = "Validations\\";
                    extension = ".validation";
                    break;
                case EntityKind.DataValue:
                    subfolder = "DataValues\\";
                    extension = ".dataValue";
                    break;
                case EntityKind.Form:
                    subfolder = "Forms\\";
                    extension = ".form";
                    break;
                default:
                    return null;
            }


            // Variables.
            var strId = GuidHelper.GetString(id);
            var filename = strId + extension;
            var path = Path.Combine(basePath, subfolder, filename);
            var entityType = Enum.GetName(typeof(EntityKind), entity.Kind);


            // Return the information about the entity.
            return (filename, entityType, path);

        }

        #endregion

    }

}