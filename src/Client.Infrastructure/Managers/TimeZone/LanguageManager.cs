using Repres.Application.Features.Languages.Queries.GetAll;
using Repres.Client.Infrastructure.Extensions;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.TimeZone
{
    public class LanguageManager : ILanguageManager
    {
        private readonly HttpClient _httpClient;

        public LanguageManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<GetAllLanguagesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.LanguageEndpoints.GetAll);
            return await response.ToResult<List<GetAllLanguagesResponse>>();
        }
    }
}
