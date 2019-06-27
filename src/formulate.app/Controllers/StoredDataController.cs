namespace formulate.app.Controllers
{

    // Namespaces.
    using core.Extensions;
    using Forms;
    using Helpers;
    using Managers;
    using Models.Requests;
    using Persistence;
    using Persistence.Internal.Sql.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using NPoco;

    using Umbraco.Core.Logging;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Scoping;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;


    /// <summary>
    /// Controller for stored data from form submissions.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class StoredDataController : UmbracoAuthorizedJsonController
    {

        #region Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }


        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Forms { get; set; }


        private IScopeProvider ScopeProvider { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoredDataController(IConfigurationManager configurationManager, IFormPersistence formPersistence, IScopeProvider scopeProvider)
        {
            Config = configurationManager;
            Forms = formPersistence;
            ScopeProvider = scopeProvider;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Gets form submissions based on the specified constraints.
        /// </summary>
        /// <param name="request">
        /// The constraints.
        /// </param>
        /// <returns>
        /// The form submissions.
        /// </returns>
        [HttpGet]
        public object GetSubmissions([FromUri] GetStoredDataRequest request)
        {

            // Variables.
            var formId = GuidHelper.GetGuid(request.FormId);
            var form = Forms.Retrieve(formId);
            var fieldsById = form.Fields.ToDictionary(x => x.Id, x => x);

            var dbResults = GetPagedResults(request, formId);
            var items = dbResults.Items;

            // If the form was not found, indicate an error.
            if (form == null)
            {
                return new
                {
                    Success = false
                    //TODO: Failure message?
                };
            }


            // Return form submissions.
            return new
            {
                Success = true,
                Total = (int)dbResults.TotalItems,
                Submissions = items.Select(x => new
                {
                    GeneratedId = GuidHelper.GetString(x.GeneratedId),
                    CreationDate = x.CreationDate.AddMinutes(-request.TimezoneOffset).ToString(),
                    Url = x.Url,
                    PageId = x.PageId,
                    Fields = GetDataForFields(x.DataValues, fieldsById, form),
                    Files = GetDataForFiles(x.FileValues, fieldsById, form)
                }).ToArray()
            };

        }

        private Page<FormulateSubmission> GetPagedResults(GetStoredDataRequest request, Guid formId)
        {
            using (var scope = ScopeProvider.CreateScope())
            {
                var dbResults = scope.Database.Page<FormulateSubmission>(request.Page, request.ItemsPerPage,
                    "WHERE FormId = @0 ORDER BY SequenceId DESC", formId);

                return dbResults;
            }
        }

        /// <summary>
        /// Deletes the form submission with the specified ID.
        /// </summary>
        /// <param name="request">
        /// The request containing the ID of the submission to delete.
        /// </param>
        /// <returns>
        /// An object indicating whether or not the deletion was successful.
        /// </returns>
        [HttpPost]
        public object DeleteSubmission(DeleteSubmissionRequest request)
        {
            // Variables.
            var id = GuidHelper.GetGuid(request.GeneratedId);

            // Delete the submission.
            using (var scope = ScopeProvider.CreateScope())
            {
                scope.Database.Delete("FormulateSubmission", "GeneratedId", null, id);
            }
            
            // Indicate success.
            return new
            {
                Success = true
            };

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Returns the anonymous object array for the specified form fields.
        /// </summary>
        /// <param name="strJson">
        /// The serialized form fields.
        /// </param>
        /// <param name="fieldsById">
        /// The form field information, stored in a dictionary by field ID.
        /// </param>
        /// <param name="form">
        /// The form the fields relate to.
        /// </param>
        /// <returns>
        /// The anonymous object array containing field information.
        /// </returns>
        private object GetDataForFields(string strJson, Dictionary<Guid, IFormField> fieldsById, Form form)
        {

            // Create a list to store the field data in.
            var fieldInfo = new[]
            {
                new
                {
                    Name = default(string),
                    Value = default(string),
                    Id = default(Guid)
                }
            }.Take(0).ToList();


            // Deserialize the field data, gather extra field info, and populate the field info.
            var deserialized = JsonHelper.Deserialize<dynamic>(strJson);
            foreach (var field in deserialized)
            {

                // Variables.
                var strGuid = (field.FieldId as object)?.ToString();
                var guid = GuidHelper.GetGuid(strGuid);
                var value = (field.Value as object)?.ToString();
                var fieldName = (field.FieldName as object).ToString();
                var formField = default(IFormField);


                // Attempt to get the current field name.
                if (fieldsById.TryGetValue(guid, out formField))
                {
                    fieldName = formField.Name;
                }


                // Remember the field info.
                fieldInfo.Add(new
                {
                    Name = fieldName,
                    //TODO: Transform for presentation in grid?
                    Value = value,
                    Id = guid
                });

            }

            // Sort fields by their order in the form.
            var fieldInfoById = fieldInfo.ToDictionary(x => x.Id, x => x);
            var orderedFieldIds = fieldInfo.Select(x => x.Id)
                .OrderByCollection(form.Fields.Select(y => y.Id));

            // Return the array of field information.
            return orderedFieldIds
                .Select(x => fieldInfoById[x])
                .Select(x => new
                {
                    Name = x.Name,
                    Value = x.Value
                })
                .ToArray();

        }


        /// <summary>
        /// Returns the anonymous object array for the specified form file fields.
        /// </summary>
        /// <param name="strJson">
        /// The serialized form file fields.
        /// </param>
        /// <param name="fieldsById">
        /// The form field information, stored in a dictionary by field ID.
        /// </param>
        /// <param name="form">
        /// The form the file fields relate to.
        /// </param>
        /// <returns>
        /// The anonymous object array containing file field information.
        /// </returns>
        private object GetDataForFiles(string strJson, Dictionary<Guid, IFormField> fieldsById, Form form)
        {

            // Create a list to store the field data in.
            var fieldInfo = new[]
            {
                new
                {
                    Name = default(string),
                    Filename = default(string),
                    PathSegment = default(string),
                    Id = default(Guid)
                }
            }.Take(0).ToList();


            // Deserialize the field data, gather extra field info, and populate the field info.
            var deserialized = JsonHelper.Deserialize<dynamic>(strJson);
            foreach (var field in deserialized)
            {

                // Variables.
                var strGuid = (field.FieldId as object)?.ToString();
                var guid = GuidHelper.GetGuid(strGuid);
                var fieldName = (field.FieldName as object).ToString();
                var pathSegment = (field.PathSegment as object).ToString();
                var filename = (field.Filename as object).ToString();
                var formField = default(IFormField);


                // Attempt to get the current field name.
                if (fieldsById.TryGetValue(guid, out formField))
                {
                    fieldName = formField.Name;
                }


                // Remember the field info.
                fieldInfo.Add(new
                {
                    Name = fieldName,
                    Filename = filename,
                    PathSegment = pathSegment,
                    Id = guid
                });

            }


            // Sort fields by their order in the form.
            var fieldInfoById = fieldInfo.ToDictionary(x => x.Id, x => x);
            var orderedFieldIds = fieldInfo.Select(x => x.Id)
                .OrderByCollection(form.Fields.Select(y => y.Id));


            // Return the array of field information.
            return orderedFieldIds
                .Select(x => fieldInfoById[x])
                .Select(x => new
                {
                    Name = x.Name,
                    Filename = x.Filename,
                    PathSegment = x.PathSegment
                })
                .ToArray();

        }

        #endregion

    }

}