﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using PorkRibsData.DataBase;
using PorkRibsData.Models;
using PorkRibsData.ConfigurationServices;
using PorkRibsData.DataBase.Intit.Interfaces;
using PorkRibsRepositories;
using PorkRibsAPI.Factories.Interface;
using PorkRibsData.DataBase.Init;
using PorkRibsAPI.Factories;
using PorkRibsAPI.TokenFactories.Interface;
using PorkRibsAPI.TokenFactories;

namespace PorkRibsAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PorkRibsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>()                
                .AddEntityFrameworkStores<PorkRibsDbContext>();

            services.AddJWTService(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder
                    .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });

            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 4445;
            //});

            services.AddTransient<IInitializer, Initializer>();
            services.AddTransient<IJWTTokenFactory, JWTTokenFactory>();
            services.AddTransient<IRefreshTokenFactory, RefreshTokenFactory>();
            services.AddTransient<IGenericRepository<RefreshToken>, RefreshTokenRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}