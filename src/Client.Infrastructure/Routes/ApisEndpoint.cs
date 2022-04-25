namespace Repres.Client.Infrastructure.Routes
{
    public static class ApisEndpoint
    {
        public static string GetUserAccess(string userId)
        {
            return $"api/v1/apis/access/{userId}";
        }
        public static string GetAll = "api/v1/apis";
        public static string Save = "api/v1/apis";
        public static string PersistApiToken = "api/v1/apis/token/persist";
    }
}
