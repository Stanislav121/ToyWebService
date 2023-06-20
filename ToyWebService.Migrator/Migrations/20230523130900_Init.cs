using FluentMigrator;

namespace ToyWebService.Migrator.Migrations;

// For DDL operation use None
// For Data manipulation(DML) use Default(work in transaction). Like update phone numbers
[Migration(20230523130900, TransactionBehavior.None)]
public class Init : Migration
{
    public override void Up()
    {
        Create.Table("item")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("tag_ids").AsCustom("int[]");
    }

    public override void Down()
    {
        Delete.Table("item");
    }
}