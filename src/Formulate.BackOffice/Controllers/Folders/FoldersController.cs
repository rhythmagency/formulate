namespace Formulate.BackOffice.Controllers.Folders
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.EditorModels.Folders;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Utilities;
    using Formulate.Core.Folders;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core.Services;

    /// <summary>
    /// A controller for persisting folders in the Formulate backoffice.
    /// </summary>
    /// <remarks>Unlike other controllers folders is just used for saving and not getting data.</remarks>
    [FormulateBackOfficePluginController]
    public sealed class FoldersController : FormulateBackOfficeEntityApiController
    {
        /// <summary>
        /// The folder entity repository.
        /// </summary>
        private readonly IFolderEntityRepository _folderEntityRepository;
        
        public FoldersController(IEditorModelMapper editorModelMapper, IFolderEntityRepository folderEntityRepository, ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService) : base(editorModelMapper, treeEntityRepository, localizedTextService)
        {
            _folderEntityRepository = folderEntityRepository;
        }

        [HttpPost]
        public ActionResult Save(FolderEditorModel model)
        {
            var entity = _editorModelMapper.MapToEntity<FolderEditorModel, PersistedFolder>(model);
            _folderEntityRepository.Save(entity);

            return Ok(new {
                success = true
            });
        }
    }
}
