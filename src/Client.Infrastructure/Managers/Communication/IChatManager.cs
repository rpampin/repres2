using Repres.Application.Models.Chat;
using Repres.Application.Responses.Identity;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Application.Interfaces.Chat;

namespace Repres.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}