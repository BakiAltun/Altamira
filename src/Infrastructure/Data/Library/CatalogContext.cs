using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.Infrastructure.Data.Library.Config;

namespace Vimo.Infrastructure.Data.Library
{
    public partial class CatalogContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public CatalogContext(DbContextOptions<CatalogContext> options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;

            // dotnet ef migrations add Initial -s ../Presentation/Web/Api/Vimo.Web.Api.csproj -o Data/Library/Migrations
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration((IEntityTypeConfiguration<User>)_serviceProvider.GetService(typeof(UserConfiguration)));



        }

          
    }
}