namespace Formulate.BackOffice.Controllers.Layouts
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Formulate.Core.Layouts;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Services;

    using Umbraco.Extensions;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.Layouts;
    using Formulate.BackOffice.EditorModels.Layouts;

    [FormulateBackOfficePluginController]
    public sealed class LayoutsController : FormulateBackOfficeEntityApiController
    {
        private readonly ILayoutEntityRepository layoutEntities;
        private readonly ICreateLayoutsScaffoldingEntity _createLayoutsScaffoldingEntity;
        private readonly IGetLayoutsChildEntityOptions _getLayoutsChildEntityOptions;

        public LayoutsController(
            IEditorModelMapper editorModelMapper,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            ILayoutEntityRepository layoutEntities,
            ICreateLayoutsScaffoldingEntity createLayoutsScaffoldingEntity,
            IGetLayoutsChildEntityOptions getLayoutsChildEntityOptions) :
                base(editorModelMapper, treeEntityRepository, localizedTextService)
        {
            this.layoutEntities = layoutEntities;
            _createLayoutsScaffoldingEntity = createLayoutsScaffoldingEntity;
            _getLayoutsChildEntityOptions = getLayoutsChildEntityOptions;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? kindId, Guid? parentId)
        {
            var parent = TreeEntityRepository.Get(parentId);
            var options = _getLayoutsChildEntityOptions.Get(parent);

            var isValidOption = kindId.HasValue ? options.Any(x => x.EntityType == entityType && x.KindId == kindId) : options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var input = new CreateLayoutsScaffoldingEntityInput()
            {
                EntityType = entityType,
                KindId = kindId,
                Parent = parent,
                RootId = TreeEntityRepository.GetRootId(TreeRootTypes.Layouts)
            };
            var entity = _createLayoutsScaffoldingEntity.Create(input);

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
            var parent = TreeEntityRepository.Get(id);
            var options = _getLayoutsChildEntityOptions.Get(parent);

            return Ok(options);
        }

        [HttpPost]
        public ActionResult Save(LayoutEditorModel model)
        {
            var entity = _editorModelMapper.MapToEntity<LayoutEditorModel, PersistedLayout>(model);
            layoutEntities.Save(entity);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
