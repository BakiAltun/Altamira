using MediatR;

namespace Vimo.ApplicationCore.Services.UserServices
{
    public class SaveUserCommand : IRequest<bool> 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }

        public AddressItem Address { get; set; }
        public CompanyItem Company { get; set; }

        public class AddressItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Street { get; set; }
            public string Suite { get; set; }
            public string City { get; set; }
            public string Zipcode { get; set; }
            public string GeoLat { get; set; }
            public string GeoLng { get; set; }
        }


        public class CompanyItem
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string CatchPhrase { get; set; }
            public string Bs { get; set; }
        }
    }
}