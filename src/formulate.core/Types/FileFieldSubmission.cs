namespace formulate.core.Types
{
    using System;
    public class FileFieldSubmission
    {
        public Guid FieldId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public FileFieldSubmission()
        {
            FileData = new byte[0];
        }
    }
}