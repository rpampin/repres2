using Repres.Application.Interfaces.Common;
using Repres.Shared.Wrapper;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Services.ThirdParty
{
    public interface IOuraApiService : IApiService
    {
        Task<IResult> ResetUserData(string userId);
    }
}
