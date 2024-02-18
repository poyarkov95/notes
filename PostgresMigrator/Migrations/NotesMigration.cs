using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(202402181800, "notestable")]
    public class NotesMigration : Migration
    {
        public override void Up()
        {
            Create.Schema(Constants.Schemas.PORTAL);

            Create.Table(Constants.Tables.NOTE)
                .InSchema(Constants.Schemas.PORTAL)
                .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("user_id").AsGuid().NotNullable()
                .WithColumn("title").AsString(255).Nullable()
                .WithColumn("details").AsString().Nullable()
                .WithColumn("creation_date").AsDateTime().NotNullable()
                .WithColumn("edit_date").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(Constants.Tables.NOTE).InSchema(Constants.Schemas.PORTAL);
            Delete.Schema(Constants.Schemas.PORTAL);
        }
    }
}