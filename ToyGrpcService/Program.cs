using Microsoft.AspNetCore.Server.Kestrel.Core;
using ToyGrpcService.Services;
using ToyWebService.Application.BLL.Service;
using ToyWebService.Application.BLL.Service.Interfaces;
using ToyWebService.Application.DAL;
using ToyWebService.Application.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Setup a HTTP/2 endpoint without TLS.
        options.ListenLocalhost(5042, o => o.Protocols =
            HttpProtocols.Http2);
    }
});

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<ItemsService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();