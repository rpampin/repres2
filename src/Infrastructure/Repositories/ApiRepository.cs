using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Domain.Entities.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly IRepositoryAsync<Api, int> _repository;

        public ApiRepository(IRepositoryAsync<Api, int> repository)
        {
            _repository = repository;
        }

        public async Task CreateApi(string apiName)
            => await _repository.AddAsync(new Api { Name = apiName });

        public async Task<Api> GetApiByName(string apiName)
            => await _repository.Entities.FirstOrDefaultAsync(x => x.Name == apiName);

        public async Task<bool> ApiExists(string apiName)
            => await _repository.Entities.AnyAsync(a => a.Name == apiName);
    }
}
