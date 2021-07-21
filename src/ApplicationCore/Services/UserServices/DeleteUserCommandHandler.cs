using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Exceptions;
using Vimo.ApplicationCore.Interfaces;
using Vimo.ApplicationCore.Interfaces.Data;

namespace Vimo.ApplicationCore.Services.UserServices
{
    public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAsyncRepository<User> _repository;
        private readonly IAppCaching _appCaching;

        public DeleteAnnouncementCommandHandler(IAsyncRepository<User> repository, IAppCaching appCaching)
        {
            _repository = repository;
            _appCaching = appCaching;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new NotFoundException();

            await _repository.DeleteAsync(entity);
            _appCaching.Delete<User>(entity.Id);
            return true;
        }
    }
}