using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using Repres.Application.Interfaces.Services;
using Repres.Application.Requests.Mail;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Shared.Services
{
    public class GmailMailService : IMailService
    {
        private readonly string[] _scopes = { GmailService.Scope.GmailSend };
        private readonly string _applicationName = "Gmail API .NET Oura Ring";

        private readonly ILogger<GmailMailService> _logger;

        private GmailService GmailService { get; set; }

        public GmailMailService(ILogger<GmailMailService> logger)
        {
            _logger = logger;
            InitializeService();
        }

        private void InitializeService()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential
                    .FromStream(stream)
                    .CreateScoped(_scopes)
                    .CreateWithUser("itsme@marcdressen.com");
            }

            GmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName,
            });
        }

        public Task SendAsync(MailRequest mailRequest)
        {
            try
            {
                var newMsg = new Message();
                string message = $"To: {mailRequest.To}\r\nSubject: {mailRequest.Subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{mailRequest.Body}";
                newMsg.Raw = this.Base64UrlEncode(message.ToString());
                Message response = GmailService.Users.Messages.Send(newMsg, "me").Execute();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Task.CompletedTask;
                //throw;
            }
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            // Special "url-safe" base64 encode.
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}