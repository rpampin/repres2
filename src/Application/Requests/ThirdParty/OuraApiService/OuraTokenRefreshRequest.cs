namespace Repres.Application.Requests.ThirdParty.OuraApiService
{
    public class OuraTokenRefreshRequest
    {
        public string grant_type { get => "refresh_token"; }
        public string refresh_token { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}
