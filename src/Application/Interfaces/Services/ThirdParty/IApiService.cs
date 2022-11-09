using Hangfire.Server;
using Repres.Application.Interfaces.Common;
using Repres.Application.Responses.ThirdParty;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Services.ThirdParty
{
    public interface IApiService : IService
    {
        string Name { get; }
        Task<string> GetAuthUri();
        Task<TokenResponse> GetAuthorizationToken(string code, string state);
        Task<TokenResponse> GetAuthorizationRefreshToken(string refreshToken);
        Task ExecuteScheduledJob(string userId, PerformContext context, DateTime? start, DateTime? end, CancellationToken cancellationToken);
        Task ResetUserData(string userId);
    }
}
