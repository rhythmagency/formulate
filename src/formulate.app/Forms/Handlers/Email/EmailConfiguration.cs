namespace formulate.app.Forms.Handlers.Email
{
    using System;
    using System.Collections.Generic;
    public class EmailConfiguration
    {
        public string SenderEmail { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public IEnumerable<Guid> RecipientFields { get; set; }
        public IEnumerable<Guid> FieldsToInclude { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public bool AppendFields { get; set; }
        public bool IncludeHiddenFields { get; set; }
        public bool ExcludeFieldLabels { get; set; }
        public bool AppendPayload { get; set; }
    }
}