namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Attributes;
    using Core.Configuration;
    using Core.ConfiguredForms;
    using Core.Folders;
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Core.Layouts;
    using Core.Persistence;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
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
        private readonly IConfiguredFormEntityRepository configuredFormRepository;
        private readonly IFormHandlerFactory formHandlerFactory;
        private readonly IFormFieldFactory formFieldFactory;
        private readonly FormHandlerDefinitionCollection formHandlerDefinitions;
        private readonly FormFieldDefinitionCollection formFieldDefinitions;
        private readonly IOptions<TemplatesOptions> templatesConfig;
        private readonly ILayoutEntityRepository layoutEntities;

        public FormsController(ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            IFormEntityRepository formEntityRepository,
            IFormHandlerFactory formHandlerFactory,
            IFormFieldFactory formFieldFactory,
            FormHandlerDefinitionCollection formHandlerDefinitions,
            FormFieldDefinitionCollection formFieldDefinitions,
            IOptions<TemplatesOptions> templatesConfig,
            ILayoutEntityRepository layoutEntities,
            IConfiguredFormEntityRepository configuredFormRepository)
            : base(treeEntityRepository, localizedTextService)
        {
            this.formEntityRepository = formEntityRepository;
            this.formHandlerFactory = formHandlerFactory;
            this.formFieldFactory = formFieldFactory;
            this.formHandlerDefinitions = formHandlerDefinitions;
            this.formFieldDefinitions = formFieldDefinitions;
            this.templatesConfig = templatesConfig;
            this.layoutEntities = layoutEntities;
            this.configuredFormRepository = configuredFormRepository;
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

            // If this is a folder, return immediately.
            if (baseResult.Entity is PersistedFolder)
            {
                return Ok(baseResult);
            }

            // If this is a configured form, return details for the configured form.
            if (baseResult.Entity is PersistedConfiguredForm)
            {
                return GetConfiguredForm(baseResult);
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

        /// <summary>
        /// Saves the form.
        /// </summary>
        /// <param name="request">
        /// The request to save the form.
        /// </param>
        /// <returns>
        /// An indicator of success.
        /// </returns>
        [HttpPost]
        public ActionResult Save(PersistedForm entity)
        {
            formEntityRepository.Save(entity);
            return Ok(new
            {
                Success = true,
            });
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

        /// <summary>
        /// Returns the form field definitions.
        /// </summary>
        /// <returns>
        /// The array of form field definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetFieldDefinitions()
        {
            var definitions = formFieldDefinitions.ToArray();
            return Ok(definitions);
        }

        /// <summary>
        /// Returns the form template definitions.
        /// </summary>
        /// <returns>
        /// The array of form template definitions.
        /// </returns>
        [HttpGet]
        public IActionResult GetTemplateDefinitions()
        {
            return Ok(templatesConfig.Value.Items.Select(x => new
            {
                x.Id,
                x.Name,
            }));
        }

        /// <inheritdoc cref="GenerateNewPathAndId(GenerateNewPathAndIdRequest)" />
        /// <remarks>
        /// This is never called, but it is used to generate a URL that
        /// is passed to the frontend.
        /// </remarks>
        [NonAction]
        public IActionResult GenerateNewPathAndId() => new EmptyResult();

        /// <summary>
        /// Generates a new path and ID for a new entity.
        /// </summary>
        /// <param name="request">
        /// The request to generate a new path.
        /// </param>
        /// <returns>
        /// The path and ID.
        /// </returns>
        /// <remarks>
        /// This method is necessary because the frontend doesn't know the
        /// path for the parent of a new entity.
        /// </remarks>
        public IActionResult GenerateNewPathAndId(GenerateNewPathAndIdRequest request)
        {
            // Variables.
            var newPath = default(Guid[]);
            var newId = Guid.NewGuid();

            // Does this new entity have a parent?
            if (request.ParentId.HasValue)
            {
                // Append new ID to path from parent entity.
                var parentEntity = base.GetEntity(request.ParentId.Value);
                newPath = parentEntity.Entity.Path
                    .Concat(new[] { newId })
                    .ToArray();
            }
            else
            {
                // Create new path.
                newPath = new Guid[]
                {
                    Guid.Parse(FormConstants.RootId),
                    newId,
                };
            }

            // Return the path and ID.
            return Ok(new
            {
                Path = newPath,
                Id = newId,
            });
        }

        /// <summary>
        /// Returns the data that should be sent to the browser for a configured form.
        /// </summary>
        /// <param name="baseResult">
        /// The generic result for the entity that needs to be converted into the
        /// more specialized result for the configured form.
        /// </param>
        /// <returns>
        /// The response containing the configured form data.
        /// </returns>
        private IActionResult GetConfiguredForm(GetEntityResponse baseResult)
        {
            var configuredForm = baseResult.Entity as PersistedConfiguredForm;
            var layout = configuredForm.LayoutId.HasValue
                ? layoutEntities.Get(configuredForm.LayoutId.Value)
                : null;
            var template = configuredForm.TemplateId.HasValue
                ? templatesConfig.Value.Items
                    .FirstOrDefault(x => x.Id == configuredForm.TemplateId.Value)
                : null;
            return Ok(new
            {
                Entity = new
                {
                    configuredForm.Name,
                    configuredForm.Alias,
                    configuredForm.Id,
                    configuredForm.Path,
                    configuredForm.LayoutId,
                    LayoutName = layout?.Name,
                    configuredForm.TemplateId,
                    TemplateName = template?.Name,
                },
                EntityType = baseResult.EntityType,
                TreePath = baseResult.TreePath,
            });
        }

        /// <inheritdoc cref="Save(PersistedForm)"/>
        /// <remarks>
        /// This exists purely so the "SaveConfiguredForm" method can be referenced
        /// without parameters so reflection can be used to generate a URL for it.
        /// </remarks>
        [NonAction]
        public IActionResult SaveConfiguredForm()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Saves the configured form to the file system.
        /// </summary>
        /// <param name="entity">
        /// The configured form to save.
        /// </param>
        /// <returns>
        /// An indicator of success.
        /// </returns>
        [HttpPost]
        public IActionResult SaveConfiguredForm(PersistedConfiguredForm entity)
        {
            configuredFormRepository.Save(entity);
            return Ok(new
            {
                Success = true,
            });
        }

        /// <inheritdoc cref="GetConfiguredForm(Guid)"/>
        /// <remarks>
        /// This exists purely so the "GetConfiguredForm" method can be referenced
        /// without parameters so reflection can be used to generate a URL for it.
        /// </remarks>
        [NonAction]
        public IActionResult GetConfiguredForm()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Gets details about the configured form with the specified ID.
        /// </summary>
        /// <param name="od">
        /// The ID of the configured form.
        /// </param>
        /// <returns>
        /// The configured form details.
        /// </returns>
        [HttpGet]
        public IActionResult GetConfiguredForm(Guid id)
        {
            var entity = new GetEntityResponse()
            {
                Entity = configuredFormRepository.Get(id),
            };
            return GetConfiguredForm(entity);
        }
    }
}