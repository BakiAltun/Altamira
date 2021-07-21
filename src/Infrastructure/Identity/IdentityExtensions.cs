using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Vimo.Infrastructure.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(UserManager.SecretKey);

            services.AddAuthentication(x =>
                   {
                       x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   }).AddJwtBearer(x =>
         {
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

            services.AddScoped<IUserManager, UserManager>();

            return services;
        }
    }
}