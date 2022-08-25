namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Submissions.Requests;
    using System;
    using System.Collections.Generic;
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
            var formHandlers = CreateFormHandlers(form.Handlers);

            foreach (var item in formHandlers)
            {
                if (item is FormHandler formHandler)
                {
                    formHandler.Handle(input);
                }
                
                if (item is AsyncFormHandler asyncFormHandler)
                {
                    await asyncFormHandler.HandleAsync(input, cancellationToken);
                }
            }

            return true;
        }

        private IReadOnlyCollection<IFormHandler> CreateFormHandlers(PersistedFormHandler[] items)
        {
            if (items == null)
            {
                return Array.Empty<IFormHandler>();
            }

            var enabledItems = items.Where(x => x.Enabled).ToArray();
            var createdFormHandlers = new List<IFormHandler>();

            foreach(var item in enabledItems)
            {
                var createdFormHandler = _formHandlerFactory.Create(item);

                if (createdFormHandler is not null)
                {
                    createdFormHandlers.Add(createdFormHandler);
                }
            }

            return createdFormHandlers.ToArray();
        }
    }
}
