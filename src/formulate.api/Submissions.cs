namespace formulate.api
{

    //  Namespaces.
    using app.Forms;
    using app.Persistence;
    using app.Validations;
    using core.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Umbraco.Core.Logging;


    /// <summary>
    /// Used for form submissions.
    /// </summary>
    public class Submissions
    {

        #region Constants

        private const string HandlerError = "An error occurred while executing one of the Formulate form handlers.";
        private const string PreHandlerError = "An error occurred while preparing one of the Formulate form handlers.";

        #endregion


        #region Delegates

        /// <summary>
        /// Delegate used when forms are submitting.
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        public delegate void FormSubmitEvent(FormSubmissionContext context);

        #endregion


        #region Events

        /// <summary>
        /// Event raised when forms are submitting.
        /// </summary>
        /// <remarks>
        /// Subscribing to this gives you the opportunity to alter form submissions. This is
        /// useful, for example, if you need to automatically alter some of the submitted data.
        /// </remarks>
        public static event FormSubmitEvent Submitting;

        #endregion


        #region Properties

        /// <summary>
        /// Form persistence.
        /// </summary>
        private IFormPersistence Forms { get; set; }


        /// <summary>
        /// Validation persistence.
        /// </summary>
        private IValidationPersistence Validations { get; set; }

        private ILogger Logger { get; set; }

        #endregion

        public Submissions(IFormPersistence formPersistence, IValidationPersistence validationPersistence, ILogger logger)
        {
            Forms = formPersistence;
            Validations = validationPersistence;
            Logger = logger;
        }


        #region Methods

        /// <summary>
        /// Submits a form.
        /// </summary>
        /// <param name="formId">
        /// The ID of the form to submit.
        /// </param>
        /// <param name="data">
        /// The form data to submit.
        /// </param>
        /// <param name="files">
        /// The file data to submit.
        /// </param>
        /// <param name="payload">
        /// Extra data related to the submission.
        /// </param>
        /// <param name="options">
        /// The options for this submission.
        /// </param>
        /// <param name="context">
        /// The contextual information for the form request.
        /// </param>
        /// <returns>
        /// The result of the submission.
        /// </returns>
        public SubmissionResult SubmitForm(Guid formId,
            IEnumerable<FieldSubmission> data, IEnumerable<FileFieldSubmission> files,
            IEnumerable<PayloadSubmission> payload, SubmissionOptions options,
            FormRequestContext context)
        {

            // Is the form ID valid?
            var form = Forms.Retrieve(formId);
            if (form == null)
            {
                return new SubmissionResult()
                {
                    Success = false
                };
            }


            // Create submission context.
            var submissionContext = new FormSubmissionContext()
            {
                Files = files,
                Data = data,
                Form = form,
                Payload = payload,
                CurrentPage = context.CurrentPage,
                HttpContext = context.HttpContext,
                Services = context.Services,
                UmbracoContext = context.UmbracoContext,
                UmbracoHelper = context.UmbracoHelper,
                SubmissionId = Guid.NewGuid(),
                ExtraContext = new Dictionary<string, object>(),
                SubmissionCancelled = false
            };


            // Invoke submitting event (gives listeners a chance to change the submission).
            Submitting?.Invoke(submissionContext);


            // Fail the form submission if SubmissionCancelled is true.
            if (submissionContext.SubmissionCancelled)
            {
                return new SubmissionResult()
                {
                    Success = false
                };
            }


            // Validate against native field validations.
            foreach (var field in form.Fields)
            {
                var fieldId = field.Id;
                var value = data.Where(x => x.FieldId == fieldId)
                    .SelectMany(x => x.FieldValues);
                if (!field.IsValid(value))
                {
                    return new SubmissionResult()
                    {
                        Success = false
                    };
                }
            }


            // Validate?
            if (options.Validate)
            {
                var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
                {
                    Id = x.Key,
                    Values = x.SelectMany(y => y.FieldValues).ToList()
                }).ToDictionary(x => x.Id, x => x.Values);
                var filesById = files.GroupBy(x => x.FieldId).Select(x => new
                {
                    Id = x.Key,
                    Values = x.Select(y => y).ToList()
                }).ToDictionary(x => x.Id, x => x.Values);
                foreach (var field in form.Fields)
                {
                    var validations = field.Validations
                        .Select(x => Validations.Retrieve(x))
                        .ToList();
                    if (!validations.Any())
                    {
                        continue;
                    }
                    var validationContext = new ValidationContext()
                    {
                        Field = field,
                        Form = form
                    };
                    foreach (var validation in validations)
                    {
                        var dataValues = valuesById.ContainsKey(field.Id)
                            ? valuesById[field.Id]
                            : new List<string>();
                        var fileValues = filesById.ContainsKey(field.Id)
                            ? filesById[field.Id]
                            : new List<FileFieldSubmission>();
                        var isValid = validation
                            .IsValueValid(dataValues, fileValues, validationContext);
                        if (!isValid)
                        {
                            return new SubmissionResult()
                            {
                                Success = false
                            };
                        }
                    }
                }
            }


            // Get a fresh instance of each handler (this is to avoid any cross-threading issues
            // with multiple threads sharing data).
            var enabledHandlers = form.Handlers
                .Where(x => x.Enabled)
                .Select(x => x.GetFreshCopy())
                .ToArray();


            // Prepare the form handlers.
            // This occurs on the current thread in case the handler needs information
            // only available in the current thread.
            try
            {
                foreach (var handler in enabledHandlers)
                {
                    handler.PrepareHandleForm(submissionContext);
                }
            }
            catch (Exception ex)
            {
                Logger.Error<Submissions_Instance>(ex, PreHandlerError);

                return new SubmissionResult()
                {
                    Success = false
                };
            }


            // Initiate form handlers on a new thread (they may take some time to complete).
            var task = new Task(() =>
            {
                foreach (var handler in enabledHandlers)
                {
                    handler.HandleForm(submissionContext);
                }
            });

            task.ContinueWith(FormHandlersExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            task.Start();

            // Return success.
            return new SubmissionResult()
            {
                Success = true
            };
        }

        private void FormHandlersExceptionHandler(Task task)
        {
            Logger.Error<Submissions_Instance>(task.Exception, HandlerError);
        }

        #endregion


        #region Submissions_Instance

        /// <summary>
        /// An instance version of the Submissions class (necessary for logging).
        /// </summary>
        private class Submissions_Instance { }

        #endregion

    }

}