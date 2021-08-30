using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Interfaces;
using ESourcing.Sourcing.Settings.SourcingDatabase;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace ESourcing.Sourcing.Infrastructure.IOC
{
    public static class CustomIOCExtension
    {
        public static void AddSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<SourcingDatabaseSettings>(configuration.GetSection(nameof(SourcingDatabaseSettings)));
            #endregion

            #region Singleton Service Dependencies
            services.AddSingleton<ISourcingDatabaseSettings>(provider => provider.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);
            #endregion
        }
        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ISourcingContext, SourcingContext>();
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IBidRepository, BidRepository>();

            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ESourcing.Sourcing",
                    Version = "1.0.0"
                });
            });
        }

        public static void AddEventBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                ConnectionFactory connectionFactory = new()
                {
                    HostName = configuration["EventBusSettings:HostName"],
                    UserName = configuration["EventBusSettings:Password"],
                    Password = configuration["EventBusSettings:Password"]
                };
                int retryCount = int.Parse(configuration["EventBusSettings:RetryCount"]);

                return new DefaultRabbitMQPersistentConnection(connectionFactory, logger, retryCount);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();
        }
    }
}
