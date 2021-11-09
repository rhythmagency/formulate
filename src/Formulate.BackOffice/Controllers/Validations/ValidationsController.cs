using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;

namespace Formulate.BackOffice.Controllers.Validations
{
    /// <summary>
    /// Controller for Formulate validations.
    /// </summary>
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class ValidationsController : FormulateBackOfficeEntityApiController
    {
        private readonly ValidationDefinitionCollection _validationDefinitions;

        public ValidationsController(ValidationDefinitionCollection validationDefinitions, ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService) : base(treeEntityRepository, localizedTextService)
        {
            _validationDefinitions = validationDefinitions;
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
                entity = new PersistedValidation()
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

            var validationOptions = _validationDefinitions.Select(x => new CreateChildEntityOption()
                {
                    Name = x.DefinitionLabel,
                    DefinitionId = x.DefinitionId,
                    EntityType = EntityTypes.Validation,
                    Icon = FormulateValidationsTreeController.Constants.ItemNodeIcon
                }).OrderBy(x => x.Name)
                .ToArray();


            if (id is null)
            {
                options.AddValidationsFolderOption();
                options.AddRange(validationOptions);

                return options;
            }

            var entity = TreeEntityRepository.Get(id.Value);

            if (entity is not PersistedFolder)
            {
                return options;
            }

            options.AddValidationsFolderOption();
            options.AddRange(validationOptions);

            return options;
        }
    }
}
