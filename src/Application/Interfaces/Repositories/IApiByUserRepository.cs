using Repres.Domain.Entities.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Repositories
{
    public interface IApiByUserRepository
    {
        Task<ApiByUser> GetApiByUser(string api, string user);
        Task<bool> IsUserAuthenticated(string api, string user);
    }
}
