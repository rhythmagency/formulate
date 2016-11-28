namespace formulate.app.Controllers
{

    // Namespaces.
    using Forms;
    using Helpers;
    using Managers;
    using Models.Requests;
    using Persistence;
    using Persistence.Internal.Sql.Models;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Umbraco.Web;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi.Filters;
    using ResolverConfig = Resolvers.Configuration;


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
        private IConfigurationManager Config
        {
            get
            {
                return ResolverConfig.Current.Manager;
            }
        }


        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Forms { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoredDataController()
            : this(UmbracoContext.Current)
        {
        }


        /// <summary>
        /// Primary constructor.
        /// </summary>
        /// <param name="context">Umbraco context.</param>
        public StoredDataController(UmbracoContext context)
            : base(context)
        {
            Forms = FormPersistence.Current.Manager;
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
            var db = ApplicationContext.DatabaseContext.Database;
            var dbResults = db.Page<FormulateSubmission>(request.Page, request.ItemsPerPage,
                "WHERE FormId = @0 ORDER BY SequenceId DESC", formId);
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
                    CreationDate = x.CreationDate.AddMinutes(-request.TimezoneOffset).ToString(),
                    Url = x.Url,
                    PageId = x.PageId,
                    Fields = GetDataForFields(x.DataValues, fieldsById),
                    //TODO: Account for files.
                    Files = new[] { new { } }.Take(0).ToArray()
                }).ToArray()
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
        /// <returns>
        /// The anonymous object array containing field information.
        /// </returns>
        private object GetDataForFields(string strJson, Dictionary<Guid, IFormField> fieldsById)
        {

            // Create a list to store the field data in.
            var fieldInfo = new[]
            {
                new
                {
                    Name = default(string),
                    Value = default(string)
                }
            }.Take(0).ToList();


            // Deserialize the field data, gather extra field info, and populate the field info.
            var deserialied = JsonHelper.Deserialize<dynamic>(strJson);
            foreach (var field in deserialied)
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
                    Value = value
                });

            }


            // Return the array of field information.
            return fieldInfo.ToArray();

        }

        #endregion

    }

}