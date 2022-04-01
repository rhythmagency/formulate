namespace Formulate.BackOffice.Controllers.Forms
{
    // Namespaces.
    using System;

    /// <summary>
    /// The data for a request to generate a new path and ID.
    /// </summary>
    public class GenerateNewPathAndIdRequest
    {
        /// <summary>
        /// Optional. The ID of the parent entity.
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}