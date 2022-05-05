using Repres.Application.Features.TimeZones.Queries.GetAll;
using Repres.Client.Infrastructure.Extensions;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.TimeZone
{
    public class TimeZoneManager : ITimeZoneManager
    {
        private readonly HttpClient _httpClient;

        public TimeZoneManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<GetAllTimeZonesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.TimeZoneEndpoints.GetAll);
            return await response.ToResult<List<GetAllTimeZonesResponse>>();
        }
    }
}
