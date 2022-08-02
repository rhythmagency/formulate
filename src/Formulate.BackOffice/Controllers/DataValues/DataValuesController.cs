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

namespace Formulate.BackOffice.Controllers.DataValues
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class DataValuesController : FormulateBackOfficeEntityApiController
    {
        private readonly IDataValuesEntityRepository _dataValuesEntityRepository;
        private readonly DataValuesDefinitionCollection _dataValuesDefinitions;
        private readonly IGetDataValuesChildEntityOptions _getDataValuesChildEntityOptions;
        private readonly ICreateDataValuesScaffoldingEntity _createDataValuesScaffoldingEntity;

        public DataValuesController(IBuildEditorModel buildEditorModel,
            IDataValuesEntityRepository dataValuesEntityRepository,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            DataValuesDefinitionCollection dataValuesDefinitions,
            IGetDataValuesChildEntityOptions getDataValuesChildEntityOptions,
            ICreateDataValuesScaffoldingEntity createDataValuesScaffoldingEntity) : base(buildEditorModel, treeEntityRepository, localizedTextService)
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
                var legacyId = Guid.Parse("bbf66f6a-8f7d-4aba-9d5b-194a46084ec2");
                if (id == legacyId)
                {
                    return Ok("formulate-legacy-data-value");
                }
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

            var buildInput = new BuildEditorModelInput(entity, true);
            var editorModel = _buildEditorModel.Build(buildInput);

            return Ok(editorModel);
        }

        [HttpGet]
        public IActionResult  GetCreateOptions(Guid? id)
        {
            var entity = TreeEntityRepository.Get(id);
            var options = _getDataValuesChildEntityOptions.Get(entity);

            return this.Ok(options);
        }

        [HttpPost]
        public ActionResult Save(SavePersistedDataValuesRequest request)
        {
            PersistedDataValues savedEntity;

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
                    var rootId = TreeEntityRepository.GetRootId(TreeRootTypes.DataValues);

                    entityToSavePath.Add(rootId);
                }

                entityToSavePath.Add(entityToSave.Id);
                entityToSave.Path = entityToSavePath.ToArray();

                savedEntity = _dataValuesEntityRepository.Save(entityToSave);
            }
            else
            {
                savedEntity = _dataValuesEntityRepository.Save(request.Entity);
            }

            return Ok(new SavePersistedDataValuesResponse()
            {
                EntityId = savedEntity.BackOfficeSafeId(),
                EntityPath = savedEntity.TreeSafePath()
            });
        }
    }
}
