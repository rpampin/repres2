using Repres.Application.Responses.Audit;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.Audit
{
    public interface IAuditManager : IManager
    {
        Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync();

        Task<IResult<string>> DownloadFileAsync(string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false);
    }
}