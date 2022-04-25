using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Application.Requests.Identity;
using Repres.Application.Responses.Identity;
using Repres.Shared.Wrapper;

namespace Repres.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}