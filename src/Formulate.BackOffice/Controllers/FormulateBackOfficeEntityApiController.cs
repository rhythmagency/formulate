namespace Formulate.BackOffice.Controllers
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Formulate.BackOffice.Utilities;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Web.BackOffice.Controllers;

    using Umbraco.Extensions;

    [FormulateBackOfficePluginController]
    public abstract partial class FormulateBackOfficeEntityApiController : UmbracoAuthorizedApiController
    {
        protected readonly ITreeEntityRepository TreeEntityRepository;

        protected readonly IEditorModelMapper _editorModelMapper;

        private readonly ILocalizedTextService _localizedTextService;

        protected FormulateBackOfficeEntityApiController(IEditorModelMapper editorModelMapper, ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService)
        {
            TreeEntityRepository = treeEntityRepository;
            _localizedTextService = localizedTextService;
            _editorModelMapper = editorModelMapper;
        }

        /// <summary>
        /// Returns the response containing the data for the entity
        /// with the specified ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the entity.
        /// </param>
        /// <returns>
        /// The response contining the data for the entity, or a response
        /// indicating the entity could not be found.
        /// </returns>
        [HttpGet]
        public virtual IActionResult Get(Guid id)
        {
            var entity = TreeEntityRepository.Get(id);

            // Data not found?
            if (entity == null)
            {
                return NotFound();
            }

            var mapInput = new MapToEditorModelInput(entity, false);
            var editorModel = _editorModelMapper.MapToEditor(mapInput);

            // Return the response with the data.
            return Ok(editorModel);
        }

        [HttpGet]
        public virtual IActionResult Delete(Guid id)
        {
            var entity = TreeEntityRepository.Get(id);

            if (entity is null)
            {
                return NotFound();
            }

            var deletedEntityIds = TreeEntityRepository.Delete(entity);
            
            var response = new DeleteEntityResponse()
            {
                DeletedEntityIds = deletedEntityIds.Select(x=>x.ToString("N")).ToArray(),
                ParentPath = entity.TreeSafeParentPath()
            };

            return Ok(response);
        }

        [HttpPost]
        public virtual IActionResult Move(MoveEntityRequest request)
        {
            var rootId = TreeEntityRepository.GetRootId(request.TreeType);
            var entity = TreeEntityRepository.Get(request.EntityId);
            var parentPath = new List<Guid>();

            if (request.ParentId.HasValue)
            {
                var parent = TreeEntityRepository.Get(request.ParentId.Value);

                if (parent is not null)
                {
                    var isParentADescendant = parent.Path.Contains(entity.Id);

                    if (isParentADescendant)
                    {
                        var notificationModel = new SimpleNotificationModel();
                        notificationModel.AddErrorNotification(_localizedTextService.Localize("moveOrCopy", "notAllowedByPath"), "");

                        return ValidationProblem(notificationModel);
                    }

                    parentPath.AddRange(parent.Path);
                }
            }

            if (parentPath.Any() == false)
            {
                parentPath.Add(rootId);
            }

            var newEntityPath = TreeEntityRepository.Move(entity, parentPath.ToArray());

            var treeSafePath = string.Join(",", newEntityPath.Select(x => x == rootId ? "-1" : x.ToString("N")));

            return Ok(treeSafePath);
        }
    }
}