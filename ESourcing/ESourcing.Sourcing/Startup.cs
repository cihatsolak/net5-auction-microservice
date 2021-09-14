using ESourcing.Sourcing.Hubs.Auctions;
using ESourcing.Sourcing.Infrastructure.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESourcing.Sourcing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSettingsConfigurations(Configuration);
            services.AddServiceConfiguration();
            services.AddSwaggerConfiguration();
            services.AddEventBusConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/swagger/v1/swagger.json", "ESourcing.Sourcing v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapHub<AuctionHub>("/auctionhub");
                endpoints.MapControllers();
            });
        }
    }
}
