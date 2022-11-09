using MediatR;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Oura.Command.ResetData
{
    public class ResetDataCommand : IRequest<IResult>
    {
        public string UserId { get; set; }
    }

    internal class ResetDataHandler : IRequestHandler<ResetDataCommand, IResult>
    {
        private readonly IApiService _apiService;

        public ResetDataHandler(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IResult> Handle(ResetDataCommand request, CancellationToken cancellationToken)
        {
            await _apiService.ResetUserData(request.UserId);
            return Result.Success();
        }
    }
}
