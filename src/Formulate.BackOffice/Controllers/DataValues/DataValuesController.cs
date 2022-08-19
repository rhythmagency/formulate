namespace Formulate.BackOffice.Controllers.DataValues
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Formulate.Core.DataValues;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Services;

    using Umbraco.Extensions;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.DataValues;
    using Formulate.BackOffice.EditorModels.DataValues;
    using Formulate.BackOffice.Utilities.CreateOptions.DataValues;

    [FormulateBackOfficePluginController]
    public sealed class DataValuesController : FormulateBackOfficeEntityApiController
    {
        private readonly IDataValuesEntityRepository _dataValuesEntityRepository;
        private readonly IGetDataValuesChildEntityOptions _getDataValuesChildEntityOptions;
        private readonly ICreateDataValuesScaffoldingEntity _createDataValuesScaffoldingEntity;

        public DataValuesController(IEditorModelMapper editorModelMapper,
            IDataValuesEntityRepository dataValuesEntityRepository,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            IGetDataValuesChildEntityOptions getDataValuesChildEntityOptions,
            ICreateDataValuesScaffoldingEntity createDataValuesScaffoldingEntity) : base(editorModelMapper, treeEntityRepository, localizedTextService)
        {
            _dataValuesEntityRepository = dataValuesEntityRepository;
            _getDataValuesChildEntityOptions = getDataValuesChildEntityOptions;
            _createDataValuesScaffoldingEntity = createDataValuesScaffoldingEntity;
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
                RootId = TreeEntityRepository.GetRootId(TreeTypes.DataValues)
            };
            var entity = _createDataValuesScaffoldingEntity.Create(input);

            if (entity is null)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var mapInput = new MapToEditorModelInput(entity, true);
            var editorModel = _editorModelMapper.MapToEditor(mapInput);

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
            var entity = _editorModelMapper.MapToEntity<DataValuesEditorModel, PersistedDataValues>(model);
            _dataValuesEntityRepository.Save(entity);

            return Ok(new
            {
                Success = true,
            });
        }
    }
}
