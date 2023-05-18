using ToyGrpcService.Services;
using ToyWebService.Application.BLL.Service;
using ToyWebService.Application.BLL.Service.Interfaces;
using ToyWebService.Application.DAL;
using ToyWebService.Application.DAL.Interfaces;

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
        SetupDI(services);

        services.AddGrpc();
    }

    private void SetupDI(IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        
        app.UseEndpoints(e =>
        {
            e.MapGrpcService<GreeterService>();
            e.MapGrpcService<ItemsService>();
            e.MapGet("/",
                () =>
                    "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        });
    }
}