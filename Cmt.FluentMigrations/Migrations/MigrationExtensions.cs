using FluentMigrator.Builders.Create.Table;

namespace Cmt.Migrations.Migrations
{
    public static class MigrationExtensions
    {
        public static ICreateTableWithColumnSyntax AsBaseEntity(
            this ICreateTableWithColumnSyntax syntax)
        {
            syntax
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime().NotNullable()
                .WithColumn("UpdatedBy").AsInt32().NotNullable();

            return syntax;
        }
    }
}
