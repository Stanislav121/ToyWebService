using Npgsql;

namespace ToyWebService.Utils.DAL;

public class ConnectionStringBuilder
{
    public static string GetConnectionString(PostgresDbConfig config, string dbName, int maxPoolSize = 40)
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Password = config.Password,
            Username = config.Username,
            Host = config.Host,
            Port = config.Port,
            Database = dbName,
            // Connection string for migrations must be different with connection string for another application
            // This is to ensure that the connection with the connection string for migrations
            // is not taken from the connection pool for the main application.
            // Since this connection does not know and has no created custom database types in this migrations
            // Or you will need add connection.ReloadTypes() in any connection
            MaxPoolSize = maxPoolSize
        };
            
        Console.WriteLine(builder.ConnectionString);
        return builder.ConnectionString;
    }
}