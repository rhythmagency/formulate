namespace Formulate.BackOffice.Controllers.Fields
{
    using Formulate.BackOffice.Attributes;
    using Formulate.Core.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Umbraco.Cms.Web.BackOffice.Controllers;
    

    [FormulateBackOfficePluginController]
    public sealed class FieldsController : UmbracoAuthorizedApiController
    {
        /// <summary>
        /// The buttons config.
        /// </summary>
        private readonly ButtonsOptions _buttonsConfig;

        public FieldsController(IOptions<ButtonsOptions> buttonsConfig)
        {
            _buttonsConfig = buttonsConfig.Value;
        }

        [HttpGet]   
        public IActionResult GetButtonKinds()
        {
            return Ok(_buttonsConfig);
        }
    }
}
