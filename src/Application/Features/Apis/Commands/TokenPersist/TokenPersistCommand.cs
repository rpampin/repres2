using AutoMapper;
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

namespace Repres.Application.Features.Apis.Commands.TokenPersist
{
    public class TokenPersistCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public string Api { get; set; }
        public string Code { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
    }

    internal class TokenPersistCommandHandler : IRequestHandler<TokenPersistCommand, Result<string>>
    {
        private readonly IStringLocalizer<TokenPersistCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IApiRepository _apiRepository;
        private readonly IApiByUserRepository _apiByUserRepository;
        private readonly IEnumerable<IApiService> _apiServices;
        private readonly IDateTimeService _dateTimeService;

        public TokenPersistCommandHandler(
            IUnitOfWork<int> unitOfWork,
            IStringLocalizer<TokenPersistCommandHandler> localizer,
            IApiRepository apiRepository,
            IApiByUserRepository apiByUserRepository,
            IDateTimeService dateTimeService,
            IEnumerable<IApiService> apiServices)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _apiRepository = apiRepository;
            _apiByUserRepository = apiByUserRepository;
            _dateTimeService = dateTimeService;
            _apiServices = apiServices;
        }

        public async Task<Result<string>> Handle(TokenPersistCommand command, CancellationToken cancellationToken)
        {
            var apiService = _apiServices.FirstOrDefault(s => s.Name == command.Api);
            var api = await _apiRepository.GetApiByName(command.Api);

            if (apiService != null)
            {
                TokenResponse token;
                try
                {
                    token = await apiService.GetAuthorizationToken(command.Code, command.State);
                }
                catch (InvalidOperationException ex)
                {
                    return await Result<string>.FailAsync(ex.Message);
                }

                var apiByUser = await _apiByUserRepository.GetApiByUser(command.Api, command.UserId);
                if (apiByUser == null)
                {
                    await _unitOfWork.Repository<ApiByUser>().AddAsync(new()
                    {
                        Api = api,
                        UserId = command.UserId,
                        AccessToken = token.AccessToken,
                        RefreshToken = token.RefreshToken,
                        AccessExpiryDate = token.AccessExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.AccessExpiresIn.Value) : null,
                        RefreshExpiryDate = token.RefreshExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.RefreshExpiresIn.Value) : null
                    });
                }
                else
                {
                    apiByUser.AccessToken = token.AccessToken;
                    apiByUser.RefreshToken = token.RefreshToken;
                    apiByUser.AccessExpiryDate = token.AccessExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.AccessExpiresIn.Value) : null;
                    apiByUser.RefreshExpiryDate = token.RefreshExpiresIn.HasValue ? _dateTimeService.NowUtc.AddSeconds(token.RefreshExpiresIn.Value) : null;
                    await _unitOfWork.Repository<ApiByUser>().UpdateAsync(apiByUser);
                }

                await _unitOfWork.Commit(cancellationToken);

                return await Result<string>.SuccessAsync(command.Api, _localizer["Api Linked"]);
            }

            return await Result<string>.FailAsync(_localizer[$"{command.Api} api implementation not found!"]);
        }
    }
}
