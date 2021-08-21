using ESourcing.Sourcing.Data;
using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Interfaces;
using ESourcing.Sourcing.Settings.SourcingDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<ISourcingContext, SourcingContext>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
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
    }
}
