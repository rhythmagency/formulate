namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Attributes;
    using Core.ConfiguredForms;
    using Core.Folders;
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Core.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Trees;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Web.BackOffice.Filters;
    using Umbraco.Extensions;
    using ViewModels.Forms;

    /// <summary>
    /// Manages back office API operations for Formulate forms.
    /// </summary>
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FormsController : FormulateBackOfficeEntityApiController 
    {
        private readonly IFormEntityRepository formEntityRepository;
        private readonly IFormHandlerFactory formHandlerFactory;
        private readonly IFormFieldFactory formFieldFactory;
        private readonly FormHandlerDefinitionCollection formHandlerDefinitions;

        public FormsController(ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            IFormEntityRepository formEntityRepository,
            IFormHandlerFactory formHandlerFactory,
            IFormFieldFactory formFieldFactory,
            FormHandlerDefinitionCollection formHandlerDefinitions)
            : base(treeEntityRepository, localizedTextService)
        {
            this.formEntityRepository = formEntityRepository;
            this.formHandlerFactory = formHandlerFactory;
            this.formFieldFactory = formFieldFactory;
            this.formHandlerDefinitions = formHandlerDefinitions;
        }

        [HttpGet]
        public IActionResult GetScaffolding(EntityTypes entityType, Guid? parentId)
        {
            var options = GetCreateOptions(parentId);

            var isValidOption = options.Any(x => x.EntityType == entityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
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
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var response = new GetEntityResponse()
            {
                Entity = entity,
                EntityType = entityType,
                TreePath = parent.TreeSafePath(),
            };

            return Ok(response);
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

        /// <inheritdoc cref="FormulateBackOfficeEntityApiController.Get(Guid)"/>
        public override IActionResult Get(Guid id)
        {
            // Get the base data.
            var baseResult = base.GetEntity(id);

            // Data not found?
            if (baseResult == null)
            {
                return NotFound();
            }

            // Supplement the base response with additional data.
            var form = baseResult.Entity as PersistedForm;
            var entity = new FormViewModel(form, formHandlerFactory, formFieldFactory);
            var formResponse = new GetFormResponse()
            {
                Entity = entity,
                EntityType = baseResult.EntityType,
                TreePath = baseResult.TreePath,
            };

            // Return the response with the data.
            return Ok(formResponse);
        }

        [HttpPost]
        public ActionResult Save(SavePersistedFormRequest request)
        {
            //TDOO: Implement. Refer to ValidationsController.Save for a good example.
            return null;
        }

        /// <summary>
        /// Returns the form handler definitions.
        /// </summary>
        /// <returns>
        /// The array of form handler definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetHandlerDefinitions()
        {
            var definitions = formHandlerDefinitions.ToArray();
            return Ok(definitions);
        }
    }
}