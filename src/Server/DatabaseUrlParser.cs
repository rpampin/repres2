using System.Text.RegularExpressions;

namespace Repres.Server
{
    public static class DatabaseUrlParser
    {
        public static string Parse(string databaseUrl)
        {
#if DEBUG
            string pattern = @"postgres://(.+):(.*)@(.+):(\d*)/(.+)";
            string connectionStringTemplate = "User ID={0};Password={1};Host={2};Port={3};Database={4};";
#else
            string pattern = @"postgres://([a-zA-Z0-9]*):([a-zA-Z0-9]*)@([a-zA-Z0-9-.]*):(\d*)/([a-zA-Z0-9-.]*)";
            string connectionStringTemplate = "User ID={0};Password={1};Host={2};Port={3};Database={4};SSL Mode=Require;Trust Server Certificate=true;";
#endif
            var match = Regex.Match(databaseUrl, pattern);
            var connectionString = string.Format(
                connectionStringTemplate,
                match.Groups[1].Value,
                match.Groups[2].Value,
                match.Groups[3].Value,
                match.Groups[4].Value,
                match.Groups[5].Value);
            return connectionString;
        }
    }
}
