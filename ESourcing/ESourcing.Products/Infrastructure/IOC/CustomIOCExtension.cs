using ESourcing.Products.Data;
using ESourcing.Products.Data.Interfaces;
using ESourcing.Products.Repositories;
using ESourcing.Products.Repositories.Interfaces;
using ESourcing.Products.Settings.ProductDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ESourcing.Products.Infrastructure.IOC
{
    public static class CustomIOCExtension
    {
        public static void AddSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));
            #endregion

            #region Singleton Service Dependencies
            services.AddSingleton<IProductDatabaseSettings>(provider => provider.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            #endregion
        }

        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProductContext, ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ESourcing.Products",
                    Version = "1.0.0"
                });
            });
        }
    }
}
