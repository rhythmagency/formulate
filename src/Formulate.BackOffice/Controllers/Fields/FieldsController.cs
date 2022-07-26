namespace Formulate.BackOffice.Controllers.Fields
{
    using Formulate.BackOffice.Attributes;
    using Formulate.Core.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Umbraco.Cms.Web.BackOffice.Controllers;
    using Umbraco.Cms.Web.BackOffice.Filters;

    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FieldsController : UmbracoAuthorizedApiController
    {
        private readonly ButtonsOptions _buttonConfig;

        public FieldsController(IOptions<ButtonsOptions> buttonsConfig)
        {
            _buttonConfig = buttonsConfig.Value;
        }

        [HttpGet]   
        public IActionResult GetButtonKinds()
        {
            return Ok(_buttonConfig.Items);
        }
    }
}
