using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ToyWebService.Utils.DAL;
using Microsoft.Extensions.Configuration;

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
                                  throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT is not set");


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
            var dbConfig = config.GetSection("Databases:Product").Get<PostgresDbConfig>();
            var defaultSchemaConnectionString = ConnectionStringBuilder.GetConnectionString(
                dbConfig, "postgres", 39);
            Init.CreateDataBase(defaultSchemaConnectionString, dbConfig.Database);

            var connectionString = ConnectionStringBuilder.GetConnectionString(
                dbConfig, dbConfig.Database, 39);

            using (var serviceProvider = CreateMigrator(connectionString))
            using (var scope = serviceProvider.CreateScope())
            {
                // Put the database update into a scope to ensure
                // that all resources will be disposed.
                ApplyMigrations(scope.ServiceProvider);
            }
        }

        private static ServiceProvider CreateMigrator(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .WithGlobalCommandTimeout(TimeSpan.FromSeconds(30))
                    .ScanIn(typeof(Migrations.Init).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void ApplyMigrations(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}