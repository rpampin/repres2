using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Repres.Application.Interfaces.Repositories;
using Repres.Domain.Entities.ThirdParty;
using Repres.Shared.Constants.Application;
using Repres.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Commands.Edit
{
    public class EditApiCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Scopes { get; set; }
    }

    internal class EditApiCommandHandler : IRequestHandler<EditApiCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<EditApiCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public EditApiCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<EditApiCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(EditApiCommand command, CancellationToken cancellationToken)
        {
            var api = await _unitOfWork.Repository<Api>().GetByIdAsync(command.Id);
            if (api != null)
            {
                api.ClientId = command.ClientId ?? api.ClientId;
                api.DisplayName = command.DisplayName ?? api.Name;
                api.Secret = command.Secret ?? api.Secret;
                api.Scopes = command.Scopes ?? api.Scopes;
                await _unitOfWork.Repository<Api>().UpdateAsync(api);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllApisCacheKey);
                return await Result<int>.SuccessAsync(api.Id, _localizer["Api Updated"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Api Not Found!"]);
            }
        }
    }
}
