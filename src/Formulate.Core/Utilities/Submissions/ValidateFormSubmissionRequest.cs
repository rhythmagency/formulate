namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.FormFields;
    using Formulate.Core.Submissions.Requests;
    using Formulate.Core.Submissions.Responses;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ValidateFormSubmissionRequest : IValidateFormSubmissionRequest
    {
        private readonly IFormFieldFactory _formFieldFactory;

        public ValidateFormSubmissionRequest(IFormFieldFactory formFieldFactory)
        {
            _formFieldFactory = formFieldFactory;
        }

        public ValidateFormSubmissionRequestOutput Validate(FormSubmissionRequest input)
        {
            var form = input.Form;
            var fieldValues = input.FieldValues;
            var errors = new List<ValidationErrorSubmissionResponse>();

            foreach (var field in form.Fields)
            {
                var formField = _formFieldFactory.Create(field);

                if (formField is null)
                {
                    continue;
                }

                var values = fieldValues.ContainsKey(field.Id) ? fieldValues[field.Id] : FormFieldValues.Empty;
                var result = formField.Validate(values);

                if (result.ErrorMessages.Any())
                {
                    errors.Add(new ValidationErrorSubmissionResponse(field.Name, field.Id, result.ErrorMessages));
                }
            }

            return new ValidateFormSubmissionRequestOutput()
            {
                Errors = errors
            };
        }
    }
}
