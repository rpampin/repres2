using Repres.Application.Responses.Identity;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Application.Interfaces.Chat;
using Repres.Application.Models.Chat;

namespace Repres.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}