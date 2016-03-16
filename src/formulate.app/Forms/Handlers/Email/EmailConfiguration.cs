namespace formulate.app.Forms.Handlers.Email
{
    using System.Collections.Generic;
    public class EmailConfiguration
    {
        public string SenderEmail { get; set; }
        public IEnumerable<string> Recipients { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public bool AppendFields { get; set; }
    }
}