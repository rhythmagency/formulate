namespace Formulate.BackOffice.Controllers.ConfiguredForms
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Core.Services;

    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.EditorModels.ConfiguredForm;
    using Formulate.Core.ConfiguredForms;

    [FormulateBackOfficePluginController]
    public sealed class ConfiguredFormsController : FormulateBackOfficeEntityApiController
    {
        private readonly IConfiguredFormEntityRepository _configuredFormEntityRepository;

        public ConfiguredFormsController(
            IConfiguredFormEntityRepository configuredFormEntityRepository,
            IMapEditorModel mapEditorModel,
            ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService) : base(mapEditorModel, treeEntityRepository, localizedTextService)
        {
            _configuredFormEntityRepository = configuredFormEntityRepository;
        }

        [HttpPost]
        public ActionResult Save(ConfiguredFormEditorModel model)
        {
            var entity = _mapEditorModel.MapFrom<ConfiguredFormEditorModel, PersistedConfiguredForm>(model);
            _configuredFormEntityRepository.Save(entity);

            return Ok(new
            {
                success = true
            });
        }
    }
}
