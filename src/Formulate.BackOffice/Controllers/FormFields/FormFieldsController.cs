namespace Formulate.BackOffice.Controllers.FormFields
{
    using Formulate.BackOffice.Attributes;
    using Formulate.Core.Configuration;
    using Formulate.Core.FormFields;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
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
        private readonly FormFieldDefinitionCollection _formFieldDefinitions;

        public FormFieldsController(IOptions<ButtonsOptions> buttonsConfig, FormFieldDefinitionCollection formFieldDefinitions)
        {
            _buttonsConfig = buttonsConfig.Value;
            _formFieldDefinitions = formFieldDefinitions;
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
            var groupedDefinitions = _formFieldDefinitions.GroupBy(x => x.Category);
            var viewModels = groupedDefinitions.Select(group => new
            {
                key = group.Key,
                items = group.OrderBy(x => x.DefinitionLabel).ToArray()
            }).ToArray();

            return Ok(viewModels);
        }
    }
}
