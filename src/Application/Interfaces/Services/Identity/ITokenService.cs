using Repres.Application.Interfaces.Common;
using Repres.Application.Requests.Identity;
using Repres.Application.Responses.Identity;
using Repres.Shared.Wrapper;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}