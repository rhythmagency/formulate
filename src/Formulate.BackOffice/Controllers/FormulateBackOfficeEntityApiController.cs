using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;

namespace Formulate.BackOffice.Controllers
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public abstract partial class FormulateBackOfficeEntityApiController : UmbracoAuthorizedApiController
    {
        protected readonly ITreeEntityRepository TreeEntityRepository;

        private readonly ILocalizedTextService _localizedTextService;

        protected FormulateBackOfficeEntityApiController(ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService)
        {
            TreeEntityRepository = treeEntityRepository;
            _localizedTextService = localizedTextService;
        }

        [HttpGet]
        public virtual IActionResult Get(Guid id)
        {
            var entity = TreeEntityRepository.Get(id);

            if (entity is null)
            {
                return NotFound();
            }

            var response = new GetEntityResponse()
            {
                Entity = entity,
                EntityType = entity.EntityType(),
                TreePath = entity.TreeSafePath()
            };

            return Ok(response);
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
