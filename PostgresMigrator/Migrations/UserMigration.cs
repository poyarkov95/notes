using System;
using FluentMigrator;

namespace PostgresMigrator.Migrations
{
    [Migration(202400482100, "usertable")]
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
                .WithColumn("token").AsString(255).Nullable()
                .WithColumn("registration_date").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("token_expire_date").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(Constants.Tables.USER).InSchema(Constants.Schemas.PORTAL);
        }
    }
}