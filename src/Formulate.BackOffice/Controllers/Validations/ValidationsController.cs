using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.BackOffice.Utilities;
using Formulate.Core.Folders;
using Formulate.Core.Persistence;
using Formulate.Core.Types;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;
using Formulate.BackOffice.Utilities.Validations;

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
        private readonly ICreateValidationsScaffoldingEntity _createValidationsScaffoldingEntity;
        private readonly IGetValidationsChildEntityOptions _getValidationsChildEntityOptions;

        public ValidationsController(IBuildEditorModel buildEditorModel,
            IValidationEntityRepository validationEntityRepository, 
            ValidationDefinitionCollection validationDefinitions, 
            ITreeEntityRepository treeEntityRepository, 
            ILocalizedTextService localizedTextService,
            ICreateValidationsScaffoldingEntity createValidationsScaffoldingEntity,
            IGetValidationsChildEntityOptions getValidationsChildEntityOptions) : base(buildEditorModel, treeEntityRepository, localizedTextService)
        {
            _validationEntityRepository = validationEntityRepository;
            _validationDefinitions = validationDefinitions;
            _createValidationsScaffoldingEntity = createValidationsScaffoldingEntity;
            _getValidationsChildEntityOptions = getValidationsChildEntityOptions;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? kindId, Guid? parentId)
        {
            var parent = TreeEntityRepository.Get(parentId);
            var options = _getValidationsChildEntityOptions.Get(parent);
            var isValidOption = kindId.HasValue ? options.Any(x => x.EntityType == entityType && x.KindId == kindId) : options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var input = new CreateValidationsScaffoldingEntityInput()
            {
                EntityType = entityType,
                KindId = kindId,
                Parent = parent,
                RootId = TreeEntityRepository.GetRootId(TreeRootTypes.Validations)
            };
            var entity = _createValidationsScaffoldingEntity.Create(input);

            if (entity is null)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var buildInput = new BuildEditorModelInput(entity, true);
            var editorModel = _buildEditorModel.Build(buildInput);

            return Ok(editorModel);
        }

        [HttpGet]
        public IActionResult GetCreateOptions(Guid? id)
        {
            var parent = TreeEntityRepository.Get(id);
            var options = _getValidationsChildEntityOptions.Get(parent);
                        
            return Ok(options);
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
