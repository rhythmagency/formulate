namespace formulate.app.Forms.Handlers.StoreData
{

    // Namespaces.
    using Helpers;
    using System;
    using System.Linq;

    /// <summary>
    /// A form submission handler that stores the submitted data.
    /// </summary>
    public class StoreDataHandler : IFormHandlerType
    {

        #region Properties

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


            // This will store the formatted values.
            var valueList = new[]
            {
                new
                {
                    Key = default(string),
                    Value = default(string)
                }
            }.Take(0).ToList();


            // This will store the file information.
            var fileList = new[]
            {
                new
                {
                    Key = default(string),
                    PathSegment = default(string),
                    Filename = default(string)
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
            }).ToDictionary(x => x.Id, x => x.Filename);


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


            //TODO: Store files (in a configured path).


            //TODO: Store data.
            var serializedValues = JsonHelper.Serialize(valueList.ToArray());
            var serializedFiles = JsonHelper.Serialize(fileList.ToArray());
            // Database fields:
            //   Sequence ID.
            //   Generated ID.
            //   Date / Time.
            //   Form ID.
            //   Values.
            //   File Values.
            //   URL.
            //   Page ID.

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