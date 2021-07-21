using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Interfaces;
using Vimo.ApplicationCore.Interfaces.Data;
using Vimo.ApplicationCore.Specifications;

namespace Vimo.ApplicationCore.Services.UserServices
{
    public class SaveAnnouncementCommandHandler : IRequestHandler<SaveUserCommand, bool>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IAppCaching _appCaching;
        public SaveAnnouncementCommandHandler(IAsyncRepository<User> repository, IAppCaching appCaching)
        {
            _repository = repository;
            _appCaching = appCaching;
        }

        public async Task<bool> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FirstOrDefaultAsync(new UserWithItemsSpecification(request.Id)) ?? new();

            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Phone = request.Phone;
            entity.Username = request.Username;
            entity.WebSite = request.WebSite;

            if (request.Address != null)
                entity.AddAddress(new(request.Address.Id)
                {
                    Street = request.Address.Street,
                    Suite = request.Address.Suite,
                    City = request.Address.City,
                    Zipcode = request.Address.Zipcode,
                    GeoLat = request.Address.GeoLat,
                    GeoLng = request.Address.GeoLng
                });

            if (request.Company != null)
                entity.AddCompany(new(request.Address.Id)
                {
                    Name = request.Company.Name,
                    CatchPhrase = request.Company.CatchPhrase,
                    Bs = request.Company.Bs,
                });

            await Save(entity);

            request.Id = entity.Id;

            _appCaching.Set(entity, entity.Id);
            return true;
        }

        private async Task Save(User entity)
        {
            if (entity.Id == default)
            {
                entity.SetInsert();
                await _repository.AddAsync(entity);
            }
            else
            {
                entity.SetUpdate();
                await _repository.UpdateAsync(entity);
            }
        }
    }

}