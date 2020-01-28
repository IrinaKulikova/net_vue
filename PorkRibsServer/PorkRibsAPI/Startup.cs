using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using PorkRibsData.DataBase;
using PorkRibsData.Models;
using PorkRibsData.DataBase.Intit.Interfaces;
using PorkRibsRepositories;
using PorkRibsAPI.Factories.Interface;
using PorkRibsData.DataBase.Init;
using PorkRibsAPI.Factories;
using PorkRibsAPI.TokenFactories.Interface;
using PorkRibsAPI.TokenFactories;
using Microsoft.OpenApi.Models;
using PorkRibsRepositories.Interfaces;
using PorkRibsAPI.ConfigurationServices;

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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PorkRibsAPI", Version = "v1" });
            });

            services.AddTransient<IInitializer, Initializer>();
            services.AddTransient<IJWTTokenFactory, JWTTokenFactory>();
            services.AddTransient<IRefreshTokenFactory, RefreshTokenFactory>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PorkRibsAPI V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}