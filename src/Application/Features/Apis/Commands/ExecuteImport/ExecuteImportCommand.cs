using MediatR;
using Repres.Application.Interfaces.Services.ThirdParty;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Commands.ExecuteImport
{
    public class ExecuteImportCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }

    internal class ExecuteImportCommandHandler : IRequestHandler<ExecuteImportCommand, Unit>
    {
        private readonly IEnumerable<IApiService> _apiServices;

        public ExecuteImportCommandHandler(IEnumerable<IApiService> apiServices)
        {
            _apiServices = apiServices;
        }

        public async Task<Unit> Handle(ExecuteImportCommand request, CancellationToken cancellationToken)
        {
            foreach (var service in _apiServices)
                await service.ExecuteScheduledJob(request.UserId, null, request.Start, request.End, cancellationToken);
            return Unit.Value;
        }
    }
}
