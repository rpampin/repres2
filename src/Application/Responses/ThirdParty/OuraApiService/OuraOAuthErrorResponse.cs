namespace Repres.Application.Responses.ThirdParty.OuraApiService
{
    public class OuraOAuthErrorResponse
    {
        public int status { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}
