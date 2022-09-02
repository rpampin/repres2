using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Domain.Entities.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Repositories
{
    public class ApiByUserRepository : IApiByUserRepository
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepositoryAsync<ApiByUser, int> _repository;

        public ApiByUserRepository(IRepositoryAsync<ApiByUser, int> repository, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
        }

        public Task<ApiByUser> GetApiByUser(string api, string user)
            => _repository.Entities.Include(e => e.Api).FirstOrDefaultAsync(x => x.UserId == user && x.Api.Name == api);

        public Task<bool> IsUserAuthenticated(string api, string user)
            => _repository.Entities.AnyAsync(x => 
                x.UserId == user && 
                x.Api.Name == api &&
                ((
                    x.AccessExpiryDate.HasValue && x.AccessExpiryDate.Value >= _dateTimeService.NowUtc) ||
                    x.RefreshExpiryDate.HasValue && x.RefreshExpiryDate.Value >= _dateTimeService.NowUtc
                ));
    }
}
