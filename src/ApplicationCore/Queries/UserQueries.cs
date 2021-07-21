using System.Collections.Generic;
using System.Linq;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Exceptions;
using Vimo.ApplicationCore.Interfaces;
using Vimo.ApplicationCore.Interfaces.Data;
using Vimo.ApplicationCore.Queries.Models;
using Vimo.ApplicationCore.Specifications;

namespace Vimo.ApplicationCore.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IAppCaching _appCaching;

        public UserQueries(IAsyncRepository<User> repository, IAppCaching appCaching)
        {
            _repository = repository;
            _appCaching = appCaching;
        }

        public UserModel Get(int id)
        {
            var entity = _appCaching.Get<User>(id, async () =>
            {
                return await _repository.FirstOrDefaultAsync(new UserWithItemsSpecification(id));
            });

            if (entity == null)
                throw new NotFoundException();

            return MapToModel(entity);
        }

        public IList<UserModel> Get()
        {
            var entities = _appCaching.Get<User>();

            if (entities == null)
                throw new NotFoundException();

            return entities.Select(MapToModel).ToList();
        }

        private UserModel MapToModel(User entity)
        {
            if (entity == null)
                return null;
                
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Username = entity.Username,
                Email = entity.Email,
                Phone = entity.Phone,
                WebSite = entity.WebSite,
                Addresses = entity.Addresses.Select(x => new UserModel.AddressItem
                {
                    Id = x.Id,
                    Street = x.Street,
                    Suite = x.Suite,
                    City = x.City,
                    Zipcode = x.Zipcode,
                    GeoLat = x.GeoLat,
                    GeoLng = x.GeoLng
                }).ToList(),
                Companies = entity.Companies.Select(x => new UserModel.CompanyItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Bs = x.Bs,
                    CatchPhrase = x.CatchPhrase,
                }).ToList()
            };
        }
    }
    public interface IUserQueries
    {
        UserModel Get(int id);
        IList<UserModel> Get();
    }
}