using Repres.Domain.Contracts;

namespace Repres.Domain.Entities.ThirdParty
{
    public class Api : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Scopes { get; set; }
    }
}
