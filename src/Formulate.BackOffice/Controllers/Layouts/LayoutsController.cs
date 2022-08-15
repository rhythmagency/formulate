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
using Formulate.Core.Utilities;
using Formulate.BackOffice.Utilities;
using Formulate.BackOffice.Utilities.Layouts;
using Formulate.BackOffice.EditorModels.Layouts;

namespace Formulate.BackOffice.Controllers.Layouts
{
    [FormulateBackOfficePluginController]
    public sealed class LayoutsController : FormulateBackOfficeEntityApiController
    {
        private readonly LayoutDefinitionCollection layoutDefinitions;
        private readonly ILayoutEntityRepository layoutEntities;
        private readonly ICreateLayoutsScaffoldingEntity _createLayoutsScaffoldingEntity;
        private readonly IGetLayoutsChildEntityOptions _getLayoutsChildEntityOptions;

        public LayoutsController(
            IMapEditorModel mapEditorModel,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            LayoutDefinitionCollection layoutDefinitions,
            ILayoutEntityRepository layoutEntities,
            ICreateLayoutsScaffoldingEntity createLayoutsScaffoldingEntity,
            IGetLayoutsChildEntityOptions getLayoutsChildEntityOptions) :
                base(mapEditorModel, treeEntityRepository, localizedTextService)
        {
            this.layoutDefinitions = layoutDefinitions;
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
            var editorModel = _mapEditorModel.MapTo(mapInput);

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
            var entity = _mapEditorModel.MapFrom<LayoutEditorModel, PersistedLayout>(model);
            layoutEntities.Save(entity);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
