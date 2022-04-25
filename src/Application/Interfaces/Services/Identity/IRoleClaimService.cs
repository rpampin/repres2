using System.Collections.Generic;
using System.Threading.Tasks;
using Repres.Application.Interfaces.Common;
using Repres.Application.Requests.Identity;
using Repres.Application.Responses.Identity;
using Repres.Shared.Wrapper;

namespace Repres.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}