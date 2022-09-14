using Repres.Shared.Wrapper;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Services.ThirdParty.Oura
{
    public interface IOuraManager : IManager
    {
        Task<IResult> ResetUserData(string userId);
    }
}
