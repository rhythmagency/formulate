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
    
    using Umbraco.Extensions;
    using Formulate.BackOffice.Utilities;
    using Formulate.BackOffice.Utilities.Forms;
    using Formulate.BackOffice.EditorModels.Forms;

    /// <summary>
    /// Manages back office API operations for Formulate forms.
    /// </summary>
    [FormulateBackOfficePluginController]
    public sealed class FormsController : FormulateBackOfficeEntityApiController
    {
        private readonly IGetFormsChildEntityOptions _getFormsChildEntityOptions;
        private readonly ICreateFormsScaffoldingEntity _createFormsScaffoldingEntity;
        private readonly IFormEntityRepository formEntityRepository;
        private readonly FormHandlerDefinitionCollection formHandlerDefinitions;
        private readonly FormFieldDefinitionCollection formFieldDefinitions;
        private readonly TemplateDefinitionCollection templateDefinitions;

        public FormsController(ITreeEntityRepository treeEntityRepository,
            ILocalizedTextService localizedTextService,
            IFormEntityRepository formEntityRepository,
            FormHandlerDefinitionCollection formHandlerDefinitions,
            FormFieldDefinitionCollection formFieldDefinitions,
            TemplateDefinitionCollection templateDefinitions,
            IEditorModelMapper editorModelMapper,
            IGetFormsChildEntityOptions getFormsChildEntityOptions,
            ICreateFormsScaffoldingEntity createFormsScaffoldingEntity)
            : base(editorModelMapper, treeEntityRepository, localizedTextService)
        {
            this.formEntityRepository = formEntityRepository;
            this.formHandlerDefinitions = formHandlerDefinitions;
            this.formFieldDefinitions = formFieldDefinitions;
            this.templateDefinitions = templateDefinitions;
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

            var mapInput = new MapToEditorModelInput(entity, true);
            var editorModel = _editorModelMapper.MapToEditor(mapInput);

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
        public ActionResult Save(FormEditorModel model)
        {
            var entity = _editorModelMapper.MapToEntity<FormEditorModel, PersistedForm>(model);
            formEntityRepository.Save(entity);
            
            return Ok();
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
                id = x.Id,
                name = x.Name,
            }).ToArray();

            return Ok(options);
        }
    }
}
