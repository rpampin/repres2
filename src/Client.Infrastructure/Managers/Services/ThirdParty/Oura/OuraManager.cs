using Repres.Client.Infrastructure.Extensions;
using Repres.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Services.ThirdParty.Oura
{
    public class OuraManager : IOuraManager
    {
        private readonly HttpClient _httpClient;

        public OuraManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult> ResetUserData(string userId)
        {
            var response = await _httpClient.PostAsync(Routes.OuraEndpoint.ResetUserData(userId), null);
            return await response.ToResult();
        }
    }
}
