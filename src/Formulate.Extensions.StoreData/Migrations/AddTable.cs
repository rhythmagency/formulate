namespace Formulate.Extensions.StoreData.Migrations
{
    using Formulate.Extensions.StoreData.Models;
    using Microsoft.Extensions.Logging;
    using NPoco;
    using Umbraco.Cms.Infrastructure.Migrations;
    using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
    using Umbraco.Cms.Infrastructure.Persistence.DatabaseModelDefinitions;

    public sealed class AddTable : MigrationBase
    {
        public AddTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", nameof(AddTable));

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists(FormulateSubmissionDto.TableName) == false)
            {
                Create.Table<TableSchema>()
                      .Do();
            }
            else
            {
                Logger.LogDebug("The database table {DbTable} already exists, skipping", FormulateSubmissionDto.TableName);
            }
        }

        [ExplicitColumns]
        [PrimaryKey(PrimaryKeyName, AutoIncrement = false)]
        [TableName(FormulateSubmissionDto.TableName)]
        private sealed class TableSchema
        {
            private const string PrimaryKeyName = nameof(SubmissionId);

            [Column(Name = PrimaryKeyName)]
            [Constraint(Default = SystemMethods.NewGuid)]
            [Index(IndexTypes.UniqueNonClustered)]
            [PrimaryKeyColumn(AutoIncrement = false)]
            public Guid? SubmissionId { get; set; }

            [Column(Name = nameof(CreationDate))]
            [Constraint(Default = SystemMethods.CurrentDateTime)]
            [Index(IndexTypes.NonClustered)]
            public DateTime CreationDate { get; set; }

            [Column(Name = nameof(DataValues))]
            [SpecialDbType(SpecialDbTypes.NTEXT)]
            [NullSetting(NullSetting = NullSettings.Null)]
            public string? DataValues { get; set; }

            [Column(Name = nameof(FileValues))]
            [SpecialDbType(SpecialDbTypes.NTEXT)]
            [NullSetting(NullSetting = NullSettings.Null)]
            public string? FileValues { get; set; }

            [Column(Name = nameof(FormId))]
            public Guid FormId { get; set; }

            [Column(Name = nameof(PageId))]
            [NullSetting(NullSetting = NullSettings.Null)]
            public int? PageId { get; set; }
        }
    }
}
