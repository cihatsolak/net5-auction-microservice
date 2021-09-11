using ESourcing.UI.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ESourcing.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            CreateAndSeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateAndSeedDatabase(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                var webAppContext = serviceProvider.GetRequiredService<WebAppContext>();
                WebAppContextSeed.SeedAsync(webAppContext, loggerFactory).Wait();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(exception, "An error occured seeding the DB");
            }
        }
    }
}
