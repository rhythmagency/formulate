namespace Formulate.BackOffice.Controllers.FormFields
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.CreateOptions.FormFields;
    using Formulate.BackOffice.Utilities.FormFields;
    using Formulate.Core.Configuration;
    using Formulate.Core.FormFields;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using Umbraco.Cms.Web.BackOffice.Controllers;


    [FormulateBackOfficePluginController]
    public sealed class FormFieldsController : UmbracoAuthorizedApiController
    {
        /// <summary>
        /// The buttons config.
        /// </summary>
        private readonly ButtonsOptions _buttonsConfig;

        /// <summary>
        /// The form field definitions.
        /// </summary>
        private readonly IGetFormFieldOptions _getFormFieldOptions;
        
        private readonly IGetFormFieldScaffolding _getFormFieldScaffolding;
        
        private readonly IEditorModelMapper _editorModelMapper;

        public FormFieldsController(IOptions<ButtonsOptions> buttonsConfig, IGetFormFieldScaffolding getFormFieldScaffolding, IGetFormFieldOptions getFormFieldOptions, IEditorModelMapper editorModelMapper)
        {
            _buttonsConfig = buttonsConfig.Value;
            _getFormFieldOptions = getFormFieldOptions;
            _getFormFieldScaffolding = getFormFieldScaffolding;
            _editorModelMapper = editorModelMapper;
        }

        [HttpGet]   
        public IActionResult GetButtonKinds()
        {
            return Ok(_buttonsConfig);
        }

        /// <summary>
        /// Returns the form field definitions.
        /// </summary>
        /// <returns>
        /// The array of form field definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetDefinitions()
        {
            var options = _getFormFieldOptions.Get();

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
            var item = _getFormFieldScaffolding.Get(id);

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
