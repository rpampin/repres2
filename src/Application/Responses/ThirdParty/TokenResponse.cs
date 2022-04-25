namespace Repres.Application.Responses.ThirdParty
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int? AccessExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public int? RefreshExpiresIn { get; set; }
    }
}
