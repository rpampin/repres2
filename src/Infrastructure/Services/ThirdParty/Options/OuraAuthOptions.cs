namespace Repres.Infrastructure.Services.ThirdParty.Options
{
    public class OuraAuthOptions
    {
        public string AuthBaseAddress { get; set; }
        public string ApiBaseAddress { get; set; }
        public string RedirectUri { get; set; }
        public string AuthorizeUrl { get; set; }
        public string AccessTokenUrl { get; set; }
        public string RefreshTokenUrl { get; set; }
        public string SleepSummaries { get; set; }
        public string ActivitySummaries { get; set; }
        public string ReadinessSummaries { get; set; }

    }
}
