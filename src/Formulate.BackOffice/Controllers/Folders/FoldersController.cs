using System;
using System.Collections.Generic;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.EditorModels.Folders;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.BackOffice.Utilities;
using Formulate.Core.Folders;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace Formulate.BackOffice.Controllers.Folders
{
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
        
        public FoldersController(IMapEditorModel mapEditorModel, IFolderEntityRepository folderEntityRepository, ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService) : base(mapEditorModel, treeEntityRepository, localizedTextService)
        {
            _folderEntityRepository = folderEntityRepository;
        }

        [HttpPost]
        public ActionResult Save(FolderEditorModel model)
        {
            var entity = _mapEditorModel.MapFrom<FolderEditorModel, PersistedFolder>(model);
            _folderEntityRepository.Save(entity);

            return Ok(new {
                success = true
            });
        }
    }
}
