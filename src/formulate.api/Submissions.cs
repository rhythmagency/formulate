namespace formulate.api
{

    //  Namespaces.
    using app.Persistence;
    using app.Resolvers;
    using core.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Umbraco.Core.Logging;


    /// <summary>
    /// Used for form submissions.
    /// </summary>
    public static class Submissions
    {

        #region Constants

        private const string HandlerError = "An error occurred while executing one of the Formulate form handlers.";

        #endregion


        #region Properties

        /// <summary>
        /// Form persistence.
        /// </summary>
        private static IFormPersistence Forms
        {
            get
            {
                return FormPersistence.Current.Manager;
            }
        }


        /// <summary>
        /// Validation persistence.
        /// </summary>
        private static IValidationPersistence Validations
        {
            get
            {
                return ValidationPersistence.Current.Manager;
            }
        }

        #endregion


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
        /// <param name="options">
        /// The options for this submission.
        /// </param>
        /// <returns>
        /// The result of the submission.
        /// </returns>
        public static SubmissionResult SubmitForm(Guid formId,
            IEnumerable<FieldSubmission> data, IEnumerable<FileFieldSubmission> files,
            SubmissionOptions options)
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


            // Validate?
            if (options.Validate)
            {
                var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
                {
                    Id = x.Key,
                    Values = x.SelectMany(y => y.FieldValues).ToList()
                }).ToDictionary(x => x.Id, x => x.Values);
                foreach (var field in form.Fields)
                {
                    var validations = field.Validations.Select(x => Validations.Retrieve(x)).ToList();
                    foreach (var validation in validations)
                    {
                        //TODO: var validationResult = validation.Validate(form, field, valuesById);
                    }
                }
            }


            // Initiate form handlers on a new thread (they may take some time to complete).
            var t = new Thread(() =>
            {
                try
                {
                    foreach (var handler in form.Handlers)
                    {
                        handler.HandleForm(form, data, files);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error<Submissions_Instance>(HandlerError, ex);
                }
            });
            t.IsBackground = true;
            t.Start();


            // Return success.
            return new SubmissionResult()
            {
                Success = true
            };

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