using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty
{
    public class ApiByUser : AuditableEntity<int>
    {
        public string UserId { get; set; }
        public Api Api { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessExpiryDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshExpiryDate { get; set; }
    }
}
