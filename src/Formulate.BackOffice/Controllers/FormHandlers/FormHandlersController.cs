namespace Formulate.BackOffice.Controllers.FormHandlers
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.CreateOptions.FormHandlers;
    using Formulate.BackOffice.Utilities.FormHandlers;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Umbraco.Cms.Web.BackOffice.Controllers;

    [FormulateBackOfficePluginController]
    public sealed class FormHandlersController : UmbracoAuthorizedApiController
    {
        private readonly IGetFormHandlerOptions _getFormHandlerOptions;

        private readonly IGetFormHandlerScaffolding _getFormHandlerScaffolding;

        private readonly IEditorModelMapper _editorModelMapper;

        public FormHandlersController(IGetFormHandlerOptions getFormHandlerOptions, IGetFormHandlerScaffolding getFormHandlerScaffolding, IEditorModelMapper editorModelMapper)
        {
            _getFormHandlerOptions = getFormHandlerOptions;
            _getFormHandlerScaffolding = getFormHandlerScaffolding;
            _editorModelMapper = editorModelMapper;
        }

        /// <summary>
        /// Returns the form handler definitions.
        /// </summary>
        /// <returns>
        /// The array of form handler definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetDefinitions()
        {
            var options = _getFormHandlerOptions.Get();

            return Ok(options);
        }

        [NonAction]
        public IActionResult GetScaffolding()
        {
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult GetScaffolding(Guid id)
        {
            var item = _getFormHandlerScaffolding.Get(id);

            if (item is null)
            {
                return BadRequest();
            }

            var input = new MapToEditorModelInput(item, true);
            var editorModel = _editorModelMapper.MapToEditor(input);

            return Ok(editorModel);
        }
    }
}
