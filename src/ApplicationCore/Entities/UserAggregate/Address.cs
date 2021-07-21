using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vimo.ApplicationCore.Entities.UserAggregate
{
    public class UserAddress : UpsertEntity
    {
        public UserAddress()
        {

        }

        public UserAddress(int id)
        {
            Id = id;
        }

         public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string GeoLat { get; set; }
        public string GeoLng { get; set; }
    }
}