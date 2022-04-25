using MediatR;
using Microsoft.Extensions.Localization;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Application.Responses.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using Repres.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Commands.RefreshTokenPersist
{
    public class RefreshTokenPersistCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public string Api { get; set; }
        public string RefreshToken { get; set; }
    }

    internal class RefreshTokenPersistCommandHandler : IRequestHandler<RefreshTokenPersistCommand, Result<string>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IDateTimeService _dateTimeService;
        private readonly IEnumerable<IApiService> _apiServices;
        private readonly IApiByUserRepository _apiByUserRepository;
        private readonly IStringLocalizer<RefreshTokenPersistCommandHandler> _localizer;

        public RefreshTokenPersistCommandHandler(IUnitOfWork<int> unitOfWork,
                                                 IDateTimeService dateTimeService,
                                                 IEnumerable<IApiService> apiServices,
                                                 IApiByUserRepository apiByUserRepository,
                                                 IStringLocalizer<RefreshTokenPersistCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _apiServices = apiServices ?? throw new ArgumentNullException(nameof(apiServices));
            _apiByUserRepository = apiByUserRepository ?? throw new ArgumentNullException(nameof(apiByUserRepository));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public async Task<Result<string>> Handle(RefreshTokenPersistCommand command, CancellationToken cancellationToken)
        {
            var apiService = _apiServices.FirstOrDefault(s => s.Name == command.Api);

            if (apiService != null)
            {
                TokenResponse token;
                try
                {
                    token = await apiService.GetAuthorizationRefreshToken(command.RefreshToken);
                }
                catch (InvalidOperationException ex)
                {
                    return await Result<string>.FailAsync(ex.Message);
                }

                var apiByUser = await _apiByUserRepository.GetApiByUser(command.Api, command.UserId);
                apiByUser.AccessToken = token.AccessToken;
                apiByUser.RefreshToken = token.RefreshToken;
                apiByUser.AccessExpiryDate = token.AccessExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.AccessExpiresIn.Value) : null;
                apiByUser.RefreshExpiryDate = token.RefreshExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.RefreshExpiresIn.Value) : null;
                await _unitOfWork.Repository<ApiByUser>().UpdateAsync(apiByUser);

                await _unitOfWork.Commit(cancellationToken);

                return await Result<string>.SuccessAsync(token.AccessToken, string.Empty);
            }

            return await Result<string>.FailAsync(_localizer[$"{command.Api} api implementation not found!"]);
        }
    }
}
