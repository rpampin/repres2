using MediatR;
using Repres.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.TimeZones.Queries.GetAll
{
    public class GetAllTimeZonesQuery : IRequest<Result<List<GetAllTimeZonesResponse>>> { }

    internal class GetAllTimeZonesQueryHandler : IRequestHandler<GetAllTimeZonesQuery, Result<List<GetAllTimeZonesResponse>>>
    {
        public async Task<Result<List<GetAllTimeZonesResponse>>> Handle(GetAllTimeZonesQuery request, CancellationToken cancellationToken)
            => await Result<List<GetAllTimeZonesResponse>>.SuccessAsync(TimeZoneInfo.GetSystemTimeZones().Select(tz => new GetAllTimeZonesResponse()
            {
                Id = tz.Id,
                DisplayName = tz.DisplayName
            }).ToList());
    }
}
