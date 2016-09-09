namespace formulate.app.Controllers
{

    // Namespaces.
    using Folders;
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
    [UmbracoApplicationAuthorize("formulate")]
    public class FoldersController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string PersistFolderError = @"An error occurred while attempting to persist the Formulate folder.";
        private const string GetFolderInfoError = @"An error occurred while attempting to get the folder info for a Formulate folder.";
        private const string MoveFolderError = @"An error occurred while attempting to move a Formulate folder.";
        private const string FolderUnderItself = @"A Formulate folder cannot be moved under itself.";
        private const string DeleteFolderError = @"An error occurred while attempting to delete the Formulate folder.";

        #endregion


        #region Properties

        private IFolderPersistence Persistence { get; set; }
        private IEntityPersistence Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FoldersController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public FoldersController(UmbracoContext context)
            : base(context)
        {
            Persistence = FolderPersistence.Current.Manager;
            Entities = EntityPersistence.Current.Manager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns info about the folder with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the folder info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetFolderInfo([FromUri] GetFolderInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.FolderId);
                var folder = Persistence.Retrieve(id);
                var partialPath = folder.Path
                    .Select(x => GuidHelper.GetString(x));
                var fullPath = new[] { rootId }
                    .Concat(partialPath)
                    .ToArray();


                // Set result.
                result = new
                {
                    Success = true,
                    FolderId = GuidHelper.GetString(folder.Id),
                    Path = fullPath,
                    Name = folder.Name
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FoldersController>(GetFolderInfoError, ex);
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
        /// Persists a folder.
        /// </summary>
        /// <param name="request">
        /// The request to persist a folder.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// folder data.
        /// </returns>
        [HttpPost]
        public object PersistFolder(PersistFolderRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Parse or create the folder ID.
                var folderId = string.IsNullOrWhiteSpace(request.FolderId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.FolderId);


                // Get path.
                var parentId = GuidHelper.GetGuid(request.ParentId);
                var parent = Entities.Retrieve(parentId);
                var path = parent.Path
                    .Concat(new[] { folderId }).ToArray();


                // Create the folder.
                var folder = new Folder()
                {
                    Id = folderId,
                    Path = path,
                    Name = request.FolderName
                };


                // Persist the folder.
                Persistence.Persist(folder);


                // Success.
                result = new
                {
                    Success = true,
                    FolderId = GuidHelper.GetString(folderId),
                    Path = path.Select(x => GuidHelper.GetString(x))
                        .ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FoldersController>(PersistFolderError, ex);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return the result.
            return result;

        }


        /// <summary>
        /// Moves folder to a new parent.
        /// </summary>
        /// <param name="request">
        /// The request to move the folder.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about the folder.
        /// </returns>
        [HttpPost]
        public object MoveFolder(MoveFolderRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var parentId = GuidHelper.GetGuid(request.NewParentId);


            // Catch all errors.
            try
            {

                // Declare list of anonymous type.
                var savedDescendants = new[]
                {
                    new
                    {
                        Id = string.Empty,
                        Path = new string[] { }
                    }
                }.Take(0).ToList();


                // Variables.
                var folderId = GuidHelper.GetGuid(request.FolderId);
                var parentPath = Entities.Retrieve(parentId).Path;


                // Get folder and descendants.
                var folder = Persistence.Retrieve(folderId);
                var descendants = Entities.RetrieveDescendants(folderId);


                // Check if destination folder is under current folder.
                var oldFolderPath = folder.Path;
                if (parentPath.Any(x => x == folderId))
                {
                    result = new
                    {
                        Success = false,
                        Reason = FolderUnderItself
                    };
                    return result;
                }


                // Move folder and descendants.
                var oldParentPath = oldFolderPath.Take(oldFolderPath.Length - 1).ToArray();
                var path = EntityHelper.GetClientPath(Entities.MoveEntity(folder, parentPath));
                foreach (var descendant in descendants)
                {
                    var descendantParentPath = descendant.Path.Take(descendant.Path.Length - 1);
                    var descendantPathEnd = descendantParentPath.Skip(oldParentPath.Length);
                    var newParentPath = parentPath.Concat(descendantPathEnd).ToArray();
                    var clientPath = EntityHelper.GetClientPath(
                        Entities.MoveEntity(descendant, newParentPath));
                    savedDescendants.Add(new
                    {
                        Id = GuidHelper.GetString(descendant.Id),
                        Path = clientPath
                    });
                }


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(folderId),
                    Path = path,
                    Descendants = savedDescendants.ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FoldersController>(MoveFolderError, ex);
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
        /// Deletes the foldfer with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to delete the folder.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost()]
        public object DeleteFolder(DeleteFolderRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var folderId = GuidHelper.GetGuid(request.FolderId);
                var descendants = Entities.RetrieveDescendants(folderId);


                // Delete the folder.
                Persistence.Delete(folderId);


                // Delete the descendants.
                foreach (var descendant in descendants)
                {
                    Entities.DeleteEntity(descendant);
                }


                // Success.
                result = new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FoldersController>(DeleteFolderError, ex);
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