using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PorkRibsData.DataBase;
using PorkRibsData.Settings;
using System.Text;
using System.Threading.Tasks;

namespace PorkRibsData.ConfigurationServices
{
    public static class JWTtokenService
    {
        public static void AddJWTService(this IServiceCollection services,
                                         IConfiguration configuration)
        {
            var JWTConfigurationSection = configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(JWTConfigurationSection);

            var JWTConfiguration = JWTConfigurationSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(JWTConfiguration.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userManager = context.HttpContext.RequestServices
                            .GetRequiredService<PorkRibsDbContext>().Users;
                        var name = context.Principal.Identity.Name;
                        var profile = userManager.FindAsync(name);

                        if (profile == null)
                        {
                            context.Fail("Unauthorized");
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
