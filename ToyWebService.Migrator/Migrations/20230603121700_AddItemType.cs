using FluentMigrator;

namespace ToyWebService.Migrator.Migrations;

[Migration(20230603121700)]
public class AddItemType : Migration
{
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        if not exists(select 1 from pg_type where typname = 'item_v1') then
            create type item_v1 as
                (
                    id bigint,
                    name text,
                    tag_ids int[]
                );
        end if;
    end
$$;
";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE if exists item_v1
    end;
$$;
";
        
        Execute.Sql(sql);
    }
}