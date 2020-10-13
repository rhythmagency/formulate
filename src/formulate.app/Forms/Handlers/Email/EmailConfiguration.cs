namespace formulate.app.Forms.Handlers.Email
{
    using System;
    using System.Collections.Generic;

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
        public IEnumerable<string> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the fields containing the recipients of the email.
        /// </summary>
        public IEnumerable<Guid> RecipientFields { get; set; }

        /// <summary>
        /// Gets or sets the type of delivery for the recipients (e.g., to, cc, bcc).
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        /// Gets or sets the fields to include.
        /// </summary>
        public IEnumerable<Guid> FieldsToInclude { get; set; }

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
