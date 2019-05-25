using Cmt.FluentMigrations.Infrastructure;
using FluentMigrator;

namespace Cmt.Migrations.Migrations.Cmt
{
    [Tags(Db.Cmt)]
    [Migration(20181205120100)]
    public class InitCourse : Migration
    {
        public override void Up()
        {
            Create.Table("Course")
                .AsBaseEntity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Course");
        }
    }
}
