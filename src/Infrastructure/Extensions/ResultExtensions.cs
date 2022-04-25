using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Extensions
{
    internal static class ResultExtensions
    {
        internal static async Task<T> ToResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<T>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return responseObject;
        }
    }
}