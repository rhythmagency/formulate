using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.Core.Layouts;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;

namespace Formulate.BackOffice.Controllers.Layouts
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class LayoutsController : FormulateBackOfficeEntityApiController
    {
        private readonly LayoutDefinitionCollection _layoutDefinitions;

        public LayoutsController(ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService, LayoutDefinitionCollection layoutDefinitions) : base(treeEntityRepository, localizedTextService)
        {
            _layoutDefinitions = layoutDefinitions;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? kindId, Guid? parentId)
        {
            var options = this.GetCreateOptions(parentId);

            var isValidOption = kindId.HasValue ? options.Any(x => x.EntityType == entityType && x.KindId == kindId) : options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var parent = parentId.HasValue ? TreeEntityRepository.Get(parentId.Value) : default;
            IPersistedEntity entity = null;

            if (entityType == EntityTypes.Layout && kindId.HasValue)
            {
                entity = new PersistedLayout()
                {
                    KindId = kindId.Value,
                };
            }
            else if (entityType == EntityTypes.Folder)
            {
                entity = new PersistedFolder();
            }

            if (entity is null)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var response = new GetEntityResponse()
            {
                Entity = entity,
                EntityType = entityType,
                TreePath = parent.TreeSafePath(),
            };

            return Ok(response);
        }

        [HttpGet]
        public IEnumerable<CreateChildEntityOption> GetCreateOptions(Guid? id)
        {
            var options = new List<CreateChildEntityOption>();

            var layoutOptions = _layoutDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                KindId = x.KindId,
                EntityType = EntityTypes.Layout,
                Icon = FormulateLayoutsTreeController.Constants.ItemNodeIcon
            }).OrderBy(x => x.Name)
              .ToArray();


            if (id is null)
            {
                options.AddLayoutsFolderOption();
                options.AddRange(layoutOptions);

                return options;
            }

            var entity = TreeEntityRepository.Get(id.Value);

            if (entity is not PersistedFolder)
            {
                return options;
            }

            options.AddLayoutsFolderOption();
            options.AddRange(layoutOptions);

            return options;
        }
    }
}
