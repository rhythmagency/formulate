namespace Formulate.Core.Utilities.Submissions
{
    using Formulate.Core.FormHandlers;
    using Formulate.Core.Notifications;
    using Formulate.Core.Submissions.Requests;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Umbraco.Cms.Core.Scoping;

    internal sealed class SubmitFormSubmissionRequest : ISubmitFormSubmissionRequest
    {
        /// <summary>
        /// The core scope provider.
        /// </summary>
        private readonly ICoreScopeProvider _coreScopeProvider;

        /// <summary>
        /// The form handler factory.
        /// </summary>
        private readonly IFormHandlerFactory _formHandlerFactory;

        public SubmitFormSubmissionRequest(ICoreScopeProvider coreScopeProvider, IFormHandlerFactory formHandlerFactory)
        {
            _coreScopeProvider = coreScopeProvider;
            _formHandlerFactory = formHandlerFactory;
        }

        public async Task<bool> SubmitAsync(FormSubmissionRequest input, CancellationToken cancellationToken)
        {
            var modifiedInput = NotifySubmission(input);

            return await PerformSubmitAsync(modifiedInput, cancellationToken);
        }

        private async Task<bool> PerformSubmitAsync(FormSubmissionRequest input, CancellationToken cancellationToken)
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

        /// <summary>
        /// Notifies custom notification handlers that form is being submitted.
        /// </summary>
        /// <param name="input">The request.</param>
        /// <returns>A potentially modified <see cref="FormSubmissionRequest"/>.</returns>
        private FormSubmissionRequest NotifySubmission(FormSubmissionRequest input)
        {
            using (var scope = _coreScopeProvider.CreateCoreScope())
            {
                var notification = new FormSubmittingNotification(input);
                scope.Notifications.Publish(notification);
                scope.Complete();

                return notification.Request;
            }
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
