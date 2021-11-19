using ESourcing.UI.Clients;
using ESourcing.UI.Core.Entities;
using ESourcing.UI.Core.Repositories;
using ESourcing.UI.Core.Repositories.Base;
using ESourcing.UI.Infrastructure.Data;
using ESourcing.UI.Infrastructure.Repositories;
using ESourcing.UI.Infrastructure.Repositories.Base;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ESourcing.UI
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
            services.AddDbContext<WebAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebAppConnection")));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddDefaultTokenProviders()
              .AddEntityFrameworkStores<WebAppContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddFluentValidation(configurationExpression =>
            {
                configurationExpression.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.ConfigureApplicationCookie(options =>
                {
                    options.Cookie.Name = "MicroserviceArchitectureIdentityCookie";
                    options.LoginPath = new PathString("/User/SignIn");
                    options.LogoutPath = new PathString("/User/SignOut");
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.SlidingExpiration = false;

                    options.Cookie.HttpOnly = false;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                });

            services.AddAuthentication()
                .AddFacebook(options =>
            {
                options.AppId = "942618773003475";
                options.AppSecret = "c6e0b1ddff2cc428c8242a18ae46d8a8";
            })
                .AddGoogle(options =>
            {
                options.ClientId = "";
                options.ClientSecret = "";
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddHttpClient<ProductClient>();
            services.AddHttpClient<AuctionClient>();
            services.AddHttpClient<BidClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auction}/{action=Index}/{id?}");
            });
        }
    }
}
