using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vimo.ApplicationCore.Entities.UserAggregate;

namespace Vimo.Infrastructure.Data.Library.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly ISeedDataService _seedDataService;

        public UserConfiguration(ISeedDataService seedDataService)
        {
            _seedDataService = seedDataService;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            var navigation = builder.Metadata.FindNavigation(nameof(User.Addresses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigation2 = builder.Metadata.FindNavigation(nameof(User.Companies));
            navigation2.SetPropertyAccessMode(PropertyAccessMode.Field);


            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<User> builder)
        {
            var seedData = _seedDataService.GetUserData().GetAwaiter().GetResult();
            int id = 1;

            builder.HasData(seedData.Select(x => new User(x.Id)
            {
                Name = x.Name,
                Email = x.Email,
                WebSite = x.Website,
                Phone = x.Phone,
                Username = x.Username,
                Password = x.Email == "Sincere@april.biz" ? "123456" : RandomAlphanumeric(8)
            }));

            builder
            .OwnsMany(x => x.Addresses)
            // .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasData(seedData.Select(x => new
            {
                Id = id++,
                UserId = x.Id,
                Street = x.Address.Street,
                City = x.Address.City,
                Suite = x.Address.Suite,
                Zipcode = x.Address.Zipcode,
                GeoLat = x.Address.Geo.Lat,
                GeoLng = x.Address.Geo.Lng,
                InsertedAt = DateTimeOffset.UtcNow,
            }));

            builder
            .OwnsMany(x => x.Companies)
            .HasData(seedData.Select(x => new
            {
                Id = id++,
                UserId = x.Id,
                Bs = x.Company.Bs,
                CatchPhrase = x.Company.CatchPhrase,
                Name = x.Company.Name,
                InsertedAt = DateTimeOffset.UtcNow,
            }));
        }

        private string RandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return RandomString(chars, length);
        }

        public string RandomString(string chars, int length)
        {
            Random random = new();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}