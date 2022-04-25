using Repres.Shared.Wrapper;
using System.Threading.Tasks;
using Repres.Application.Features.Dashboards.Queries.GetData;

namespace Repres.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}