namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Extensions;
    using Forms;
    using Helpers;
    using Models.Requests;
    using Persistence;
    using Resolvers;
    using System;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using CoreConstants = Umbraco.Core.Constants;
    using FormConstants = formulate.app.Constants.Trees.Forms;


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

        private IFormPersistence Persistence { get; set; }
        private IEntityPersistence Entities { get; set; }
        private IValidationPersistence Validations { get; set; }
        private IConfiguredFormPersistence ConFormPersistence { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public FormsController(UmbracoContext context)
            : base(context)
        {
            Persistence = FormPersistence.Current.Manager;
            Entities = EntityPersistence.Current.Manager;
            Validations = ValidationPersistence.Current.Manager;
            ConFormPersistence = ConfiguredFormPersistence.Current.Manager;
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
                LogHelper.Error<FormsController>(GetFormInfoError, ex);
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
                var formId = string.IsNullOrWhiteSpace(request.FormId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.FormId);


                // Get the fields.
                var fields = request.Fields.MakeSafe()
                    .Select(x =>
                    {
                        var fieldType = Type.GetType(x.TypeFullName);
                        var genericType = typeof(FormField<>);
                        var specificType =
                            genericType.MakeGenericType(new[] { fieldType });
                        var field = Activator.CreateInstance(specificType)
                            as IFormField;
                        field.Id = string.IsNullOrWhiteSpace(x.Id)
                            ? Guid.NewGuid()
                            : GuidHelper.GetGuid(x.Id);
                        field.Alias = x.Alias;
                        field.Name = x.Name;
                        field.Label = x.Label;
                        field.Validations = x.Validations.MakeSafe()
                            .Select(y => GuidHelper.GetGuid(y)).ToArray();
                        field.FieldConfiguration =
                            JsonHelper.Serialize(x.Configuration);
                        return field;
                    })
                    .ToArray();


                // Get the handlers.
                var handlers = request.Handlers.MakeSafe().Select(x =>
                {
                    var handlerType = Type.GetType(x.TypeFullName);
                    var genericType = typeof(FormHandler<>);
                    var specificType =
                        genericType.MakeGenericType(new[] { handlerType });
                    var handler = Activator.CreateInstance(specificType)
                        as IFormHandler;
                    handler.Id = string.IsNullOrWhiteSpace(x.Id)
                        ? Guid.NewGuid()
                        : GuidHelper.GetGuid(x.Id);
                    handler.Alias = x.Alias;
                    handler.Name = x.Name;
                    handler.HandlerConfiguration =
                        JsonHelper.Serialize(x.Configuration);
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
                LogHelper.Error<FormsController>(PersistFormError, ex);
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
                configs.ForEach(x => ConFormPersistence.Delete(x.Id));
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
                LogHelper.Error<FormsController>(DeleteFormError, ex);
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
                LogHelper.Error<FormsController>(MoveFormError, ex);
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

    }

}