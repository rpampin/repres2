using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System.IO;

namespace Repres.Infrastructure.Helper
{
    public class GoogleSheetsHelper
    {
        public SheetsService SheetService { get; set; }
        public DriveService DriveService { get; set; }
        const string APPLICATION_NAME = "OuraRing";
        static readonly string[] Scopes = 
        { 
            SheetsService.Scope.Spreadsheets, 
            DriveService.Scope.Drive,
            DriveService.Scope.DriveFile
        };
        public GoogleSheetsHelper()
        {
            InitializeService();
        }
        private void InitializeService()
        {
            var credential = GetCredentialsFromFile();
            SheetService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
            DriveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
        }

        private GoogleCredential GetCredentialsFromFile()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            return credential;
        }
    }
}
