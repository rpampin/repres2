using Repres.Application.Features.Apis.Commands.Edit;
using Repres.Application.Features.Apis.Commands.TokenPersist;
using Repres.Application.Features.Apis.Queries.GetAccess;
using Repres.Application.Features.Apis.Queries.GetAll;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Services.ThirdParty
{
    public interface IApiManager : IManager
    {
        Task<IResult<List<GetApiAccessResponse>>> GetUserApiAccess(GetApiAccessQuery query);
        Task<IResult<List<GetAllApisResponse>>> GetAllAsync();
        Task<IResult<int>> SaveAsync(EditApiCommand request);
        Task<IResult<string>> PersistApiToken(TokenPersistCommand request);
    }
}
