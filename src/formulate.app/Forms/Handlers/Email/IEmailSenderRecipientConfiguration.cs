namespace formulate.app.Forms.Handlers.Email
{

    // Namespaces.
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The portion of the email configuration used for the email sender and the email
    /// recipients.
    /// </summary>
    public interface IEmailSenderRecipientConfiguration
    {

        #region Properties

        /// <summary>
        /// The sender of the email.
        /// </summary>
        string SenderEmail { get; }

        /// <summary>
        /// The recipients of the email.
        /// </summary>
        IEnumerable<string> Recipients { get; }

        /// <summary>
        /// The fields containing the recipients of the email.
        /// </summary>
        IEnumerable<Guid> RecipientFields { get; }

        /// <summary>
        /// The type of delivery for the recipients (e.g., to, cc, bcc).
        /// </summary>
        string DeliveryType { get; }

        #endregion

    }

}