using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ToyGrpcService;

public class Program
{
    public static void Main(params string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.ConfigureKestrel(options =>
                {
                    options.ListenLocalhost(5042,
                        o => o.Protocols = HttpProtocols.Http2);
                });
                builder.UseStartup<Startup>();
            });
    }
}