namespace Formulate.Extensions.StoreData.Models
{
    using NPoco;
    using System;
    using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

    [TableName(TableName)]
    internal sealed class FormulateSubmissionDto
    {
        public const string TableName = "FormulateSubmissions";

        [Column(Name = nameof(SubmissionId))]
        public Guid? SubmissionId { get; set; }

        [Column(Name = nameof(CreationDate))]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Column(Name = nameof(DataValues))]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string? DataValues { get; set; }

        [Column(Name = nameof(FileValues))]
        public string? FileValues { get; set; }

        [Column(Name = nameof(FormId))]
        public Guid FormId { get; set; }

        [Column(Name = nameof(PageId))]
        public int? PageId { get; set; }
    }
}
