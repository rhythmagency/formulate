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
using Formulate.Core.Types;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Filters;
using Umbraco.Extensions;
using Formulate.BackOffice.Utilities;
using Formulate.BackOffice.Utilities.DataValues;
using Formulate.BackOffice.EditorModels.DataValues;

namespace Formulate.BackOffice.Controllers.DataValues
{
    [FormulateBackOfficePluginController]
    public sealed class DataValuesController : FormulateBackOfficeEntityApiController
    {
        private readonly IDataValuesEntityRepository _dataValuesEntityRepository;
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;
        private readonly IGetDataValuesChildEntityOptions _getDataValuesChildEntityOptions;
        private readonly ICreateDataValuesScaffoldingEntity _createDataValuesScaffoldingEntity;

        public DataValuesController(IMapEditorModel mapEditorModel,
            IDataValuesEntityRepository dataValuesEntityRepository,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            DataValuesDefinitionCollection dataValuesDefinitions,
            IGetDataValuesChildEntityOptions getDataValuesChildEntityOptions,
            ICreateDataValuesScaffoldingEntity createDataValuesScaffoldingEntity) : base(mapEditorModel, treeEntityRepository, localizedTextService)
        {
            _dataValuesEntityRepository = dataValuesEntityRepository;
            _dataValuesDefinitions = dataValuesDefinitions;
            _getDataValuesChildEntityOptions = getDataValuesChildEntityOptions;
            _createDataValuesScaffoldingEntity = createDataValuesScaffoldingEntity;
        }
        
        [NonAction]
        public IActionResult GetDefinitionDirective()
        {
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult GetDefinitionDirective(Guid id)
        {
            var definition = _dataValuesDefinitions.FirstOrDefault(id);

            if (definition is null)
            {
                return NotFound();
            }

            return Ok(definition.Directive);
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? kindId, Guid? parentId)
        {
            var parent = TreeEntityRepository.Get(parentId);
            var options = _getDataValuesChildEntityOptions.Get(parent);

            var isValidOption = kindId.HasValue ? options.Any(x => x.EntityType == entityType && x.KindId == kindId) : options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var input = new CreateDataValuesScaffoldingEntityInput()
            {
                EntityType = entityType,
                Parent = parent,
                KindId = kindId,
                RootId = TreeEntityRepository.GetRootId(TreeRootTypes.DataValues)
            };
            var entity = _createDataValuesScaffoldingEntity.Create(input);

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
            var entity = TreeEntityRepository.Get(id);
            var options = _getDataValuesChildEntityOptions.Get(entity);

            return this.Ok(options);
        }

        [HttpPost]
        public ActionResult Save(DataValuesEditorModel model)
        {
            var entity = _mapEditorModel.MapFrom<DataValuesEditorModel, PersistedDataValues>(model);
            _dataValuesEntityRepository.Save(entity);

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
