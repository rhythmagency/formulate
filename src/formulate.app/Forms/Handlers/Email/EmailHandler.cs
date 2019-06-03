namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using core.Extensions;
    using core.Types;
    using Helpers;
    using Managers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using Umbraco.Core;


    /// <summary>
    /// A handler that sends an email.
    /// </summary>
    public class EmailHandler : IFormHandlerType
    {

        #region Public Static Properties

        /// <summary>
        /// The key to use when extracting the extra email recipients from the extra context on the
        /// form submission context. The value is expected to be a list of strings, with each string
        /// being an email address.
        /// </summary>
        public const string ExtraRecipientsKey = "Formulate Core: Email: Extra Recipients";


        /// <summary>
        /// The key to use when extracting the extra subject line text from the extra context on the
        /// form submission context. The value is expected to be a string.
        /// </summary>
        public const string ExtraSubjectKey = "Formulate Core: Email: Extra Subject";


        /// <summary>
        /// The key to use when extracting the extra message body text from the extra context on the
        /// form submission context. The value is expected to be a string.
        /// </summary>
        public const string ExtraMessageKey = "Formulate Core: Email: Extra Message";

        #endregion


        #region Private Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config { get; set; }

        #endregion


        public EmailHandler(IConfigurationManager configurationManager)
        {
            Config = configurationManager;
        }


        #region Public Properties

        /// <summary>
        /// The Angular directive that renders this handler.
        /// </summary>
        public string Directive => "formulate-email-handler";


        /// <summary>
        /// The icon shown in the picker dialog.
        /// </summary>
        public string Icon => "icon-formulate-email";


        /// <summary>
        /// The ID that uniquely identifies this handler (useful for serialization).
        /// </summary>
        public Guid TypeId => new Guid("A0C06033CB94424F9C035B10A420DB16");


        /// <summary>
        /// The label that appears when the user is choosing the handler.
        /// </summary>
        public string TypeLabel => "Email";

        #endregion


        #region Public Methods

        /// <summary>
        /// Deserializes the configuration for an email handler.
        /// </summary>
        /// <param name="configuration">
        /// The serialized configuration.
        /// </param>
        /// <returns>
        /// The deserialized configuration.
        /// </returns>
        public object DeserializeConfiguration(string configuration)
        {

            // Variables.
            var recipients = new List<string>();
            var recipientFields = new List<Guid>();
            var fieldsToInclude = new List<Guid>();
            var config = new EmailConfiguration()
            {
                Recipients = recipients,
                RecipientFields = recipientFields,
                FieldsToInclude = fieldsToInclude
            };
            var configData = JsonHelper.Deserialize<JObject>(configuration);
            var dynamicConfig = configData as dynamic;
            var properties = configData.Properties().Select(x => x.Name);
            var propertySet = new HashSet<string>(properties);


            // Get recipients.
            if (propertySet.Contains("recipients"))
            {
                foreach (var recipient in dynamicConfig.recipients)
                {
                    recipients.Add(recipient.email.Value as string);
                }
            }


            // Get email recipient fields.
            if (propertySet.Contains("recipientFields"))
            {
                foreach (var recipient in dynamicConfig.recipientFields)
                {
                    recipientFields.Add(GuidHelper.GetGuid(recipient.id.Value as string));
                }
            }


            // Get fields to include in message.
            if (propertySet.Contains("fieldsToInclude"))
            {
                foreach (var field in dynamicConfig.fieldsToInclude)
                {
                    fieldsToInclude.Add(GuidHelper.GetGuid(field.id.Value as string));
                }
            }


            // Get simple properties.
            if (propertySet.Contains("deliveryType"))
            {
                config.DeliveryType = dynamicConfig.deliveryType.Value as string;
            }
            if (propertySet.Contains("senderEmail"))
            {
                config.SenderEmail = dynamicConfig.senderEmail.Value as string;
            }
            if (propertySet.Contains("appendFields"))
            {
                config.AppendFields = (dynamicConfig.appendFields.Value as bool?).GetValueOrDefault();
            }
            if (propertySet.Contains("includeHiddenFields"))
            {
                config.IncludeHiddenFields = (dynamicConfig.includeHiddenFields.Value as bool?)
                    .GetValueOrDefault();
            }
            if (propertySet.Contains("excludeFieldLabels"))
            {
                config.ExcludeFieldLabels = (dynamicConfig.excludeFieldLabels.Value as bool?)
                    .GetValueOrDefault();
            }
            if (propertySet.Contains("appendPayload"))
            {
                config.AppendPayload = (dynamicConfig.appendPayload.Value as bool?).GetValueOrDefault();
            }
            if (propertySet.Contains("message"))
            {
                config.Message = dynamicConfig.message.Value as string;
            }
            if (propertySet.Contains("subject"))
            {
                config.Subject = dynamicConfig.subject.Value as string;
            }


            // Return the email configuration.
            return config;

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
        /// Handles a form submission (sends an email).
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
            var config = configuration as EmailConfiguration;
            var form = context.Form;
            var data = context.Data;
            var dataForMessage = data;
            var files = context.Files;
            var filesForMessage = files;
            var payload = context.Payload;
            var extraContext = context.ExtraContext;
            var extraEmails = (AttemptGetValue(extraContext, ExtraRecipientsKey) as List<string>).MakeSafe();
            var extraSubject = AttemptGetValue(extraContext, ExtraSubjectKey) as string ?? string.Empty;
            var extraMessage = AttemptGetValue(extraContext, ExtraMessageKey) as string ?? string.Empty;
            var baseMessage = config.Message + extraMessage;
            var plainTextBody = default(string);
            var htmlBody = default(string);

            // Create message.
            var message = new MailMessage();
            message.From = new MailAddress(config.SenderEmail);
            message.Subject = config.Subject + extraSubject;


            // Add headers to message.
            foreach (var header in Config.EmailHeaders)
            {
                message.Headers.Add(header.Name, header.Value);
            }


            // Get recipients from field values.
            var emailFieldIds = new HashSet<Guid>(config.RecipientFields);
            var fieldEmails = data
                .Where(x => emailFieldIds.Contains(x.FieldId)).SelectMany(x => x.FieldValues)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Where(x => IsEmailInValidFormat(x));


            // Include only specific fields in the email message?
            if (config.FieldsToInclude.Any())
            {

                // Variables.
                var fieldIdsToInclude = new HashSet<Guid>(config.FieldsToInclude);

                // Selected server side fields.
                var serverSideFields = form.Fields
                    .Where(x => x.IsServerSideOnly)
                    .Where(x => fieldIdsToInclude.Contains(x.Id))
                    .Select(x => new FieldSubmission()
                    {
                        FieldId = x.Id,
                        // The values don't matter here, as the IFormFieldType.FormatValue is how server
                        // side fields return a field value.
                        FieldValues = Enumerable.Empty<string>()
                    });

                // Combine submitted data and server side field data.
                var dataWithServerSideFields = data.Concat(serverSideFields);

                // Filter normal fields.
                dataForMessage = dataWithServerSideFields
                    .Where(x => fieldIdsToInclude.Contains(x.FieldId)).ToArray();

                // Filter file fields.
                filesForMessage = files
                    .Where(x => fieldIdsToInclude.Contains(x.FieldId)).ToArray();

            }


            // Any allowed recipients (if not, abort early)?
            var rawRecipients = config.Recipients
                .Concat(fieldEmails)
                .Concat(extraEmails);
            var allowedRecipients = FilterEmails(rawRecipients);
            if (!allowedRecipients.Any())
            {
                return;
            }
            foreach (var recipient in allowedRecipients)
            {
                if ("to".InvariantEquals(config.DeliveryType))
                {
                    message.To.Add(recipient);
                }
                else if ("cc".InvariantEquals(config.DeliveryType))
                {
                    message.CC.Add(recipient);
                }
                else
                {
                    message.Bcc.Add(recipient);
                }
            }


            // Append fields?
            if (config.AppendFields)
            {
                var chosenPayload = config.AppendPayload
                    ? payload
                    : payload.Take(0);
                htmlBody = ConstructMessage(form, dataForMessage, filesForMessage,
                    chosenPayload, baseMessage, config.IncludeHiddenFields, config.ExcludeFieldLabels, true);
                plainTextBody = ConstructMessage(form, dataForMessage, filesForMessage,
                    chosenPayload, baseMessage, config.IncludeHiddenFields, config.ExcludeFieldLabels, false);
                foreach (var file in filesForMessage)
                {
                    var dataStream = new MemoryStream(file.FileData);
                    message.Attachments.Add(new Attachment(dataStream, file.FileName));
                }
            }
            else
            {
                htmlBody = WebUtility.HtmlEncode(baseMessage);
                plainTextBody = baseMessage;
            }


            // Add plain text alternate view.
            var mimeType = new ContentType(MediaTypeNames.Text.Plain);
            var emailView = AlternateView.CreateAlternateViewFromString(plainTextBody, mimeType);
            message.AlternateViews.Add(emailView);


            // Add HTML alternate view.
            mimeType = new ContentType(MediaTypeNames.Text.Html);
            emailView = AlternateView.CreateAlternateViewFromString(htmlBody, mimeType);
            message.AlternateViews.Add(emailView);


            // Send email.
            using (var client = new SmtpClient())
            {
                client.Send(message);
            }

        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Constructs an email message from the form fields.
        /// </summary>
        /// <param name="form">
        /// The form being submitted.
        /// </param>
        /// <param name="data">
        /// The form fields.
        /// </param>
        /// <param name="files">
        /// The form files.
        /// </param>
        /// <param name="payload">
        /// Extra data related to the submission.
        /// </param>
        /// <param name="baseMessage">
        /// The base message to use (before any fields have been appended).
        /// </param>
        /// <param name="includeHiddenFields">
        /// Include hidden fields in the message?
        /// </param>
        /// <param name="excludeFieldLabels">
        /// Exclude the field labels when constructing the message?
        /// </param>
        /// <param name="isHtml">
        /// Construct the message as HTML?
        /// </param>
        /// <returns>
        /// The email message.
        /// </returns>
        private string ConstructMessage(Form form, IEnumerable<FieldSubmission> data,
            IEnumerable<FileFieldSubmission> files, IEnumerable<PayloadSubmission> payload,
            string baseMessage, bool includeHiddenFields, bool excludeFieldLabels, bool isHtml = false)
        {

            // Variables.
            var nl = isHtml
                ? Environment.NewLine + "<br>" + Environment.NewLine
                : Environment.NewLine;
            var lines = new List<string>();
            var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Values = x.SelectMany(y => y.FieldValues).ToList()
            }).ToDictionary(x => x.Id, x => x.Values);
            var filesById = files.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Filename = x.Select(y => y.FileName).FirstOrDefault()
            }).ToDictionary(x => x.Id, x => x.Filename);
            var fieldsById = form.Fields.ToDictionary(x => x.Id, x => x);
            var fieldIds = form.Fields.Select(x => x.Id);


            // Payload items.
            foreach (var item in payload)
            {
                lines.Add($"{item.Name}: {item.Value}");
            }


            // Normal fields.
            foreach (var key in valuesById.Keys.OrderByCollection(fieldIds))
            {
                var values = valuesById[key];
                var formatted = string.Join(", ", values);
                var field = default(IFormField);
                var fieldName = "Unknown Field";
                if (fieldsById.TryGetValue(key, out field))
                {

                    // Skip this value?
                    if (!includeHiddenFields && field.IsHidden)
                    {
                        continue;
                    }


                    // Get field name and formatted value.
                    fieldName = field.Name;
                    formatted = field.FormatValue(values, FieldPresentationFormats.Email);

                }
                var line = excludeFieldLabels
                    ? formatted
                    : $"{fieldName}: {formatted}";
                lines.Add(line);
            }


            // File fields.
            foreach (var key in filesById.Keys.OrderByCollection(fieldIds))
            {
                var filename = filesById[key];
                var field = default(IFormField);
                var fieldName = "Unknown Field";
                if (fieldsById.TryGetValue(key, out field))
                {

                    // Skip this file?
                    if (!includeHiddenFields && field.IsHidden)
                    {
                        continue;
                    }


                    // Get the field name.
                    fieldName = field.Name;

                }
                var line = excludeFieldLabels
                    ? $@"See attachment, ""{filename}"""
                    : $@"{fieldName}: See attachment, ""{filename}""";
                lines.Add(line);
            }

            // HTML encode?
            if (isHtml)
            {
                baseMessage = WebUtility.HtmlEncode(baseMessage);
                lines = lines.Select(x => WebUtility.HtmlEncode(x)).ToList();
            }


            // Return message.
            return baseMessage + nl + string.Join(nl, lines);

        }


        /// <summary>
        /// Filters the email addresses to only return those allowed by the whitelist.
        /// </summary>
        /// <param name="emails">
        /// The email addresses to filter.
        /// </param>
        /// <returns>
        /// The allowed email addresses.
        /// </returns>
        private IEnumerable<string> FilterEmails(IEnumerable<string> emails)
        {
            var shouldFilter = Config.EnableEmailWhitelist;
            if (shouldFilter)
            {
                var whitelist = Config.EmailWhitelist;
                var emailWhitelist = whitelist
                    .Where(x => !string.IsNullOrWhiteSpace(x.Email))
                    .Select(x => x.Email.ToLower()).Distinct();
                var emailSet = new HashSet<string>(emailWhitelist);
                var domainWhitelist = whitelist
                    .Where(x => !string.IsNullOrWhiteSpace(x.Domain))
                    .Select(x => x.Domain.ToLower()).Distinct();
                var domainSet = new HashSet<string>(domainWhitelist);
                var emailInfo = emails.Select(x => new
                {
                    Original = x,
                    Email = x.ToLower(),
                    Domain = new MailAddress(x).Host.ToLower()
                });
                return emailInfo
                    .Where(x => emailSet.Contains(x.Email) || domainSet.Contains(x.Domain))
                    .Select(x => x.Original);
            }
            else
            {
                return emails;
            }
        }


        /// <summary>
        /// Indicates whether or not the specified email address is in a valid format.
        /// </summary>
        /// <param name="email">
        /// The email address.
        /// </param>
        /// <returns>
        /// True, if the email address is in a valid format; otherwise, false.
        /// </returns>
        /// <remarks>
        /// This code is based on this Stack Overflow answer: http://stackoverflow.com/a/1374644/2052963
        /// </remarks>
        private bool IsEmailInValidFormat(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Attempts to get a value from a dictionary.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary to get the value from.
        /// </param>
        /// <param name="key">
        /// The key to use when getting the value.
        /// </param>
        /// <returns>
        /// The value, or null.
        /// </returns>
        private object AttemptGetValue(Dictionary<string, object> dictionary, string key)
        {
            var value = default(object);
            return dictionary.TryGetValue(key, out value)
                ? value
                : null;
        }

        #endregion

    }

}