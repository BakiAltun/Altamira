using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Vimo.ApplicationCore.Interfaces;

namespace Vimo.ApplicationCore.Entities.UserAggregate
{ 

    public class User : UpsertEntity, IAggregateRoot
    {

        public User()
        {

        }

        public User(int id)
        {
            Id = id;
        }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
 
        private List<UserAddress> _addresses = new List<UserAddress>();
        public IReadOnlyList<UserAddress> Addresses => _addresses.AsReadOnly();
 
         private List<UserCompany> _companies = new List<UserCompany>();
        public IReadOnlyList<UserCompany> Companies => _companies.AsReadOnly();

        public void AddAddress(UserAddress address)
        {
            var entity = _addresses.FirstOrDefault(x => x.Id == address.Id) ?? new();

            entity.City = address.City;
            entity.GeoLat = address.GeoLat;
            entity.GeoLng = address.GeoLng;
         
            entity.Street = address.Street;
            entity.Suite = address.Suite;
            entity.Zipcode = address.Zipcode;

            entity.SetUpdate();
            if (entity.Id == default)
            {
                entity.SetInsert();
                _addresses.Add(entity);
            }
        }

        public void AddCompany(UserCompany company)
        {
            var entity = _companies.FirstOrDefault(x => x.Id == company.Id) ?? new();

            entity.Bs = company.Bs;
            entity.CatchPhrase = company.CatchPhrase;
            entity.Name = company.Name;

            entity.SetUpdate();
            if (entity.Id == default)
            {
                entity.SetInsert();
                _companies.Add(entity);
            }
        }
    }
}