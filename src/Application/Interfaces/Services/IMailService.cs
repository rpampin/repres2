using Repres.Application.Requests.Mail;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}