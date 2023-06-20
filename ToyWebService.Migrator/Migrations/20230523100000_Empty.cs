namespace ToyWebService.Migrator.Migrations;
using FluentMigrator;

// You can use Empty migration as a simpliest way to make full rollback
[Migration(20230523100000, TransactionBehavior.None)]
public class Empty : Migration 
{
    public override void Up()
    {
        
    }

    public override void Down()
    {
        
    }
}