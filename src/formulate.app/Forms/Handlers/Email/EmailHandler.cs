namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using Helpers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;


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


        //TODO: Choose a better icon.
        /// <summary>
        /// The icon shown in the picker dialog.
        /// </summary>
        public string Icon => "icon-formulate-drop-down";


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

        #endregion

    }

}