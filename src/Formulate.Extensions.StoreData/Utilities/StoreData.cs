namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Core.Submissions.Requests;
    using Formulate.Core.Utilities;
    using Formulate.Extensions.StoreData.Models;
    using Umbraco.Cms.Infrastructure.Scoping;

    internal sealed class StoreData : IStoreData
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly IJsonUtility _jsonHelper;
        private readonly IStoreFiles _storeFiles;
        private readonly IStoreFields _storeFields;

        public StoreData(IStoreFields storeFields, IStoreFiles storeFiles, IJsonUtility jsonHelper, IScopeProvider scopeProvider)
        {
            _storeFields = storeFields;
            _storeFiles = storeFiles;
            _jsonHelper = jsonHelper;
            _scopeProvider = scopeProvider;
        }

        public async Task ExecuteAsync(FormSubmissionRequest submission, CancellationToken cancellationToken)
        {
            var form = submission.Form;

            // prepare data for storage.
            var fieldsInput = new StoreFieldsInput(submission);
            var fields = _storeFields.Execute(fieldsInput);
            var filesInput = new StoreFilesInput(submission);
            var files = _storeFiles.Execute(filesInput);

            // serialize stored data.
            var serializedValues = _jsonHelper.Serialize(fields);
            var serializedFiles = _jsonHelper.Serialize(files);

            // Store data to database.
            var dto = new FormulateSubmissionDto()
            {
                SubmissionId = submission.Id,
                FormId = form.Id,
                PageId = submission.PageId,
                DataValues = serializedValues,
                FileValues = serializedFiles,
            };

            using (var scope = _scopeProvider.CreateScope())
            {
                var result = await scope.Database.InsertAsync(dto);
                scope.Complete();
            }
        }
    }
}