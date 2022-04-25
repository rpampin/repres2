using Repres.Domain.Entities.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Repositories
{
    public interface IApiRepository
    {
        Task<Api> GetApiByName(string apiName);
        Task CreateApi(string apiName);
        Task<bool> ApiExists(string apiName);
    }
}
