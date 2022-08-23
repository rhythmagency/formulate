namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Submissions.Requests;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class SubmitFormSubmissionRequest : ISubmitFormSubmissionRequest
    {
        private readonly IFormHandlerFactory _formHandlerFactory;
        
        public SubmitFormSubmissionRequest(IFormHandlerFactory formHandlerFactory)
        {
            _formHandlerFactory = formHandlerFactory;
        }


        public async Task<bool> SubmitAsync(FormSubmissionRequest input, CancellationToken cancellationToken)
        {
            var form = input.Form;
            var enabledHandlers = form.Handlers.Where(x => x.Enabled).ToArray();

            foreach (var handler in enabledHandlers)
            {
                var formHandler = _formHandlerFactory.Create(handler);

                if (formHandler is null)
                {
                    continue;
                }

                if (formHandler is FormHandler typedFormHandler)
                {
                    await typedFormHandler.Handle(input, cancellationToken);
                }
            }

            return true;
        }
    }
}
