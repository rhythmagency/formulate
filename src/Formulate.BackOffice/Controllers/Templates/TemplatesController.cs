namespace Formulate.BackOffice.Controllers.Templates
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities.EditorModels.Templates;
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Web.BackOffice.Controllers;

    [FormulateBackOfficePluginController]
    public sealed class TemplatesController : UmbracoAuthorizedApiController
    {
        private readonly IGetTemplateEditorModels _getTemplateEditorModels;

        public TemplatesController(IGetTemplateEditorModels getTemplateEditorModels)
        {
            _getTemplateEditorModels = getTemplateEditorModels;
        }

        /// <summary>
        /// Returns the form template definitions.
        /// </summary>
        /// <returns>
        /// The array of form template definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var options = _getTemplateEditorModels.GetAll();

            return Ok(options);
        }
    }
}
