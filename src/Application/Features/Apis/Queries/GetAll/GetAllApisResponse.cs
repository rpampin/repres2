namespace Repres.Application.Features.Apis.Queries.GetAll
{
    public class GetAllApisResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }
}
