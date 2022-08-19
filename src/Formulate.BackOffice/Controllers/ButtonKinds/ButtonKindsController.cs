namespace Formulate.BackOffice.Controllers.Templates
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities.EditorModels.Buttons;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Web.BackOffice.Controllers;

    [FormulateBackOfficePluginController]
    public sealed class ButtonKindsController : UmbracoAuthorizedApiController
    {
        private readonly IGetButtonKindEditorModels _getButtonKindEditorModels;

        public ButtonKindsController(IGetButtonKindEditorModels getButtonKindEditorModels)
        {
            _getButtonKindEditorModels = getButtonKindEditorModels;
        }

        /// <summary>
        /// Returns any configured button kinds.
        /// </summary>
        /// <returns>
        /// The array of configured button kinds.
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var options = _getButtonKindEditorModels.GetAll();

            return Ok(options);
        }
    }
}
