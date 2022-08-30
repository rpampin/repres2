using Repres.Application.Requests.Identity;
using Repres.Shared.Wrapper;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Identity.Account
{
    public interface IAccountManager : IManager
    {
        Task<IResult> ChangePasswordAsync(ChangePasswordRequest model);

        Task<IResult> UpdateProfileAsync(UpdateProfileRequest model);

        Task<IResult<string>> GetProfilePictureAsync(string userId);

        Task<IResult<int>> GetProfileUtcMinutesAsync(string userId);

        Task<IResult<string>> GetProfileLanguageAsync(string userId);

        Task<IResult<string>> UpdateProfilePictureAsync(UpdateProfilePictureRequest request, string userId);
    }
}