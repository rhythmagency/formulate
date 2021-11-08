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
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;

namespace Formulate.BackOffice.Controllers.DataValues
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class DataValuesController : FormulateBackOfficeEntityApiController
    {
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;

        public DataValuesController(ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService, DataValuesDefinitionCollection dataValuesDefinitions) : base(treeEntityRepository, localizedTextService)
        {
            _dataValuesDefinitions = dataValuesDefinitions;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? definitionId, Guid? parentId)
        {
            var options = this.GetCreateOptions(parentId);

            var isValidOption = definitionId.HasValue ? options.Any(x => x.EntityType == entityType && x.DefinitionId == definitionId) : options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var parent = parentId.HasValue ? TreeEntityRepository.Get(parentId.Value) : default;
            IPersistedEntity entity = null;

            if (entityType == EntityTypes.DataValues && definitionId.HasValue)
            {
                entity = new PersistedDataValues()
                {
                    DefinitionId = definitionId.Value,
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

            var dataValueOptions = _dataValuesDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                DefinitionId = x.DefinitionId,
                EntityType = EntityTypes.DataValues,
                Icon = x.Icon
            }).OrderBy(x => x.Name)
              .ToArray();


            if (id is null)
            {
                options.AddDataValuesFolderOption();
                options.AddRange(dataValueOptions);

                return options;
            }

            var entity = TreeEntityRepository.Get(id.Value);

            if (entity is not PersistedFolder)
            {
                return options;
            }

            options.AddDataValuesFolderOption();
            options.AddRange(dataValueOptions);

            return options;
        }
    }
}
