using Repres.Application.Features.Languages.Queries.GetAll;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.TimeZone
{
    public interface ILanguageManager : IManager
    {
        Task<IResult<List<GetAllLanguagesResponse>>> GetAllAsync();
    }
}
