namespace formulate.core.Types
{
    using System;
    using System.Collections.Generic;
    public class FieldSubmission
    {
        public Guid FieldId { get; set; }
        public IEnumerable<string> FieldValues { get; set; }
        public FieldSubmission()
        {
            FieldValues = new List<string>();
        }
    }
}