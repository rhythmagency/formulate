namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Attributes;
    using Core.ConfiguredForms;
    using Core.Folders;
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Core.Layouts;
    using Core.Persistence;
    using Formulate.Core.Templates;
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
    using EditorModels.Forms;
    using Formulate.BackOffice.Utilities;

    /// <summary>
    /// Manages back office API operations for Formulate forms.
    /// </summary>
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FormsController : FormulateBackOfficeEntityApiController
    {
        private readonly IFormEntityRepository formEntityRepository;
        private readonly IConfiguredFormEntityRepository configuredFormRepository;
        private readonly FormHandlerDefinitionCollection formHandlerDefinitions;
        private readonly FormFieldDefinitionCollection formFieldDefinitions;
        private readonly TemplateDefinitionCollection templateDefinitions;

        public FormsController(ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            IFormEntityRepository formEntityRepository,
            FormHandlerDefinitionCollection formHandlerDefinitions,
            FormFieldDefinitionCollection formFieldDefinitions,
            TemplateDefinitionCollection templateDefinitions,
            IConfiguredFormEntityRepository configuredFormRepository,
            IBuildEditorModel buildEditorModel)
            : base(buildEditorModel, treeEntityRepository, localizedTextService)
        {
            this.formEntityRepository = formEntityRepository;
            this.formHandlerDefinitions = formHandlerDefinitions;
            this.formFieldDefinitions = formFieldDefinitions;
            this.templateDefinitions = templateDefinitions;
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
            var entityPath = parent is null ? Array.Empty<Guid>() : parent.Path;
            IPersistedEntity entity = null;

            if (entityType == EntityTypes.Form)
            {
                entity = new PersistedForm()
                {
                    Fields = Array.Empty<PersistedFormField>(),
                    Handlers = Array.Empty<PersistedFormHandler>(),
                    Path = entityPath
                };
            }
            else if (entityType == EntityTypes.Folder)
            {
                entity = new PersistedFolder()
                {
                    Path = entityPath,
                };
            }
            else if (entityType == EntityTypes.ConfiguredForm)
            {
                entity = new PersistedConfiguredForm()
                {
                    Path = entityPath,
                };
            }

            if (entity is null)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var editorModel = _buildEditorModel.Build(entity);

            return Ok(editorModel);
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
            var groupedDefinitions = formHandlerDefinitions.GroupBy(x => x.Category);
            var viewModels = groupedDefinitions.Select(group => new
            {
                key = group.Key,
                items = group.OrderBy(x => x.DefinitionLabel).ToArray()
            }).ToArray();

            return Ok(viewModels);
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
            var groupedDefinitions = formFieldDefinitions.GroupBy(x => x.Category);
            var viewModels = groupedDefinitions.Select(group => new
             {
                key = group.Key,
                items = group.OrderBy(x => x.DefinitionLabel).ToArray()
            }).ToArray();

            return Ok(viewModels);
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
            var options = templateDefinitions.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToArray();

            return Ok(options);
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
            return Get(id);
        }

        /// <inheritdoc cref="GetFormInfo(Guid)"/>
        /// <remarks>
        /// This exists purely so the "GetFormInfo" method can be referenced
        /// without parameters so reflection can be used to generate a URL for it.
        /// </remarks>
        [NonAction]
        public IActionResult GetFormInfo()
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
        public IActionResult GetFormInfo(Guid id)
        {
            var form = formEntityRepository.Get(id);
            return Ok(new
            {
                Success = true,
                form.Id,
                form.Path,
                form.Alias,
                form.Name,
                Fields = form.Fields.Select(x => new
                {
                    x.Id,
                    x.Alias,
                    x.Name,
                    x.Label,
                    x.Category,
                    IsServerSideOnly = false,//TODO: ...
                }),
            });
        }
    }
}