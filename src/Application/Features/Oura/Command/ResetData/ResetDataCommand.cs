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
        private readonly IOuraApiService _ouraApiService;

        public ResetDataHandler(IOuraApiService ouraApiService)
        {
            _ouraApiService = ouraApiService;
        }

        public async Task<IResult> Handle(ResetDataCommand request, CancellationToken cancellationToken)
        {
            return await _ouraApiService.ResetUserData(request.UserId);
        }
    }
}
