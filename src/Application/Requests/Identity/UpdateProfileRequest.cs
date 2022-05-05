using System.ComponentModel.DataAnnotations;

namespace Repres.Application.Requests.Identity
{
    public class UpdateProfileRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string TimeZoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}