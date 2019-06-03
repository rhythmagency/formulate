namespace formulate.app.Migrations
{
    using System.Linq;

    using formulate.app.Persistence.Internal.Sql.Models;

    using Umbraco.Core;
    using Umbraco.Core.Migrations;

    public sealed class CreateSubmissionsTable : MigrationBase
    {
        public CreateSubmissionsTable(IMigrationContext context)
            : base(context)
        {
        }

        public override void Migrate()
        {
            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();
            if (tables.InvariantContains(nameof(FormulateSubmission)) == false)
            {
                Create.Table<FormulateSubmission>().Do();
            }
        }
    }
}
