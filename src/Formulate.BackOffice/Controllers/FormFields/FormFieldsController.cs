namespace Formulate.BackOffice.Controllers.FormFields
{
    using Formulate.BackOffice.Attributes;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.CreateOptions.FormFields;
    using Formulate.BackOffice.Utilities.EditorModels.Forms;
    using Formulate.BackOffice.Utilities.FormFields;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Umbraco.Cms.Web.BackOffice.Controllers;


    [FormulateBackOfficePluginController]
    public sealed class FormFieldsController : UmbracoAuthorizedApiController
    {
        /// <summary>
        /// The form field definitions.
        /// </summary>
        private readonly IGetFormFieldOptions _getFormFieldOptions;
        
        private readonly IGetFormFieldScaffolding _getFormFieldScaffolding;

        private readonly IGetFormFieldCategoryEditorModels _getFormFieldCategoryEditorModels;

        private readonly IEditorModelMapper _editorModelMapper;

        public FormFieldsController(IGetFormFieldScaffolding getFormFieldScaffolding, IGetFormFieldOptions getFormFieldOptions, IGetFormFieldCategoryEditorModels getFormFieldCategoryEditorModels,  IEditorModelMapper editorModelMapper)
        {
            _getFormFieldOptions = getFormFieldOptions;
            _getFormFieldScaffolding = getFormFieldScaffolding;
            _getFormFieldCategoryEditorModels = getFormFieldCategoryEditorModels;
            _editorModelMapper = editorModelMapper;
        }

        /// <summary>
        /// Returns the form field categories.
        /// </summary>
        /// <returns>
        /// The array of form field categories.
        /// </returns>
        [HttpGet]
        public IActionResult GetCategories()
        {
            var options = _getFormFieldCategoryEditorModels.GetAll();

            return Ok(options);
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
