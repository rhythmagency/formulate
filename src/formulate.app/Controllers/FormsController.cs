namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Extensions;
    using Forms;
    using formulate.app.CollectionBuilders;
    using formulate.app.Layouts.Kinds.Basic;
    using Helpers;
    using Layouts;
    using Managers;
    using Models.Requests;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;
    using FormConstants = Constants.Trees.Forms;
    using LayoutConstants = Constants.Trees.Layouts;


    /// <summary>
    /// Controller for Formulate forms.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class FormsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string GetFormInfoError = @"An error occurred while attempting to get the form info for a Formulate form.";
        private const string PersistFormError = @"An error occurred while attempting to persist the Formulate form.";
        private const string DeleteFormError = @"An error occurred while attempting to delete the Formulate form.";
        private const string MoveFormError = @"An error occurred while attempting to move a Formulate form.";

        #endregion


        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }
        private IFormPersistence Persistence { get; set; }
        private ILayoutPersistence LayoutPersistence { get; set; }
        private IEntityPersistence Entities { get; set; }
        private IValidationPersistence Validations { get; set; }
        private IConfiguredFormPersistence ConFormPersistence { get; set; }
        private IEntityHelper EntityHelper { get; set; }

        private FormHandlerTypeCollection FormHandlerTypeCollection { get; set; }
        private FormFieldTypeCollection FormFieldTypeCollection { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormsController(IConfigurationManager configurationManager, IFormPersistence formPersistence,
            ILayoutPersistence layoutPersistence, IEntityPersistence entityPersistence,
            IValidationPersistence validationPersistence, IConfiguredFormPersistence configuredFormPersistence,
            IEntityHelper entityHelper, FormFieldTypeCollection formFieldTypeCollection,
            FormHandlerTypeCollection formHandlerTypeCollection)
        {
            Config = configurationManager;
            Persistence = formPersistence;
            LayoutPersistence = layoutPersistence;
            Entities = entityPersistence;
            Validations = validationPersistence;
            ConFormPersistence = configuredFormPersistence;
            EntityHelper = entityHelper;
            FormHandlerTypeCollection = formHandlerTypeCollection;
            FormFieldTypeCollection = formFieldTypeCollection;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns the form info for the specified form.
        /// </summary>
        /// <param name="request">
        /// The request to get the form info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetFormInfo([FromUri] GetFormInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.FormId);
                var form = Persistence.Retrieve(id);
                var fullPath = new[] { rootId }
                    .Concat(form.Path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Set result.
                result = new
                {
                    Success = true,
                    FormId = GuidHelper.GetString(form.Id),
                    Path = fullPath,
                    Alias = form.Alias,
                    Name = form.Name,
                    Fields = form.Fields.MakeSafe().Select(x => new
                    {
                        Id = GuidHelper.GetString(x.Id),
                        x.Alias,
                        x.Name,
                        x.Label,
                        x.Category,
                        x.IsServerSideOnly,
                        Validations = x.Validations.MakeSafe()
                            .Select(y => Validations.Retrieve(y))
                            .WithoutNulls()
                            .Select(y => new
                            {
                                Id = GuidHelper.GetString(y.Id),
                                Name = y.Name
                            }).ToArray(),
                        Configuration = JsonHelper.Deserialize<object>(
                            x.FieldConfiguration),
                        Directive = x.GetDirective(),
                        Icon = x.GetIcon(),
                        TypeLabel = x.GetTypeLabel(),
                        TypeFullName = x.GetFieldType().AssemblyQualifiedName
                    }).ToArray(),
                    Handlers = form.Handlers.MakeSafe().Select(x => new
                    {
                        Id = GuidHelper.GetString(x.Id),
                        x.Alias,
                        x.Name,
                        x.Enabled,
                        Configuration = JsonHelper.Deserialize<object>(
                            x.HandlerConfiguration),
                        Directive = x.GetDirective(),
                        Icon = x.GetIcon(),
                        TypeLabel = x.GetTypeLabel(),
                        TypeFullName = x.GetHandlerType().AssemblyQualifiedName
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FormsController>(ex, GetFormInfoError);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return result.
            return result;

        }


        /// <summary>
        /// Persists a form.
        /// </summary>
        /// <param name="request">
        /// The request to persist a form.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with the
        /// form ID.
        /// </returns>
        [HttpPost]
        public object PersistForm(PersistFormRequest request)
        {

            // Variables.
            var result = default(object);
            var formsRootId = GuidHelper.GetGuid(FormConstants.Id);
            var parentId = GuidHelper.GetGuid(request.ParentId);


            // Catch all errors.
            try
            {

                // Parse or create the form ID.
                var isNew = string.IsNullOrWhiteSpace(request.FormId);
                var formId = isNew
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.FormId);


                // Get the fields.
                var fields = request.Fields.MakeSafe()
                    .Select(x =>
                    {
                        var fieldType = Type.GetType(x.TypeFullName);
                        var fieldTypeInstance = FormFieldTypeCollection
                            .FirstOrDefault(y => y.GetType() == fieldType);

                        var field = new FormField(fieldTypeInstance)
                        {
                            Id = string.IsNullOrWhiteSpace(x.Id)
                                ? Guid.NewGuid()
                                : GuidHelper.GetGuid(x.Id),
                            Alias = x.Alias,
                            Name = x.Name,
                            Label = x.Label,
                            Category = x.Category,
                            Validations = x.Validations.MakeSafe()
                                    .Select(y => GuidHelper.GetGuid(y)).ToArray(),
                            FieldConfiguration = JsonHelper.Serialize(x.Configuration)
                        };
                        return field;
                    })
                    .ToArray();


                // Get the handlers.
                var handlers = request.Handlers.MakeSafe().Select(x =>
                {
                    var handlerType = Type.GetType(x.TypeFullName);
                    var handlerTypeInstance = FormHandlerTypeCollection
                        .FirstOrDefault(y => y.GetType() == handlerType);

                    var handler = new FormHandler(handlerTypeInstance)
                    {
                        Id = string.IsNullOrWhiteSpace(x.Id)
                            ? Guid.NewGuid()
                            : GuidHelper.GetGuid(x.Id),
                        Alias = x.Alias,
                        Name = x.Name,
                        Enabled = x.Enabled,
                        HandlerConfiguration = JsonHelper.Serialize(x.Configuration)
                    };

                    return handler;
                }).ToArray();


                // Get the ID path.
                var parent = parentId == Guid.Empty ? null : Entities.Retrieve(parentId);
                var path = parent == null
                    ? new[] { formsRootId, formId }
                    : parent.Path.Concat(new[] { formId }).ToArray();


                // Create the form.
                var form = new Form()
                {
                    Id = formId,
                    Path = path,
                    Alias = request.Alias,
                    Name = request.Name,
                    Fields = fields,
                    Handlers = handlers
                };


                // Persist the form.
                Persistence.Persist(form);


                // For new forms, automatically create a layout and a form configuration.
                var layoutNamePrefix = "Layout for ";
                var layoutNameSuffix = " (Autogenerated)";
                var layoutAliasPrefix = "layout_";
                var layoutAliasSuffix = "_autogenerated";
                var autoLayoutData = JsonHelper.Serialize(new
                {
                    rows = new[]
                    {
                        new
                        {
                            cells = new []
                            {
                                new
                                {
                                    columnSpan = 12,
                                    fields = form.Fields.Select(x => new
                                    {
                                        id = GuidHelper.GetString(x.Id)
                                    })
                                }
                            }
                        }
                    },
                    formId = GuidHelper.GetString(form.Id),
                    autopopulate = true
                });
                if (isNew)
                {

                    // Create new layout.
                    var layoutId = Guid.NewGuid();
                    var layout = new Layout()
                    {
                        KindId = GuidHelper.GetGuid(app.Constants.Layouts.LayoutBasic.Id),
                        Id = layoutId,
                        Path = new[] { GuidHelper.GetGuid(LayoutConstants.Id), layoutId },
                        Name = layoutNamePrefix + request.Name + layoutNameSuffix,
                        Alias = layoutAliasPrefix + request.Alias + layoutAliasSuffix,
                        Data = autoLayoutData
                    };


                    // Persist layout.
                    LayoutPersistence.Persist(layout);


                    // Create a new form configuration.
                    var plainJsTemplateId = GuidHelper.GetGuid("f3fb1485c1d14806b4190d7abde39530");
                    var template = Config.Templates.FirstOrDefault(x => x.Id == plainJsTemplateId)
                        ?? Config.Templates.FirstOrDefault();
                    var configId = Guid.NewGuid();
                    var configuredForm = new ConfiguredForm()
                    {
                        Id = configId,
                        Path = path.Concat(new[] { configId }).ToArray(),
                        Name = request.Name,
                        TemplateId = template?.Id,
                        LayoutId = layoutId
                    };


                    // Persist form configuration.
                    ConFormPersistence.Persist(configuredForm);

                }


                // Get existing layouts that should autopopulate.
                var layouts = GetFormLayouts(null)
                    .Select(x => new
                    {
                        Layout = x,
                        Configuration = x.DeserializeConfiguration() as LayoutBasicConfiguration
                    })
                    .Where(x => x.Configuration != null)
                    .Where(x => x.Configuration.FormId.HasValue && x.Configuration.FormId.Value == formId)
                    .Where(x => x.Configuration.Autopopulate);


                //: Autopopulate the layouts.
                foreach (var existingLayout in layouts)
                {
                    existingLayout.Layout.Data = autoLayoutData;
                    var layoutName = existingLayout.Layout.Name ?? string.Empty;
                    var layoutAlias = existingLayout.Layout.Alias ?? string.Empty;
                    if (layoutName.StartsWith(layoutNamePrefix) && layoutName.EndsWith(layoutNameSuffix))
                    {
                        existingLayout.Layout.Name = layoutNamePrefix + form.Name + layoutNameSuffix;
                    }
                    if (layoutAlias.StartsWith(layoutAliasPrefix) && layoutAlias.EndsWith(layoutAliasSuffix))
                    {
                        existingLayout.Layout.Alias = layoutAliasPrefix + form.Name + layoutAliasSuffix;
                    }
                    LayoutPersistence.Persist(existingLayout.Layout);
                }


                // Success.
                result = new
                {
                    Success = true,
                    FormId = GuidHelper.GetString(formId)
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FormsController>(ex, PersistFormError);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return the result.
            return result;

        }


        /// <summary>
        /// Deletes the form with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to delete the form.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost()]
        public object DeleteForm(DeleteFormRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var formId = GuidHelper.GetGuid(request.FormId);
                var configs = ConFormPersistence.RetrieveChildren(formId);


                // Delete the form and its configurations.
                foreach (var item in configs)
                {
                    ConFormPersistence.Delete(item.Id);
                }

                Persistence.Delete(formId);


                // Success.
                result = new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FormsController>(ex, DeleteFormError);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return the result.
            return result;

        }


        /// <summary>
        /// Moves form to a new parent.
        /// </summary>
        /// <param name="request">
        /// The request to move the form.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about the form.
        /// </returns>
        [HttpPost]
        public object MoveForm(MoveFormRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var parentId = GuidHelper.GetGuid(request.NewParentId);


            // Catch all errors.
            try
            {

                // Declare list of anonymous type.
                var savedDescendants = new[]
                {
                    new
                    {
                        Id = string.Empty,
                        Path = new string[] { }
                    }
                }.Take(0).ToList();


                // Variables.
                var formId = GuidHelper.GetGuid(request.FormId);
                var form = Persistence.Retrieve(formId);
                var parentPath = Entities.Retrieve(parentId).Path;
                var oldFormPath = form.Path;
                var oldParentPath = oldFormPath.Take(oldFormPath.Length - 1).ToArray();
                var configs = ConFormPersistence.RetrieveChildren(formId);


                // Move form and configurations.
                var path = EntityHelper.GetClientPath(Entities.MoveEntity(form, parentPath));
                foreach (var config in configs)
                {
                    var descendantParentPath = config.Path.Take(config.Path.Length - 1);
                    var descendantPathEnd = descendantParentPath.Skip(oldParentPath.Length);
                    var newParentPath = parentPath.Concat(descendantPathEnd).ToArray();
                    var clientPath = EntityHelper.GetClientPath(
                        Entities.MoveEntity(config, newParentPath));
                    savedDescendants.Add(new
                    {
                        Id = GuidHelper.GetString(config.Id),
                        Path = clientPath
                    });
                }


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(formId),
                    Path = path,
                    Descendants = savedDescendants.ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<FormsController>(MoveFormError, ex);
                result = new
                {
                    Success = false,
                    Reason = UnhandledError
                };

            }


            // Return result.
            return result;

        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Returns all the form layouts that are descendants of the specified layout ID.
        /// </summary>
        /// <param name="parentId">
        /// The ID of the parent layout. If null, the search will start at the root.
        /// </param>
        /// <returns>
        /// The descendants of the specified layout.
        /// </returns>
        private IEnumerable<Layout> GetFormLayouts(Guid? parentId)
        {
            var children = LayoutPersistence.RetrieveChildren(parentId);
            foreach (var child in children)
            {
                var descendants = GetFormLayouts(child.Id);
                yield return child;
                foreach (var descendant in descendants)
                {
                    yield return descendant;
                }
            }
        }

        #endregion

    }

}