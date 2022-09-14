namespace Repres.Client.Infrastructure.Routes
{
    public static class AccountEndpoints
    {
        public static string Register = "api/identity/account/register";
        public static string ChangePassword = "api/identity/account/changepassword";
        public static string UpdateProfile = "api/identity/account/updateprofile";

        public static string GetProfilePicture(string userId)
        {
            return $"api/identity/account/profile-picture/{userId}";
        }

        public static string GetProfileTimeZone(string userId)
        {
            return $"api/identity/account/time-zone/{userId}";
        }

        public static string GetProfileLanguage(string userId)
        {
            return $"api/identity/account/language/{userId}";
        }

        public static string UpdateProfilePicture(string userId)
        {
            return $"api/identity/account/profile-picture/{userId}";
        }

        public static string GetProfileHasSheet(string userId)
        {
            return $"api/identity/account/has-sheet/{userId}";
        }
    }
}