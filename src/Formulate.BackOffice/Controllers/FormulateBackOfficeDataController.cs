using System;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace Formulate.BackOffice.Controllers
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public abstract partial class FormulateBackOfficeEntityApiController : UmbracoAuthorizedApiController
    {
        protected readonly ITreeEntityRepository TreeEntityRepository;
        
        protected FormulateBackOfficeEntityApiController(ITreeEntityRepository treeEntityRepository)
        {
            TreeEntityRepository = treeEntityRepository;
        }

        [HttpGet]
        public virtual IActionResult Get(Guid id)
        {
            var entity = TreeEntityRepository.Get(id);

            if (entity is null)
            {
                return this.NotFound();
            }

            var response = new GetEntityResponse()
            {
                Entity = entity,
                EntityType = entity.EntityType(),
                TreePath = entity.TreeSafePath()
            };

            return this.Ok(response);
        }
    }
}
