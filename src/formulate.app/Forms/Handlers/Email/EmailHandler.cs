namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using core.Types;
    using Helpers;
    using Managers;
    using Newtonsoft.Json.Linq;
    using Resolvers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;


    /// <summary>
    /// A handler that sends an email.
    /// </summary>
    public class EmailHandler : IFormHandlerType
    {

        #region Private Properties

        /// <summary>
        /// Configuration manager.
        /// </summary>
        private IConfigurationManager Config
        {
            get
            {
                return Configuration.Current.Manager;
            }
        }

        #endregion


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
            var config = new EmailConfiguration()
            {
                Recipients = recipients
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

            // Get simple properties.
            if (propertySet.Contains("senderEmail"))
            {
                config.SenderEmail = dynamicConfig.senderEmail.Value;
            }
            if (propertySet.Contains("appendFields"))
            {
                config.AppendFields = dynamicConfig.appendFields.Value;
            }
            if (propertySet.Contains("message"))
            {
                config.Message = dynamicConfig.message.Value;
            }
            if (propertySet.Contains("subject"))
            {
                config.Subject = dynamicConfig.subject.Value;
            }


            // Return the email configuration.
            return config;

        }


        /// <summary>
        /// Handles a form submission (sends an email).
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="data">The form data.</param>
        /// <param name="configuration">The handler configuration.</param>
        public void HandleForm(Form form, IEnumerable<FieldSubmission> data, object configuration)
        {

            // Create message.
            var config = configuration as EmailConfiguration;
            var message = new MailMessage()
            {
                IsBodyHtml = false
            };
            message.From = new MailAddress(config.SenderEmail);
            message.Subject = config.Subject;


            // Any allowed recipients (if not, abort early)?
            var allowedRecipients = FilterEmails(config.Recipients);
            if (!allowedRecipients.Any())
            {
                return;
            }
            foreach (var recipient in allowedRecipients)
            {
                message.To.Add(recipient);
            }


            // Append fields?
            if (config.AppendFields)
            {
                message.Body = ConstructMessage(form, data, config);
            }
            else
            {
                message.Body = config.Message;
            }


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
        /// <param name="config">
        /// The email configuration.
        /// </param>
        /// <returns>
        /// The email message.
        /// </returns>
        private string ConstructMessage(Form form, IEnumerable<FieldSubmission> data,
            EmailConfiguration config)
        {
            var lines = new List<string>();
            var valuesById = data.GroupBy(x => x.FieldId).Select(x => new
            {
                Id = x.Key,
                Values = x.SelectMany(y => y.FieldValues).ToList()
            }).ToDictionary(x => x.Id, x => x.Values);
            var fieldsById = form.Fields.ToDictionary(x => x.Id, x => x);
            foreach (var key in valuesById.Keys)
            {
                var values = valuesById[key];
                var combined = string.Join(", ", values);
                var field = default(IFormField);
                var fieldName = "Unknown Field";
                if (fieldsById.TryGetValue(key, out field))
                {
                    fieldName = field.Name;
                }
                var line = string.Format("{0}: {1}", fieldName, combined);
                lines.Add(line);
            }
            var nl = Environment.NewLine;
            return config.Message + nl + string.Join(nl, lines);
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

        #endregion

    }

}