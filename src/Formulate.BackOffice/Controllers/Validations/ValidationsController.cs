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

using Umbraco.Extensions;
using Formulate.BackOffice.Utilities.Validations;
using Formulate.BackOffice.EditorModels.Validation;

namespace Formulate.BackOffice.Controllers.Validations
{
    /// <summary>
    /// Controller for Formulate validations.
    /// </summary>
    [FormulateBackOfficePluginController]
    public sealed class ValidationsController : FormulateBackOfficeEntityApiController
    {
        private readonly IValidationEntityRepository _validationEntityRepository;
        private readonly ValidationDefinitionCollection _validationDefinitions;
        private readonly ICreateValidationsScaffoldingEntity _createValidationsScaffoldingEntity;
        private readonly IGetValidationsChildEntityOptions _getValidationsChildEntityOptions;

        public ValidationsController(IMapEditorModel mapEditorModel,
            IValidationEntityRepository validationEntityRepository, 
            ValidationDefinitionCollection validationDefinitions, 
            ITreeEntityRepository treeEntityRepository, 
            ILocalizedTextService localizedTextService,
            ICreateValidationsScaffoldingEntity createValidationsScaffoldingEntity,
            IGetValidationsChildEntityOptions getValidationsChildEntityOptions) : base(mapEditorModel, treeEntityRepository, localizedTextService)
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

            var mapInput = new MapToEditorModelInput(entity, true);
            var editorModel = _mapEditorModel.MapTo(mapInput);

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
        public ActionResult Save(ValidationEditorModel model)
        {
            var entity = _mapEditorModel.MapFrom<ValidationEditorModel, PersistedValidation>(model);
            _validationEntityRepository.Save(entity);

            return Ok(new {
                success = true
            });
        }
    }
}
