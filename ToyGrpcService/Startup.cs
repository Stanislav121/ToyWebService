using ToyGrpcService.Services;
using ToyWebService.Application.BLL.Repositories;
using ToyWebService.Application.BLL.Service;
using ToyWebService.Application.BLL.Service.Interfaces;
using ToyWebService.Application.DAL;
using ToyWebService.Application.DAL.Utils;
using ToyWebService.Utils.DAL;

namespace ToyGrpcService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //AppContext.SetSwitch();
        
        SetupDI(services);
        SetupDatabase(services);

        services.AddGrpc();
    }

    private void SetupDI(IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();
    }

    private void SetupDatabase(IServiceCollection services)
    {
        // Прежде всего нужно для маппинга UNNEST
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        var dbConfig = _configuration.GetSection("Databases:Product").Get<PostgresDbConfig>();
        var connectionString = ConnectionStringBuilder.GetConnectionString(
            dbConfig, dbConfig.Database);
        var db = new DataBase(connectionString, 30);
        //TODO так норм делать?
        services.AddSingleton<IDataBase>(db);


        // TODO
        //services.Configure<PostgresDbConfig>(_configuration.GetSection("Databases:Product"));
        //var config = GetR
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<GreeterService>();
            endpoints.MapGrpcService<ItemsGrpcService>();
            
            //endpoints.MapGet("/",
            //    () =>
            //        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });
    }
}

// TODO осталось этот класс переписать
/*public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseEndpoints(e =>
        {

            e.MapGet("/",
                () =>
                    "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });
    }

}*/