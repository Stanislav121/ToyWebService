using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ToyWebService.Utils.DAL;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ToyWebService.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Need it to run Migrator from terminal with linked appSettings
            // https://stackoverflow.com/questions/65307789/add-a-linked-appsettings-json
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                                  throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT in not set");

            
            // To use ConfigurationBuilder you need install Microsoft.Extensions.Configuration.Json
            // in this project
            // https://stackoverflow.com/questions/46134718/cant-read-appsettings-json-without-di-due-to-missing-configuration-builder
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environmentName}.json")
                // To use AddEnvironmentVariables you need install Microsoft.Extensions.Configuration.EnvironmentVariables
                .AddEnvironmentVariables()
                .Build();
            
            // To use Get install Microsoft.Extensions.Configuration.Binder
            var dbConfiguration = config.GetSection("Databases:Product").Get<PostgresDbConfig>();
            dbConfiguration.Validate();

            
            var connectionPostgresString = GetConnectionString(dbConfiguration, "postgres");
            Init.CreateDataBase(connectionPostgresString, dbConfiguration.Database);
            
            var connectionString = GetConnectionString(dbConfiguration, dbConfiguration.Database);

            using (var serviceProvider = CreateServices(connectionString))
            using (var scope = serviceProvider.CreateScope())
            {
                // Put the database update into a scope to ensure
                // that all resources will be disposed.
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        private static string GetConnectionString(PostgresDbConfig dbConfiguration, string dbName)
        {
            var connectionBuilder = new NpgsqlConnectionStringBuilder(dbConfiguration.PartialConnectionString)
            {
                Password = dbConfiguration.Password,
                Username = dbConfiguration.Username,
                Host = dbConfiguration.Host,
                Port = dbConfiguration.Port,
                Database = dbName
            };

            Console.WriteLine(connectionBuilder.ConnectionString);

            return connectionBuilder.ConnectionString;
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static ServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Migrations.Init).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}