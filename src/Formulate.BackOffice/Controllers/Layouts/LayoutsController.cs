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
    using Formulate.BackOffice.EditorModels.Layouts;

    [FormulateBackOfficePluginController]
    public sealed class LayoutsController : FormulateBackOfficeEntityApiController
    {
        private readonly ILayoutEntityRepository _layoutEntityRepository;

        public LayoutsController(
            IEditorModelMapper editorModelMapper,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            ILayoutEntityRepository layoutEntityRepository
            ) :
                base(editorModelMapper, treeEntityRepository, localizedTextService)
        {
            _layoutEntityRepository = layoutEntityRepository;
        }

        [HttpPost]
        public ActionResult Save(LayoutEditorModel model)
        {
            var entity = _editorModelMapper.MapToEntity<LayoutEditorModel, PersistedLayout>(model);
            _layoutEntityRepository.Save(entity);
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
