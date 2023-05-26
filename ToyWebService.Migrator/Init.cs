using Npgsql;

namespace ToyWebService.Migrator;

public static class Init
{
    public static void CreateDataBase(string connectionString, string dbname)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            var sqlCheckDb = "SELECT DATNAME FROM pg_catalog.pg_database WHERE DATNAME = ($1)";
            using (NpgsqlCommand checkCommand = new NpgsqlCommand(sqlCheckDb, conn)
                   {
                       Parameters =
                       {
                           new (){Value = dbname}
                       }
                   })
            {
                try
                {
                    conn.Open();
                    var i = checkCommand.ExecuteScalar();
                    //always 'true' (if it exists) or 'null' (if it doesn't)
                    if (i == null) 
                    {
                        var sqlCreate = "CREATE DATABASE \"toy-web-service\"";
                        using (NpgsqlCommand createCommand = new NpgsqlCommand(sqlCreate, conn))
                        {
                            createCommand.ExecuteScalar();
                        }
                    }
                    conn.Close();
                }
                catch (Exception e) {
                    Console.WriteLine("Can't create data base toy-web-service. You need to create the database yourself");
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}