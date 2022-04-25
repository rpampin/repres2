using Repres.Application.Features.Apis.Commands.Edit;
using Repres.Application.Features.Apis.Commands.TokenPersist;
using Repres.Application.Features.Apis.Queries.GetAccess;
using Repres.Application.Features.Apis.Queries.GetAll;
using Repres.Client.Infrastructure.Extensions;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Services.ThirdParty
{
    public class ApiManager : IApiManager
    {
        private readonly HttpClient _httpClient;

        public ApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<GetAllApisResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.ApisEndpoint.GetAll);
            return await response.ToResult<List<GetAllApisResponse>>();
        }

        public async Task<IResult<List<GetApiAccessResponse>>> GetUserApiAccess(GetApiAccessQuery query)
        {
            var response = await _httpClient.GetAsync(Routes.ApisEndpoint.GetUserAccess(query.UserId));
            return await response.ToResult<List<GetApiAccessResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(EditApiCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ApisEndpoint.Save, request);
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> PersistApiToken(TokenPersistCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ApisEndpoint.PersistApiToken, request);
            return await response.ToResult<string>();
        }
    }
}
