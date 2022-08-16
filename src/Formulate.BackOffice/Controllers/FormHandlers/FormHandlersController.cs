namespace Formulate.BackOffice.Controllers.FormHandlers
{
    using Formulate.BackOffice.Attributes;
    using Formulate.Core.FormHandlers;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Umbraco.Cms.Web.BackOffice.Controllers;

    [FormulateBackOfficePluginController]
    public sealed class FormHandlersController : UmbracoAuthorizedApiController
    {
        /// <summary>
        /// The form handler definitions.
        /// </summary>
        private readonly FormHandlerDefinitionCollection _formHandlerDefinitions;

        public FormHandlersController(FormHandlerDefinitionCollection formHandlerDefinitions)
        {
            _formHandlerDefinitions = formHandlerDefinitions;
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
            var groupedDefinitions = _formHandlerDefinitions.GroupBy(x => x.Category);
            var viewModels = groupedDefinitions.Select(group => new
            {
                key = group.Key,
                items = group.OrderBy(x => x.DefinitionLabel).ToArray()
            }).ToArray();

            return Ok(viewModels);
        }
    }
}
