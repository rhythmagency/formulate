namespace formulate.app.Controllers
{

    // Namespaces.
    using Helpers;
    using Models.Requests;
    using Persistence;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using Validations;
    using CoreConstants = Umbraco.Core.Constants;
    using ValidationConstants = formulate.app.Constants.Trees.Validations;


    /// <summary>
    /// Controller for Formulate validations.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class ValidationsController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string PersistValidationError = @"An error occurred while attempting to persist a Formulate validation.";
        private const string GetValidationInfoError = @"An error occurred while attempting to get the validation info for a Formulate validation.";
        private const string DeleteValidationError = @"An error occurred while attempting to delete the Formulate validation.";
        private const string GetKindsError = @"An error occurred while attempting to get the validation kinds.";
        private const string MoveValidationError = @"An error occurred while attempting to move a Formulate validation.";

        #endregion


        #region Properties

        private IValidationPersistence Persistence { get; set; }
        private IEntityPersistence Entities { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ValidationsController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public ValidationsController(UmbracoContext context)
            : base(context)
        {
            Persistence = ValidationPersistence.Current.Manager;
            Entities = EntityPersistence.Current.Manager;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Creates a validation.
        /// </summary>
        /// <param name="request">
        /// The request to create the validation.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost]
        public object PersistValidation(PersistValidationRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var validationsRootId = GuidHelper.GetGuid(ValidationConstants.Id);
            var parentId = GuidHelper.GetGuid(request.ParentId);
            var kindId = GuidHelper.GetGuid(request.KindId);


            // Catch all errors.
            try
            {

                // Parse or create the validation ID.
                var validationId = string.IsNullOrWhiteSpace(request.ValidationId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.ValidationId);


                // Get the ID path.
                var parent = parentId == Guid.Empty ? null : Entities.Retrieve(parentId);
                var path = parent == null
                    ? new[] { validationsRootId, validationId }
                    : parent.Path.Concat(new[] { validationId }).ToArray();


                // Create validation.
                var validation = new Validation()
                {
                    KindId = kindId,
                    Id = validationId,
                    Path = path,
                    Name = request.ValidationName,
                    Alias = request.ValidationAlias,
                    Data = JsonHelper.Serialize(request.Data)
                };


                // Persist validation.
                Persistence.Persist(validation);


                // Variables.
                var fullPath = new[] { rootId }
                    .Concat(path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(validationId),
                    Path = fullPath
                };

            }
            catch(Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(PersistValidationError, ex);
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
        /// Returns info about the validation with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the validation info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetValidationInfo(
            [FromUri] GetValidationInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.ValidationId);
                var validation = Persistence.Retrieve(id);
                var fullPath = new[] { rootId }
                    .Concat(validation.Path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();
                var kinds = ValidationHelper.GetAllValidationKinds();
                var directive = kinds.Where(x => x.Id == validation.KindId)
                    .Select(x => x.Directive).FirstOrDefault();


                // Set result.
                result = new
                {
                    Success = true,
                    ValidationId = GuidHelper.GetString(validation.Id),
                    KindId = GuidHelper.GetString(validation.KindId),
                    Path = fullPath,
                    Alias = validation.Alias,
                    Name = validation.Name,
                    Directive = directive,
                    Data = JsonHelper.Deserialize<object>(validation.Data)
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(GetValidationInfoError, ex);
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
        /// Returns info about the validations with the specified IDs.
        /// </summary>
        /// <param name="request">
        /// The request to get the info on the validations.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetValidationsInfo(
            [FromUri] GetValidationsInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var validations = new List<object>();


            // Catch all errors.
            try
            {

                // Variables.
                var ids = request.ValidationIds
                    .Select(x => GuidHelper.GetGuid(x)).ToList();
                var kinds = ValidationHelper.GetAllValidationKinds();


                // Get information about each validation.
                foreach (var id in ids)
                {

                    // Get validation.
                    var validation = Persistence.Retrieve(id);
                    if (validation == null)
                    {
                        continue;
                    }


                    // Get path to validation.
                    var partialPath = validation.Path
                        .Select(x => GuidHelper.GetString(x));
                    var fullPath = new[] { rootId }
                        .Concat(partialPath).ToArray();


                    // Get directive.
                    var directive = kinds.Where(x => x.Id == validation.KindId)
                        .Select(x => x.Directive).FirstOrDefault();


                    // Store validation info.
                    validations.Add(new
                    {
                        ValidationId = GuidHelper.GetString(validation.Id),
                        KindId = GuidHelper.GetString(validation.KindId),
                        Path = fullPath,
                        Alias = validation.Alias,
                        Name = validation.Name,
                        Directive = directive,
                        Data = JsonHelper.Deserialize<object>(validation.Data)
                    });

                }


                // Set result.
                result = new
                {
                    Success = true,
                    Validations = validations.ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(GetValidationInfoError, ex);
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
        /// Deletes the validation with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to delete the validation.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost()]
        public object DeleteValidation(DeleteValidationRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var validationId = GuidHelper.GetGuid(request.ValidationId);


                // Delete the validation.
                Persistence.Delete(validationId);


                // Success.
                result = new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(DeleteValidationError, ex);
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
        /// Returns the validation kinds.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about validation kinds.
        /// </returns>
        [HttpGet]
        public object GetValidationKinds()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var kinds = ValidationHelper.GetAllValidationKinds();


                // Return results.
                result = new
                {
                    Success = true,
                    Kinds = kinds.Select(x => new
                    {
                        Id = GuidHelper.GetString(x.Id),
                        Name = x.Name,
                        Directive = x.Directive
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(GetKindsError, ex);
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
        /// Moves validation to a new parent.
        /// </summary>
        /// <param name="request">
        /// The request to move the validation.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about the validation.
        /// </returns>
        [HttpPost]
        public object MoveValidation(MoveValidationRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var parentId = GuidHelper.GetGuid(request.NewParentId);


            // Catch all errors.
            try
            {

                // Parse the validation ID.
                var validationId = GuidHelper.GetGuid(request.ValidationId);


                // Get the ID path.
                var path = Entities.Retrieve(parentId).Path
                    .Concat(new[] { validationId }).ToArray();


                // Get validation and update path.
                var validation = Persistence.Retrieve(validationId);
                validation.Path = path;


                // Persist validation.
                Persistence.Persist(validation);


                // Variables.
                var fullPath = new[] { rootId }
                    .Concat(path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(validationId),
                    Path = fullPath
                };

            }
            catch (Exception ex)
            {

                // Error.
                LogHelper.Error<ValidationsController>(MoveValidationError, ex);
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