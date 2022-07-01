using MediatR;
using Repres.Shared.Wrapper;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Languages.Queries.GetAll
{
    public class GetAllLanguagesQuery : IRequest<Result<List<GetAllLanguagesResponse>>> { }

    internal class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, Result<List<GetAllLanguagesResponse>>>
    {
        public async Task<Result<List<GetAllLanguagesResponse>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
            => await Result<List<GetAllLanguagesResponse>>.SuccessAsync(CultureInfo.GetCultures(CultureTypes.AllCultures).Select(lg => new GetAllLanguagesResponse()
            {
                Id = lg.IetfLanguageTag,
                DisplayName = lg.NativeName
            }).ToList());
    }
}
