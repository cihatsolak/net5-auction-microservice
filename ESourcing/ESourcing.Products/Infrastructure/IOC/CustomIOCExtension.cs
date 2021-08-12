using ESourcing.Products.Settings.ProductDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ESourcing.Products.Infrastructure.IOC
{
    public static class CustomIOCExtension
    {
        public static void AddSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configures
            services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));
            #endregion

            #region Singleton Services
            services.AddSingleton<IProductDatabaseSettings>(provider => provider.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            #endregion
        }
    }
}
