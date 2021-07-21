using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Interfaces.Data;
using Vimo.Infrastructure.Data.Library;
using Vimo.Infrastructure.Data.Library.Config;

namespace Vimo.Infrastructure.Data.Library
{
    public static class DataExtensions
    {
        public static IServiceCollection RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<CatalogContext>(c =>
                                                c.LogTo(Console.WriteLine)
                                                .UseSqlServer(connectionString));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddHttpClient<ISeedDataService, SeedDataService>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddSingleton<UserConfiguration>();
            return services;
        }

        public static void UseDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CatalogContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}