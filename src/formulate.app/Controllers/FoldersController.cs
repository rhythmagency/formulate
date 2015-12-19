namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Extensions;
    using Forms;
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
    using FormsConstants = formulate.app.Constants.Trees.Forms;


    /// <summary>C:\r\formulate\src\formulate.app\Handlers\ApplicationStartingHandler.cs
    /// Controller for Formulate forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize(CoreConstants.Applications.Users)]
    public class FoldersController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string CreateFolderError = @"An error occurred while attempting to create the Formulate folder.";

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
        /// Creates a folder.
        /// </summary>
        /// <param name="request">
        /// The request to create a folder.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with the
        /// folder ID.
        /// </returns>
        [HttpPost]
        public object CreateFolder(CreateFolderRequest request)
        {

            // Variables.
            var result = default(object);
            var folderId = Guid.NewGuid();


            // Catch all errors.
            try
            {

                // Get path.
                var parentId = GuidHelper.GetGuid(request.ParentId);
                var parent = Entities.Retrieve(parentId);
                var path = parent.Path.Concat(new[] { folderId }).ToArray();


                // Create the folder.
                var form = new Folder()
                {
                    Id = folderId,
                    Path = path,
                    Name = request.FolderName
                };


                // Persist the folder.
                Persistence.Persist(form);


                // Success.
                result = new
                {
                    Success = true,
                    FolderId = GuidHelper.GetString(folderId)
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<FoldersController>(CreateFolderError, ex);
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