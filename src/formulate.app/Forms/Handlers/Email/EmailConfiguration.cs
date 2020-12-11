namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using formulate.core.Extensions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Configuration used by <see cref="EmailHandler"/>.
    /// </summary>
    public class EmailConfiguration : IEmailSenderRecipientConfiguration
    {
        /// <summary>
        /// Gets or sets the sender of the email.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the recipients of the email.
        /// </summary>
        [JsonProperty("recipients")]
        public IEnumerable<Recipient> RecipientsData { get; set; }

        /// <summary>
        /// Gets the recipients of the email.
        /// </summary>
        /// <remarks>
        /// This is a duplicate property because this version implements the interface,
        /// which requires a slightly different data type.
        /// </remarks>
        public IEnumerable<string> Recipients => RecipientsData.MakeSafe().Select(x => x.Email).ToArray();

        /// <summary>
        /// Gets or sets the fields containing the recipients of the email.
        /// </summary>
        [JsonProperty("recipientFields")]
        public IEnumerable<GuidId> RecipientFieldsData { get; set; }

        /// <summary>
        /// Gets the fields containing the recipients of the email.
        /// </summary>
        /// <remarks>
        /// This is a duplicate property because this version implements the interface,
        /// which requires a slightly different data type.
        /// </remarks>
        public IEnumerable<Guid> RecipientFields => RecipientFieldsData.MakeSafe().Select(x => x.Id).ToArray();

        /// <summary>
        /// Gets or sets the type of delivery for the recipients (e.g., to, cc, bcc).
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        /// Gets or sets the fields to include.
        /// </summary>
        [JsonProperty("fieldsToInclude")]
        public IEnumerable<GuidId> FieldsToIncludeData { get; set; }

        /// <summary>
        /// Gets the fields to include.
        /// </summary>
        /// <remarks>
        /// This is a convenience property that extracts the important data
        /// from the other version of this property.
        /// </remarks>
        public IEnumerable<Guid> FieldsToInclude => FieldsToIncludeData.MakeSafe().Select(x => x.Id).ToArray();

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to append fields.
        /// </summary>
        public bool AppendFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include hidden fields.
        /// </summary>
        public bool IncludeHiddenFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to exclude field labels.
        /// </summary>
        public bool ExcludeFieldLabels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to append payload.
        /// </summary>
        public bool AppendPayload { get; set; }
    }
}
