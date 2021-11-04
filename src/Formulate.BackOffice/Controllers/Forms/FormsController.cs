using System;
using System.Collections.Generic;
using System.Linq;
using Formulate.BackOffice.Attributes;
using Formulate.BackOffice.Persistence;
using Formulate.BackOffice.Trees;
using Formulate.Core.ConfiguredForms;
using Formulate.Core.Folders;
using Formulate.Core.FormFields;
using Formulate.Core.FormHandlers;
using Formulate.Core.Forms;
using Formulate.Core.Persistence;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Filters;

namespace Formulate.BackOffice.Controllers.Forms
{
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FormsController : FormulateBackOfficeEntityApiController 
    {
        private readonly IFormEntityRepository _formEntityRepository;

        public FormsController(ITreeEntityRepository treeEntityRepository, IFormEntityRepository formEntityRepository) : base(treeEntityRepository)
        {
            _formEntityRepository = formEntityRepository;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? parentId)
        {
            var options = GetCreateOptions(parentId);

            var isValidOption = options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                return this.BadRequest();
            }

            var parent = parentId.HasValue ? TreeEntityRepository.Get(parentId.Value) : default;
            IPersistedEntity entity = null;

            if (entityType == EntityTypes.Form)
            {
                entity = new PersistedForm()
                {
                    Fields = Array.Empty<PersistedFormField>(),
                    Handlers = Array.Empty<PersistedFormHandler>()
                };
            }
            else if (entityType == EntityTypes.Folder)
            {
                entity = new PersistedFolder();
            }
            else if (entityType == EntityTypes.ConfiguredForm)
            {
                entity = new PersistedConfiguredForm();
            }

            if (entity is null)
            {
                return this.BadRequest();
            }

            var response = new GetEntityResponse()
            {
                Entity = entity,
                EntityType = entityType,
                TreePath = parent.TreeSafePath(),
            };

            return this.Ok(response);
        }

        [HttpGet]
        public IEnumerable<CreateChildEntityOption> GetCreateOptions(Guid? id)
        {
            var options = new List<CreateChildEntityOption>();

            if (id is null)
            {
                options.AddFormFolderOption();
                options.AddFormOption();

                return options;
            }

            var entity = TreeEntityRepository.Get(id.Value);

            if (entity is PersistedForm)
            {
                options.AddConfiguredFormOption();

                return options;
            }

            if (entity is not PersistedFolder)
            {
                return options;
            }

            options.AddFormFolderOption();
            options.AddFormOption();

            return options;
        }
    }
}
