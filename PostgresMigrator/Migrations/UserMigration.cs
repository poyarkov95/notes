using System;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(202404092100, "usertable")]
    public class UserMigration : Migration
    {
        public override void Up()
        {
            Create.Table(Constants.Tables.USER)
                .InSchema(Constants.Schemas.PORTAL)
                .WithColumn("user_id").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("login").AsString(255).Nullable()
                .WithColumn("salt").AsString(255)
                .WithColumn("password_hash").AsString(255)
                .WithColumn("access_token").AsString(255).Nullable()
                .WithColumn("refresh_token").AsString(255).Nullable()
                .WithColumn("registration_date").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime);
        }

        public override void Down()
        {
            Delete.Table(Constants.Tables.USER).InSchema(Constants.Schemas.PORTAL);
        }
    }
}