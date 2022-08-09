namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using Attributes;
    using Core.ConfiguredForms;
    using Core.FormFields;
    using Core.FormHandlers;
    using Core.Forms;
    using Formulate.Core.Templates;
    using Microsoft.AspNetCore.Mvc;
    using Persistence;
    using System;
    using System.Linq;
    using Umbraco.Cms.Core.Models.ContentEditing;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Web.BackOffice.Filters;
    using Umbraco.Extensions;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.Forms;

    /// <summary>
    /// Manages back office API operations for Formulate forms.
    /// </summary>
    [JsonCamelCaseFormatter]
    [FormulateBackOfficePluginController]
    public sealed class FormsController : FormulateBackOfficeEntityApiController
    {
        private readonly IGetFormsChildEntityOptions _getFormsChildEntityOptions;
        private readonly ICreateFormsScaffoldingEntity _createFormsScaffoldingEntity;
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
            IBuildEditorModel buildEditorModel,
            IGetFormsChildEntityOptions getFormsChildEntityOptions,
            ICreateFormsScaffoldingEntity createFormsScaffoldingEntity)
            : base(buildEditorModel, treeEntityRepository, localizedTextService)
        {
            this.formEntityRepository = formEntityRepository;
            this.formHandlerDefinitions = formHandlerDefinitions;
            this.formFieldDefinitions = formFieldDefinitions;
            this.templateDefinitions = templateDefinitions;
            this.configuredFormRepository = configuredFormRepository;
            _getFormsChildEntityOptions = getFormsChildEntityOptions;
            _createFormsScaffoldingEntity = createFormsScaffoldingEntity;
        }

        [HttpGet]
        public IActionResult GetScaffolding([FromQuery] FormsGetScaffoldingRequest request)
        {
            var parent = TreeEntityRepository.Get(request.ParentId);
            var options = _getFormsChildEntityOptions.Get(parent);
            var isValidOption = options.Any(x => x.EntityType == request.EntityType);

            if (isValidOption == false)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Invalid requested item type.", "");

                return ValidationProblem(errorModel);
            }

            var input = new CreateFormsScaffoldingEntityInput()
            {
                Parent = parent,
                EntityType = request.EntityType,
                RootId = TreeEntityRepository.GetRootId(TreeRootTypes.Forms),
            };

            var entity = _createFormsScaffoldingEntity.Create(input);

            if (entity is null)
            {
                var errorModel = new SimpleNotificationModel();
                errorModel.AddErrorNotification("Unable to get a valid item type.", "");

                return ValidationProblem(errorModel);
            }

            var buildInput = new BuildEditorModelInput(entity, true);
            var editorModel = _buildEditorModel.Build(buildInput);

            return Ok(editorModel);
        }

        [HttpGet]
        public IActionResult GetCreateOptions(Guid? id)
        {
            var parent = TreeEntityRepository.Get(id);
            var options = _getFormsChildEntityOptions.Get(parent);

            return Ok(options);
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
    }
}
