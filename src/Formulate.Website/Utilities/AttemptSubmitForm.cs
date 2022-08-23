namespace Formulate.Website.Utilities
{
    using Formulate.Core.Forms;
    using Formulate.Core.Submissions.Requests;
    using Formulate.Core.Submissions.Responses;
    using Formulate.Core.Utilities.Submissions;
    using System.Threading.Tasks;
    using Umbraco.Extensions;

    internal sealed class AttemptSubmitForm : IAttemptSubmitForm
    {
        private readonly IFormEntityRepository _formEntityRepository;

        private readonly IValidateFormSubmissionRequest _validateFormSubmissionRequest;

        private readonly ISubmitFormSubmissionRequest _submitFormSubmissionRequest;

        public AttemptSubmitForm(IFormEntityRepository formEntityRepository, IValidateFormSubmissionRequest validateFormSubmissionRequest, ISubmitFormSubmissionRequest submitFormSubmissionRequest)
        {
            _formEntityRepository = formEntityRepository;
            _validateFormSubmissionRequest = validateFormSubmissionRequest;
            _submitFormSubmissionRequest = submitFormSubmissionRequest;
        }

        public async Task<AttemptSubmitFormOutput> SubmitAsync(AttemptSubmitFormInput input, CancellationToken cancellationToken)
        {
            var form = _formEntityRepository.Get(input.FormId);

            if (form is null)
            {
                return new AttemptSubmitFormOutput()
                {
                    IsSuccessful = false
                };
            }

            // Get form values.
            var values = new Dictionary<Guid, IFormFieldValues>();

            foreach (var key in input.Form.Keys)
            {
                if (Guid.TryParse(key, out var fieldId) == false)
                {
                    continue;
                }

                var value = input.Form[key];

                values.Add(fieldId, new StringFormFieldValues(value));
            }

            // Get file values.

            foreach (var groupedFiles in input.Files.GroupBy(x => x.Name))
            {
                if (Guid.TryParse(groupedFiles.Key, out var fieldId) == false)
                {
                    continue;
                }

                var files = new List<FormFileValue>();

                foreach (var file in groupedFiles)
                {
                    using var stream = file.OpenReadStream();
                    using var reader = new BinaryReader(stream);
                    var fileData = reader.ReadBytes((int)stream.Length);
                    var filename = file.FileName;

                    files.Add(new FormFileValue
                    {
                        Data = fileData,
                        Name = filename,
                        ContentType = file.ContentType
                    });
                }

                values.Add(fieldId, new FilesFormFieldValues(files));
            }

            var payload = new FormSubmissionRequest()
            {
                Form = form,
                PageId = input.PageId,
                FieldValues = values,
            };

            var validationResult = _validateFormSubmissionRequest.Validate(payload);

            if (validationResult.Errors.Any())
            {
                return new AttemptSubmitFormOutput()
                {
                    IsSuccessful = false,
                    Errors = validationResult.Errors.Select(x => new ValidationErrorSubmissionResponse(x.FieldName, x.FieldId, x.Messages)).ToArray()
                };
            }

            // submit payload.

            var submitResult = await _submitFormSubmissionRequest.SubmitAsync(payload, cancellationToken);

            return new AttemptSubmitFormOutput()
            {
                IsSuccessful = submitResult
            };
        }
    }
}
