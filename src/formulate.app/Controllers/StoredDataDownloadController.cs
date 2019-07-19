namespace formulate.app.Controllers
{

    // Namespaces.
    using ControllerTypes;
    using CsvHelper;
    using Forms;
    using Helpers;
    using Managers;
    using Models.Requests;
    using Persistence;
    using Persistence.Internal.Sql.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Hosting;
    using System.Web.Http;

    using NPoco;

    using Umbraco.Core.Persistence;
    using Umbraco.Core.Scoping;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;
    using Umbraco.Web.WebApi.Filters;

    /// <summary>
    /// Controller for downloading stored file data.
    /// </summary>
    [PluginController("formulate")]
    [UmbracoApplicationAuthorize("formulate")]
    public class StoredDataDownloadController : UmbracoAuthorizedApiController
    {
        #region Properties
        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Persistence { get; set; }

        private IScopeProvider ScopeProvider { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StoredDataDownloadController(IFormPersistence formPersistence, IConfigurationManager configurationManager, IScopeProvider scopeProvider)
        {
            Persistence = formPersistence;
            Config = configurationManager;
            ScopeProvider = scopeProvider;
        }

        #endregion


        #region Web Methods

        /// <summary>
        /// Returns a file download.
        /// </summary>
        /// <param name="request">
        /// The request for the file to download.
        /// </param>
        /// <returns>
        /// The file download.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage DownloadFile([FromUri] GetFileDownloadRequest request)
        {

            // Variables.
            var basePath = HostingEnvironment.MapPath(Config.FileStoreBasePath);
            var fullPath = Path.Combine(basePath, request.PathSegment);
            var fileData = File.ReadAllBytes(fullPath);


            // Construct download result.
            var result = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(fileData),
                StatusCode = HttpStatusCode.OK
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");


            // Return download result.
            return result;

        }


        /// <summary>
        /// Returns a CSV export.
        /// </summary>
        /// <param name="request">
        /// The request for the CSV export.
        /// </param>
        /// <returns>
        /// The file download.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage DownloadCsvExport([FromUri] GetCsvExportRequest request)
        {

            // Variables.
            var formId = GuidHelper.GetGuid(request.FormId);
            var form = Persistence.Retrieve(formId);
            var formName = SanitizeFilename(form.Name);
            var filename = $"{formName} Export.csv";
            var fileData = GenerateCsvOfFormSubmissions(form);

            // Construct download result.
            var result = new HttpResponseMessage()
            {
                Content = new ByteArrayContent(fileData),
                StatusCode = HttpStatusCode.OK
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = filename
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");


            // Return download result.
            return result;

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Generates a CSV of the form submissions for the specified form.
        /// </summary>
        /// <param name="form">
        /// The form to generate the CSV for.
        /// </param>
        /// <returns>
        /// The CSV.
        /// </returns>
        private byte[] GenerateCsvOfFormSubmissions(Form form)
        {

            // Variables.
            var formId = form.Id;
            var filteredFields = form.Fields.Where(x => !x.IsTransitory);
            var fieldIds = filteredFields.Select(x => x.Id).ToArray();
            var fieldsById = filteredFields.ToDictionary(x => x.Id, x => x);
            var extractedValues = new List<SubmissionExport>();


            // Query database for form submissions.

            var entries = FetchEntries(formId);

            // Extract field values from the database entries.
            foreach (var entry in entries)
            {
                var extractedRow = new Dictionary<Guid, string>();
                var exportRow = new SubmissionExport()
                {
                    Submission = entry,
                    Values = extractedRow
                };
                extractedValues.Add(exportRow);
                var values = GetValuesForFields(entry.DataValues);
                var filenames = GetValuesForFiles(entry.FileValues);
                foreach (var fieldId in fieldIds)
                {
                    var value = default(string);
                    if (values.TryGetValue(fieldId, out value))
                    {
                    }
                    else if (filenames.TryGetValue(fieldId, out value))
                    {
                    }
                    else
                    {
                        value = null;
                    }
                    extractedRow[fieldId] = value;
                }
            }


            // Store to a CSV.
            var config = new CsvHelper.Configuration.Configuration()
            {
                HasHeaderRecord = true,
                ShouldQuote = (field, context) => true
            };
            using (var memStream = new MemoryStream())
            {

                // Open stream/writer.
                using (var textWriter = new StreamWriter(memStream))
                using (var writer = new CsvWriter(textWriter, config))
                {
                    // Write headers.
                    writer.WriteField("Creation Date");
                    writer.WriteField("URL");
                    writer.WriteField("Page ID");
                    foreach (var fieldId in fieldIds)
                    {
                        var field = fieldsById[fieldId];
                        writer.WriteField(field.Name);
                    }
                    writer.NextRecord();


                    // Write values.
                    foreach (var row in extractedValues)
                    {
                        writer.WriteField(row.Submission.CreationDate);
                        writer.WriteField(row.Submission.Url);
                        writer.WriteField(row.Submission.PageId);
                        foreach (var fieldId in fieldIds)
                        {
                            var value = default(string);
                            if (!row.Values.TryGetValue(fieldId, out value))
                            {
                                value = null;
                            }
                            writer.WriteField(value);
                        }
                        writer.NextRecord();
                    }

                }


                // Return CSV as a byte array.
                return memStream.ToArray();

            }

        }

        private IEnumerable<FormulateSubmission> FetchEntries(Guid formId)
        {
            using (var scope = ScopeProvider.CreateScope(autoComplete: true))
            {
                var query = new Sql<ISqlContext>(scope.SqlContext)
                            .Select("*")
                            .From<FormulateSubmission>()
                            .Where("FormId = @0", formId)
                            .OrderByDescending("CreationDate");

                return scope.Database.Fetch<FormulateSubmission>(query).ToList();
            }
        }

        /// <summary>
        /// Sanitizes a source string for use as a filename.
        /// </summary>
        /// <param name="source">
        /// The source string to sanitize.
        /// </param>
        /// <returns>
        /// The sanitized string.
        /// </returns>
        private string SanitizeFilename(string source)
        {
            var invalidChars = new HashSet<char>(Path.GetInvalidFileNameChars());
            var sanitized = (source ?? string.Empty).Where(x => !invalidChars.Contains(x));
            return new string(sanitized.ToArray());
        }


        /// <summary>
        /// Extracts fields values from the serialized form fields.
        /// </summary>
        /// <param name="strJson">
        /// The serialized form fields.
        /// </param>
        /// <returns>
        /// Field values, stored by field ID.
        /// </returns>
        private Dictionary<Guid, string> GetValuesForFields(string strJson)
        {

            // Variables.
            var valuesById = new Dictionary<Guid, string>();


            // Deserialize the field data, and store each field value by field ID.
            var deserialized = JsonHelper.Deserialize<dynamic>(strJson);
            foreach (var field in deserialized)
            {

                // Variables.
                var strGuid = (field.FieldId as object)?.ToString();
                var guid = GuidHelper.GetGuid(strGuid);
                var value = (field.Value as object)?.ToString();

                // Store field value by field ID.
                valuesById[guid] = value;

            }


            // Return the dictionary of field values.
            return valuesById;

        }


        /// <summary>
        /// Extracts filenames from the serialized form fields.
        /// </summary>
        /// <param name="strJson">
        /// The serialized form fields.
        /// </param>
        /// <returns>
        /// Filenames, stored by field ID.
        /// </returns>
        private Dictionary<Guid, string> GetValuesForFiles(string strJson)
        {

            // Variables.
            var filenamesById = new Dictionary<Guid, string>();


            // Deserialize the field data, and store each filename by field ID.
            var deserialized = JsonHelper.Deserialize<dynamic>(strJson);
            foreach (var field in deserialized)
            {

                // Variables.
                var strGuid = (field.FieldId as object)?.ToString();
                var guid = GuidHelper.GetGuid(strGuid);
                var filename = (field.Filename as object).ToString();

                // Store filename by field ID.
                filenamesById[guid] = filename;

            }


            // Return the dictionary of filenames.
            return filenamesById;

        }

        #endregion

    }

}