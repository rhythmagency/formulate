namespace formulate.app.Controllers.ControllerTypes
{
    using Persistence.Internal.Sql.Models;
    using System;
    using System.Collections.Generic;
    internal class SubmissionExport
    {
        public FormulateSubmission Submission { get; set; }
        public Dictionary<Guid, string> Values { get; set; }
    }
}