namespace Repres.Application.Features.Apis.Queries.GetAccess
{
    public class GetApiAccessResponse
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string AuthUrl { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
