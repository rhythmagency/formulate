namespace formulate.app.Forms.Handlers.StoreData
{

    // Namespaces.
    using Helpers;
    using Managers;
    using Persistence.Internal.Sql.Models;
    using Resolvers;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;

    /// <summary>
    /// A form submission handler that stores the submitted data.
    /// </summary>
    public class StoreDataHandler : IFormHandlerType
    {

        #region Private Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return Configuration.Current.Manager;
            }
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// The Angular directive that renders this handler.
        /// </summary>
        public string Directive => "formulate-submission-handler";


        /// <summary>
        /// The icon shown in the picker dialog.
        /// </summary>
        public string Icon => "icon-formulate-submission";


        /// <summary>
        /// The ID that uniquely identifies this handler (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("238EA92071F44D8C9CC433D7181C9C46");


        /// <summary>
        /// The label that appears when the user is choosing the handler.
        /// </summary>
        public string TypeLabel => "Store Data";

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserializes the configuration for a store data handler.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        /// <remarks>
        /// In this case, no deserialization is necessary.
        /// </remarks>
        public object DeserializeConfiguration(string configuration)
        {
            return null;
        }


        /// <summary>
        /// Prepares to handle to form submission.
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        /// <param name="configuration">
        /// The handler configuration.
        /// </param>
        /// <remarks>
        /// In this case, no preparation is necessary.
        /// </remarks>
        public void PrepareHandleForm(FormSubmissionContext context, object configuration)
        {
        }


        /// <summary>
        /// Handles a form submission (stores it).
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        /// <param name="configuration">
        /// The handler configuration.
        /// </param>
        public void HandleForm(FormSubmissionContext context, object configuration)
        {

            // Variables.
            var form = context.Form;
            var data = context.Data;
            var files = context.Files;
            var payload = context.Payload;
            var fieldsById = form.Fields.ToDictionary(x => x.Id, x => x);
            var db = context.UmbracoContext.Application.DatabaseContext.Database;


            // This will store the formatted values.
            var valueList = new[]
            {
                new
                {
                    Key = default(string),
                    Value = default(string)
                }
            }.Take(0).ToList();


            // Group the data values by their field ID.
            var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Values = x.SelectMany(y => y.FieldValues).ToList()
            }).ToDictionary(x => x.Id, x => x.Values);


            // Store the file values by their field ID.
            var filesById = files.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Filename = x.Select(y => y.FileName).FirstOrDefault(),
                PathSegment = GenerateFilePathSegment(),
                FileData = x.Select(y => y.FileData).FirstOrDefault()
            }).ToDictionary(x => x.Id, x => x);


            // Normal fields.
            foreach (var key in valuesById.Keys)
            {
                var values = valuesById[key];
                var formatted = string.Join(", ", values);
                var field = default(IFormField);
                if (fieldsById.TryGetValue(key, out field))
                {
                    formatted = field.FormatValue(values, FieldPresentationFormats.Storage);
                }
                valueList.Add(new
                {
                    Key = GuidHelper.GetString(key),
                    Value = formatted
                });
            }


            // Store file information for serialization.
            var fileList = filesById.Values.Select(x => new[]
            {
                new
                {
                    Key = x.Id,
                    PathSegment = x.PathSegment,
                    Filename = x.Filename
                }
            });


            // Store the files.
            if (files.Any())
            {

                // Ensure base path exists.
                var basePath = HostingEnvironment.MapPath(Config.FileStoreBasePath);
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }


                // Create files.
                foreach(var key in filesById.Keys)
                {
                    var file = filesById[key];
                    var fullPath = Path.Combine(basePath, file.PathSegment);
                    var pathOnly = Path.GetDirectoryName(fullPath);
                    Directory.CreateDirectory(pathOnly);
                    File.WriteAllBytes(fullPath, file.FileData);
                }

            }


            // Store data to database.
            var serializedValues = JsonHelper.Serialize(valueList.ToArray());
            var serializedFiles = JsonHelper.Serialize(fileList.ToArray());
            db.Insert(new FormulateSubmission()
            {
                CreationDate = DateTime.Now,
                DataValues = serializedValues,
                FileValues = serializedFiles,
                FormId = form.Id,
                GeneratedId = context.SubmissionId,
                PageId = context?.CurrentPage?.Id,
                Url = context?.CurrentPage?.Url
            });

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Generates a file path segment.
        /// </summary>
        /// <returns>
        /// The file path segment (e.g., "2016-1-1/00000000000000000").
        /// </returns>
        /// <remarks>
        /// This is used to generate unique paths so lots of files can be stored without
        /// the names colliding.
        /// </remarks>
        private string GenerateFilePathSegment()
        {
            var guid = Guid.NewGuid();
            var strGuid = GuidHelper.GetString(guid);
            var now = DateTime.Now.ToString("yyyy-MM-dd");
            return $"{now}/{strGuid}";
        }

        #endregion

    }

}