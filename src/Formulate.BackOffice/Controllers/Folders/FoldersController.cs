using System;
using System.Collections.Generic;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.Core.Folders;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace Formulate.BackOffice.Controllers.Folders
{
    /// <summary>
    /// A controller for persisting folders in the Formulate backoffice.
    /// </summary>
    /// <remarks>Unlike other controllers folders is just used for saving and not getting data.</remarks>
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FoldersController : FormulateBackOfficeEntityApiController
    {
        /// <summary>
        /// The folder entity repository.
        /// </summary>
        private readonly IFolderEntityRepository _folderEntityRepository;
        
        public FoldersController(IFolderEntityRepository folderEntityRepository, ITreeEntityRepository treeEntityRepository) : base(treeEntityRepository)
        {
            _folderEntityRepository = folderEntityRepository;
        }

        [HttpPost]
        public ActionResult Save(SavePersistedFolderRequest request)
        {
            PersistedFolder savedEntity;

            if (request.Entity.Id == Guid.Empty)
            {
                var entityToSave = request.Entity;
                var entityToSavePath = new List<Guid>();
                var parent = request.ParentId.HasValue ? TreeEntityRepository.Get(request.ParentId.Value) : default;

                entityToSave.Id = Guid.NewGuid();

                if (parent is not null)
                {
                    entityToSavePath.AddRange(parent.Path);
                }
                else
                {
                    var rootId = TreeEntityRepository.GetRootId(request.TreeType);

                    entityToSavePath.Add(rootId);
                }
                
                entityToSavePath.Add(entityToSave.Id);
                entityToSave.Path = entityToSavePath.ToArray();

                savedEntity = _folderEntityRepository.Save(entityToSave);
            }
            else
            {
                savedEntity = _folderEntityRepository.Save(request.Entity);
            }

            return Ok(new SavePersistedFolderResponse()
            {
                EntityId = savedEntity.BackOfficeSafeId(),
                EntityPath = savedEntity.TreeSafePath()
            });
        }
    }
}
