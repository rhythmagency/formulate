using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Formulate.Core.Types;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Cms.Web.Common.Formatters;
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
        private readonly IValidationEntityRepository _validationEntityRepository;
        private readonly ValidationDefinitionCollection _validationDefinitions;

        public ValidationsController(IValidationEntityRepository validationEntityRepository, ValidationDefinitionCollection validationDefinitions, ITreeEntityRepository treeEntityRepository, ILocalizedTextService localizedTextService) : base(treeEntityRepository, localizedTextService)
        {
            _validationEntityRepository = validationEntityRepository;
            _validationDefinitions = validationDefinitions;
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

            if (entityType == EntityTypes.Validation && kindId.HasValue)
            {
                entity = new PersistedValidation()
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

            var validationOptions = _validationDefinitions.Select(x => new CreateChildEntityOption()
            {
                Name = x.DefinitionLabel,
                KindId = x.KindId,
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

        [NonAction]
        public IActionResult GetDefinitionDirective()
        {
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult GetDefinitionDirective(Guid id)
        {
            var definition = _validationDefinitions.FirstOrDefault(id);

            if (definition is null)
            {
                return NotFound();
            }

            return Ok(definition.Directive);
        }

        [HttpPost]
        public ActionResult Save(SavePersistedValidationRequest request)
        {
            PersistedValidation savedEntity;

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
                    var rootId = TreeEntityRepository.GetRootId(TreeRootTypes.Validations);

                    entityToSavePath.Add(rootId);
                }

                entityToSavePath.Add(entityToSave.Id);
                entityToSave.Path = entityToSavePath.ToArray();

                savedEntity = _validationEntityRepository.Save(entityToSave);
            }
            else
            {
                savedEntity = _validationEntityRepository.Save(request.Entity);
            }

            return Ok(new SavePersistedValidationResponse()
            {
                EntityId = savedEntity.BackOfficeSafeId(),
                EntityPath = savedEntity.TreeSafePath()
            });
        }
    }
}
