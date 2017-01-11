namespace formulate.app.Forms.Handlers.SendData
{

    // Namespaces.
    using System;


    /// <summary>
    /// A field mapping allows a custom name to be assigned to a field.
    /// </summary>
    /// <remarks>
    /// This is useful, for example, when translating Formulate fields for transmission
    /// to external systems that expect different field names than those used within
    /// Formulate.
    /// </remarks>
    public class FieldMapping
    {

        #region Properties

        /// <summary>
        /// Formulates field ID.
        /// </summary>
        public Guid FieldId { get; set; }


        /// <summary>
        /// The name to use for the field (typically, the name expected by an external system).
        /// </summary>
        public string FieldName { get; set; }

        #endregion

    }

}