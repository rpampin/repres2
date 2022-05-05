using Repres.Application.Features.TimeZones.Queries.GetAll;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repres.Client.Infrastructure.Managers.TimeZone
{
    public interface ITimeZoneManager : IManager
    {
        Task<IResult<List<GetAllTimeZonesResponse>>> GetAllAsync();
    }
}
