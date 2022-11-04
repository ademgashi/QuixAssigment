using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quix.Console;
using Quix.Core.Services;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogDebug("Host created.");

var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<App>().Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<IPalindromeChecker, PalindromeChecker>();
            services.AddSingleton<App>();
        });
}
