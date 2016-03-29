namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using core.Types;
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;


    /// <summary>
    /// A handler that sends an email.
    /// </summary>
    public class EmailHandler : IFormHandlerType
    {

        #region Properties

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


        #region Methods

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
            var config = configuration as EmailConfiguration;
            using (var client = new SmtpClient())
            {
                var message = new MailMessage()
                {
                    IsBodyHtml = false
                };
                message.From = new MailAddress(config.SenderEmail);
                foreach (var recipient in config.Recipients)
                {
                    message.To.Add(recipient);
                }
                message.Subject = config.Subject;
                if (config.AppendFields)
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
                    message.Body = config.Message + Environment.NewLine + string.Join(Environment.NewLine, lines);
                }
                else
                {
                    message.Body = config.Message;
                }
                client.Send(message);
            }
        }

        #endregion

    }

}