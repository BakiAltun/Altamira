using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vimo.ApplicationCore.Services.UserServices;
using Vimo.Infrastructure.ApiDocumentation;
using Vimo.Infrastructure.Data.Library;
using MediatR;
using Vimo.Infrastructure.Identity;
using Vimo.Infrastructure.Caching;
using Vimo.ApplicationCore.Queries;
using Vimo.Infrastructure.Validations;
using Vimo.Web.Api.Infrastructure.Filters;
using System.Threading.Tasks;

namespace Vimo.Web.Api
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
            services.RegisterDb(_configuration);

            services.AddApiDocumentation(_configuration);
            services.AddMediatR(typeof(SaveUserCommand).Assembly);
            services.RegisterIdentity(_configuration);
            services.RegisterCaching(_configuration);
            services.RegisterValidations(_configuration);

            services.AddScoped<IUserQueries, UserQueries>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseApiDocumentation();
            app.UseDb();
            app.UseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async context =>
                    {
                        context.Response.Redirect($"swagger");

                        await Task.CompletedTask;
                    });
        }
    }
}
