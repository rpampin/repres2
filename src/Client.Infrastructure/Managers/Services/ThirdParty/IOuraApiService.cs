using Repres.Application.Interfaces.Services.ThirdParty;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Services.ThirdParty
{
    public interface IOuraApiService : IApiService
    {
        Task ResetUserData(string userId);
    }
}
