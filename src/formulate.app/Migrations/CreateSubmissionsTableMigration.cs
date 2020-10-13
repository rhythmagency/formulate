namespace formulate.app.Migrations
{
    using System.Linq;

    using formulate.app.Persistence.Internal.Sql.Models;

    using Umbraco.Core;
    using Umbraco.Core.Migrations;

    /// <summary>
    /// A database migration for creating the Formulate Submissions table.
    /// </summary>
    internal sealed class CreateSubmissionsTableMigration : MigrationBase
    {
        /// <inheritdoc />
        public CreateSubmissionsTableMigration(IMigrationContext context)
            : base(context)
        {
        }

        /// <inheritdoc />
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
