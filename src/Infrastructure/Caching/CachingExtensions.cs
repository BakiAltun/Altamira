using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Interfaces;
using Vimo.ApplicationCore.Interfaces.Data;
using Vimo.ApplicationCore.Specifications;

namespace Vimo.Infrastructure.Caching
{
    public static class CachingExtensions
    {
        public static IServiceCollection RegisterCaching(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Redis");

            services.AddScoped<IAppCaching>(provider =>
         {
             return new AppCaching(connectionString);
         });

            return services;
        }

        public static void UseCaching(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appCaching = serviceScope.ServiceProvider.GetRequiredService<IAppCaching>();

                CachingUser(serviceScope, appCaching);
            }
        }

        private static void CachingUser(IServiceScope serviceScope, IAppCaching appCaching)
        {
            var userRepository = serviceScope.ServiceProvider.GetRequiredService<IAsyncRepository<User>>();
            var userEntities = userRepository.ListAsync(new UserWithItemsSpecification()).GetAwaiter().GetResult();
            foreach (var entity in userEntities)
                appCaching.Set(entity, entity.Id);
        }
    }
}