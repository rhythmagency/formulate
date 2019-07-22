namespace formulate.app.Controllers
{

    // Namespaces.

    using System;
    using System.Linq;
    using System.Web.Http;

    using core.Extensions;

    using DataValues;
    using DataValues.Suppliers;

    using ExtensionMethods;

    using formulate.app.CollectionBuilders;

    using Helpers;

    using Models.Requests;

    using Persistence;

    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;

    using CoreConstants = Umbraco.Core.Constants;
    using DataValuesConstants = Constants.Trees.DataValues;

    /// <summary>
    /// Controller for Formulate data values.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class DataValuesController : UmbracoAuthorizedJsonController
    {

        #region Constants

        private const string UnhandledError = @"An unhandled error occurred. Refer to the error log.";
        private const string PersistDataValueError = @"An error occurred while attempting to persist a Formulate data value.";
        private const string GetDataValueInfoError = @"An error occurred while attempting to get the data value info for a Formulate data value.";
        private const string DeleteDataValueError = @"An error occurred while attempting to delete the Formulate data value.";
        private const string GetKindsError = @"An error occurred while attempting to get the data value kinds.";
        private const string MoveDataValueError = @"An error occurred while attempting to move a Formulate data value.";
        private const string GetSuppliersError = @"An error occurred while attempting to get the data value suppliers.";

        #endregion


        #region Properties

        private IDataValuePersistence Persistence { get; set; }
        private IEntityPersistence Entities { get; set; }
        private DataValueKindCollection DataValueKindCollection { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public DataValuesController(IDataValuePersistence dataValuePersistence, IEntityPersistence entityPersistence, DataValueKindCollection dataValueKindCollection)
        {
            Persistence = dataValuePersistence;
            Entities = entityPersistence;
            DataValueKindCollection = dataValueKindCollection;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Creates a data value.
        /// </summary>
        /// <param name="request">
        /// The request to create the data value.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost]
        public object PersistDataValue(PersistDataValueRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var dataValuesRootId = GuidHelper.GetGuid(DataValuesConstants.Id);
            var parentId = GuidHelper.GetGuid(request.ParentId);
            var kindId = GuidHelper.GetGuid(request.KindId);


            // Catch all errors.
            try
            {

                // Parse or create the data value ID.
                var dataValueId = string.IsNullOrWhiteSpace(request.DataValueId)
                    ? Guid.NewGuid()
                    : GuidHelper.GetGuid(request.DataValueId);


                // Get the ID path.
                var parent = parentId == Guid.Empty ? null : Entities.Retrieve(parentId);
                var path = parent == null
                    ? new[] { dataValuesRootId, dataValueId }
                    : parent.Path.Concat(new[] { dataValueId }).ToArray();


                // Create data value.
                var dataValue = new DataValue()
                {
                    KindId = kindId,
                    Id = dataValueId,
                    Path = path,
                    Name = request.DataValueName,
                    Alias = request.DataValueAlias,
                    Data = JsonHelper.Serialize(request.Data)
                };


                // Persist data value.
                Persistence.Persist(dataValue);


                // Variables.
                var fullPath = new[] { rootId }
                    .Concat(path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(dataValueId),
                    Path = fullPath
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, PersistDataValueError);
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
        /// Returns info about the data value with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to get the data value info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetDataValueInfo(
            [FromUri] GetDataValueInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var id = GuidHelper.GetGuid(request.DataValueId);
                var dataValue = Persistence.Retrieve(id);
                var fullPath = new[] { rootId }
                    .Concat(dataValue.Path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();
                var kinds = DataValueKindCollection;
                var directive = kinds.Where(x => x.Id == dataValue.KindId)
                    .Select(x => x.Directive).FirstOrDefault();


                // Set result.
                result = new
                {
                    Success = true,
                    DataValueId = GuidHelper.GetString(dataValue.Id),
                    KindId = GuidHelper.GetString(dataValue.KindId),
                    Path = fullPath,
                    Alias = dataValue.Alias,
                    Name = dataValue.Name,
                    Directive = directive,
                    Data = JsonHelper.Deserialize<object>(dataValue.Data)
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, GetDataValueInfoError);
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
        /// Returns info about the data values with the specified ID's.
        /// </summary>
        /// <param name="request">
        /// The request to get the data values info.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpGet]
        public object GetDataValuesInfo(
            [FromUri] GetDataValuesInfoRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();


            // Catch all errors.
            try
            {

                // Variables.
                var ids = request.DataValueIds
                    .Select(x => GuidHelper.GetGuid(x));
                var dataValues = ids.Select(x => Persistence.Retrieve(x))
                    .WithoutNulls();
                var kinds = DataValueKindCollection;
                var combined = dataValues.Join(kinds,
                    x => x.KindId,
                    y => y.Id,
                    (x, y) => new
                    {
                        Value = x,
                        Kind = y
                    });


                // Set result.
                result = new
                {
                    Success = true,
                    DataValues = combined.Select(x => new 
                    {
                        DataValueId = GuidHelper.GetString(x.Value.Id),
                        KindId = GuidHelper.GetString(x.Value.KindId),
                        Path = new[] { rootId }
                            .Concat(GuidHelper.GetStrings(x.Value.Path))
                            .ToArray(),
                        Alias = x.Value.Alias,
                        Name = x.Value.Name,
                        Directive = x.Kind.Directive,
                        Data = JsonHelper.Deserialize<object>(x.Value.Data)
                    }),
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, GetDataValueInfoError);
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
        /// Deletes the data value with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request to delete the data value.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with some
        /// accompanying data.
        /// </returns>
        [HttpPost()]
        public object DeleteDataValue(DeleteDataValueRequest request)
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var dataValueId = GuidHelper.GetGuid(request.DataValueId);


                // Delete the data value.
                Persistence.Delete(dataValueId);


                // Success.
                result = new
                {
                    Success = true
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, DeleteDataValueError);
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
        /// Returns the data value kinds.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about data value kinds.
        /// </returns>
        [HttpGet]
        public object GetDataValueKinds()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var kinds = DataValueKindCollection;


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
                Logger.Error<DataValuesController>(ex, GetKindsError);
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
        /// Returns the data value suppliers.
        /// </summary>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about data value suppliers.
        /// </returns>
        [HttpGet]
        public object GetDataValueSuppliers()
        {

            // Variables.
            var result = default(object);


            // Catch all errors.
            try
            {

                // Variables.
                var suppliers = ReflectionHelper
                    .InstantiateInterfaceImplementations<ISupplyValueAndLabelCollection>()
                    .OrderBy(x => x.Name);


                // Return results.
                result = new
                {
                    Success = true,
                    Kinds = suppliers.Select(x => new
                    {
                        Name = x.Name,
                        ClassName = x.GetType().ShortAssemblyQualifiedName()
                    }).ToArray()
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, GetSuppliersError);
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
        /// Moves data value to a new parent.
        /// </summary>
        /// <param name="request">
        /// The request to move the data value.
        /// </param>
        /// <returns>
        /// An object indicating success or failure, along with information
        /// about the data value.
        /// </returns>
        [HttpPost]
        public object MoveDataValue(MoveDataValueRequest request)
        {

            // Variables.
            var result = default(object);
            var rootId = CoreConstants.System.Root.ToInvariantString();
            var parentId = GuidHelper.GetGuid(request.NewParentId);


            // Catch all errors.
            try
            {

                // Parse the data value ID.
                var dataValueId = GuidHelper.GetGuid(request.DataValueId);


                // Get the ID path.
                var path = Entities.Retrieve(parentId).Path
                    .Concat(new[] { dataValueId }).ToArray();


                // Get data value and update path.
                var dataValue = Persistence.Retrieve(dataValueId);
                dataValue.Path = path;


                // Persist data value.
                Persistence.Persist(dataValue);


                // Variables.
                var fullPath = new[] { rootId }
                    .Concat(path.Select(x => GuidHelper.GetString(x)))
                    .ToArray();


                // Success.
                result = new
                {
                    Success = true,
                    Id = GuidHelper.GetString(dataValueId),
                    Path = fullPath
                };

            }
            catch (Exception ex)
            {

                // Error.
                Logger.Error<DataValuesController>(ex, MoveDataValueError);
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