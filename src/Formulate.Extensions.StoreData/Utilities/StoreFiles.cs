namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Extensions.StoreData.Models;
    using Microsoft.AspNetCore.Hosting;
    using Umbraco.Extensions;

    internal sealed class StoreFiles : IStoreFiles
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StoreFiles(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Store form submission files in a location.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A readonly collection of <see cref="StoreDataEntry"/>.</returns>
        /// <remarks>
        /// <para>
        /// By default, files will be stored in a file system folder based on submission ID followed by field ID.
        /// </para>
        /// <para>The returned values provide a value so the Formulate backoffice can access them.</para>
        /// </remarks>
        public IReadOnlyCollection<StoreDataEntry> Execute(StoreFilesInput input)
        {
            var entries = new List<StoreDataEntry>();
            var files = input.Files;

            if (files.Any() == false)
            {
                return entries;
            }

            var configBasePath = "/App_Data/Formulate/StoreData";
            var basePath = _webHostEnvironment.MapPathWebRoot(configBasePath);

            CreateDirectoryIfMissing(basePath);

            var formattedSubmissionId = input.SubmissionId.ToString("N");
            var submissionPath = Path.Combine(basePath, formattedSubmissionId);

            CreateDirectoryIfMissing(submissionPath);

            var form = input.Form;

            // Normal fields.
            foreach (var kvp in files)
            {
                var values = kvp.Value.GetValues();
                var field = form.Fields.FirstOrDefault(x => x.Id == kvp.Key);

                if (field is null)
                {
                    continue;
                }

                var formattedFieldId = field.Id.ToString("N");
                var fieldPath = Path.Combine(submissionPath, formattedFieldId);

                CreateDirectoryIfMissing(fieldPath);

                foreach (var file in values)
                {
                    var fileSystemPath = Path.Combine(fieldPath, file.Name);
                    
                    // TODO: Add code that allows for multiple uploads where the file name is the same.

                    File.WriteAllBytes(fileSystemPath, file.Data);

                    entries.Add(new StoreDataEntry()
                    {
                        FieldId = field.Id,
                        FieldName = field.Name,
                        Value = file.Name,
                    });
                }
            }

            return entries;
        }

        private static void CreateDirectoryIfMissing(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}