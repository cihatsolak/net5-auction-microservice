﻿using ESourcing.Sourcing.Settings.SourcingDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
    }
}