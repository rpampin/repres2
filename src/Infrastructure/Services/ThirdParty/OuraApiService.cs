using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Options;
using Repres.Application.Features.Apis.Commands.RefreshTokenPersist;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.SheetApi;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Application.Requests.Mail;
using Repres.Application.Responses.ThirdParty;
using Repres.Application.Responses.ThirdParty.OuraApiService;
using Repres.Domain.Entities.ThirdParty;
using Repres.Domain.Entities.ThirdParty.Oura;
using Repres.Infrastructure.Contexts;
using Repres.Infrastructure.Extensions;
using Repres.Infrastructure.Services.ThirdParty.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Services.ThirdParty
{
    public class OuraApiService : IApiService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly OuraAuthOptions _options;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IDateTimeService _dateTimeService;
        private readonly IOuraRepository _ouraRepository;
        private readonly IApiRepository _apiRepository;
        private readonly IApiByUserRepository _apiByUserRepository;
        private readonly ISheetApi _sheetApi;
        private readonly IMailService _mailService;
        private readonly BlazorHeroContext _blazorHeroContext;

        public OuraApiService(IMapper mapper,
                              IMediator mediator,
                              IUnitOfWork<int> unitOfWork,
                              IOuraRepository ouraRepository,
                              IApiRepository apiRepository,
                              IApiByUserRepository apiByUserRepository,
                              IDateTimeService dateTimeService,
                              IOptionsSnapshot<OuraAuthOptions> options,
                              ISheetApi sheetApi,
                              IMailService mailService, 
                              BlazorHeroContext blazorHeroContext)
        {
            _mapper = mapper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
            _ouraRepository = ouraRepository;
            _apiRepository = apiRepository;
            _apiByUserRepository = apiByUserRepository;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _sheetApi = sheetApi;
            _mailService = mailService;
            _blazorHeroContext = blazorHeroContext;
        }

        public string Name => "Oura";

        public async Task<string> GetAuthUri()
        {
            var apiData = await _apiRepository.GetApiByName(Name);

            var uri = _options.AuthBaseAddress + _options.AuthorizeUrl;
            uri = uri.Replace(":clientId", apiData.ClientId);
            uri = uri.Replace(":scope", apiData.Scopes);
            uri = uri.Replace(":state", Guid.NewGuid().ToString());
            uri = uri.Replace(":redirectUri", Environment.GetEnvironmentVariable("BASE_ADDRESS") + _options.RedirectUri);

            return uri;
        }

        public async Task<TokenResponse> GetAuthorizationToken(string code, string state)
        {
            // TODO: BEFORE GETTING TOKEN, CHECK FOR STATE VALUE
            var apiData = await _apiRepository.GetApiByName(Name);

            var uri = _options.AuthBaseAddress + _options.AccessTokenUrl;
            uri = uri.Replace(":clientId", apiData.ClientId);
            uri = uri.Replace(":secret", apiData.Secret);
            uri = uri.Replace(":code", code);
            uri = uri.Replace(":redirectUri", Environment.GetEnvironmentVariable("BASE_ADDRESS") + _options.RedirectUri);

            return await GetOuraTokenResponse(uri);
        }

        public async Task<TokenResponse> GetAuthorizationRefreshToken(string refreshToken)
        {
            var apiData = await _apiRepository.GetApiByName(Name);

            var uri = _options.AuthBaseAddress + _options.RefreshTokenUrl;
            uri = uri.Replace(":clientId", apiData.ClientId);
            uri = uri.Replace(":secret", apiData.Secret);
            uri = uri.Replace(":refreshToken", refreshToken);

            return await GetOuraTokenResponse(uri);
        }

        private async Task<TokenResponse> GetOuraTokenResponse(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, null);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.ToResult<OuraTokenResponse>();
                    return new() { AccessToken = result.access_token, AccessExpiresIn = result.expires_in, RefreshToken = result.refresh_token };
                }

                var error = await response.ToResult<OuraOAuthErrorResponse>();
                throw new InvalidOperationException(error.detail);
            }
        }

        async Task NotifyReconnect(ApiByUser apiByUser, string message, CancellationToken cancellationToken)
        {
            apiByUser.AccessExpiryDate = _dateTimeService.NowUtc;
            apiByUser.RefreshExpiryDate = _dateTimeService.NowUtc;
            await _unitOfWork.Repository<ApiByUser>().UpdateAsync(apiByUser);
            await _unitOfWork.Commit(cancellationToken);

            var user = await _blazorHeroContext.Users.FindAsync(apiByUser.UserId);

            // SEND EMAIL
            var mailRequest = new MailRequest
            {
                From = "mail@codewithmukesh.com",
                To = user.Email,
                Body = $"<b>Dear {user.UserName}</b><br/><br/>We couldn't authenticate your user againts {apiByUser.Api.DisplayName}.<br/><br/>This is the received error: <b>{message}</b><br/><br/>Please go into your precognito account and reconnect your {apiByUser.Api.DisplayName} account.<br/><br/><i><small>Oura Ring</small></i>",
                Subject = $"Oura Ring: {apiByUser.Api.DisplayName} Authorization Error"
            };
            await _mailService.SendAsync(mailRequest);
        }

        public async Task ExecuteScheduledJob(string userId, DateTime? start, DateTime? end, CancellationToken cancellationToken)
        {
            var user = await _blazorHeroContext.Users.FindAsync(userId);
            
            if (user.IsActive && await _apiByUserRepository.IsUserAuthenticated(Name, userId))
            {
                string token;
                var apiByUser = await _apiByUserRepository.GetApiByUser(Name, userId);

                if (apiByUser.AccessExpiryDate.HasValue && apiByUser.AccessExpiryDate.Value > _dateTimeService.NowUtc)
                    token = apiByUser.AccessToken;
                else if (!apiByUser.RefreshExpiryDate.HasValue || apiByUser.RefreshExpiryDate.Value > _dateTimeService.NowUtc)
                {
                    var newTokenResult = await _mediator.Send(new RefreshTokenPersistCommand() { UserId = userId, RefreshToken = apiByUser.RefreshToken, Api = Name });

                    if (newTokenResult.Succeeded)
                        token = newTokenResult.Data;
                    else
                    {
                        string message = String.Join(System.Environment.NewLine, newTokenResult.Messages);
                        await NotifyReconnect(apiByUser, message, cancellationToken);
                        throw new InvalidOperationException(message);
                    }
                }
                else
                {
                    string message = $"Cannot access {apiByUser.Api.DisplayName} API. Please Re-authenticate.";
                    await NotifyReconnect(apiByUser, message, cancellationToken);
                    // TRIGGER NOTIFICATION TO USER BECAUSE IT FAILED THE AUTHORIZATION PROCESS
                    throw new InvalidOperationException(message);
                }

                var lastSleepSummaryDate = await _ouraRepository.GetLastSleepSummaryDate(userId);
                var lastReadinessSummaryDate = await _ouraRepository.GetLastReadinessSummaryDate(userId);
                var lastActivitypSummaryDate = await _ouraRepository.GetLastActivitySummaryDate(userId);

                string endPrm = end.HasValue ? end.Value.ToString("yyyy-MM-dd") : _dateTimeService.NowUtc.ToString("yyyy-MM-dd");

                var initialDate = new DateTime(1970, 1, 1);
                var sleepSummary = new List<Sleep>();
                var readinessSummary = new List<Readiness>();
                var activitySummary = new List<Activity>();

                using (var client = new HttpClient())
                {
                    // SLEEP
                    var uri = _options.ApiBaseAddress + _options.SleepSummaries;
                    uri = uri.Replace(":start", start.HasValue ? start.Value.ToString("yyyy-MM-dd") : lastSleepSummaryDate.HasValue ? lastSleepSummaryDate.Value.ToString("yyyy-MM-dd") : initialDate.ToString("yyyy-MM-dd"));
                    uri = uri.Replace(":end", endPrm);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.GetAsync(uri);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.ToResult<OuraErrorResponse>();
                        throw new InvalidOperationException(error.detail);
                    }

                    var sleepData = await response.ToResult<OuraSummaryResponse>();
                    sleepSummary = _mapper.Map<List<Sleep>>(sleepData.Sleep);

                    // READINESS
                    uri = _options.ApiBaseAddress + _options.ReadinessSummaries;
                    uri = uri.Replace(":start", start.HasValue ? start.Value.ToString("yyyy-MM-dd") : lastReadinessSummaryDate.HasValue ? lastReadinessSummaryDate.Value.ToString("yyyy-MM-dd") : initialDate.ToString("yyyy-MM-dd"));
                    uri = uri.Replace(":end", endPrm);

                    response = await client.GetAsync(uri);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.ToResult<OuraErrorResponse>();
                        throw new InvalidOperationException(error.detail);
                    }

                    var readinessData = await response.ToResult<OuraSummaryResponse>();
                    readinessSummary = _mapper.Map<List<Readiness>>(readinessData.Readiness);

                    // ACTIVITY
                    uri = _options.ApiBaseAddress + _options.ActivitySummaries;
                    uri = uri.Replace(":start", start.HasValue ? start.Value.ToString("yyyy-MM-dd") : lastActivitypSummaryDate.HasValue ? lastActivitypSummaryDate.Value.ToString("yyyy-MM-dd") : initialDate.ToString("yyyy-MM-dd"));
                    uri = uri.Replace(":end", endPrm);

                    response = await client.GetAsync(uri);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.ToResult<OuraErrorResponse>();
                        throw new InvalidOperationException(error.detail);
                    }

                    var activityData = await response.ToResult<OuraSummaryResponse>();
                    activitySummary = _mapper.Map<List<Activity>>(activityData.Activity);
                }

                foreach (var sleep in sleepSummary)
                {
                    if (!await _ouraRepository.SleepSummaryExists(userId, sleep.summary_date))
                    {
                        sleep.user_id = userId;
                        await _unitOfWork.Repository<Sleep>().AddAsync(sleep);
                    }
                }
                foreach (var readiness in readinessSummary)
                {
                    if (!await _ouraRepository.ReadinessSummaryExists(userId, readiness.summary_date))
                    {
                        readiness.user_id = userId;
                        await _unitOfWork.Repository<Readiness>().AddAsync(readiness);
                    }
                }
                foreach (var activity in activitySummary)
                {
                    if (!await _ouraRepository.ActivitySummaryExists(userId, activity.summary_date))
                    {
                        activity.user_id = userId;
                        await _unitOfWork.Repository<Activity>().AddAsync(activity);
                    }
                }

                // REMOVE OLD DATA TO KEEP DATABASE MINIMAL
                var (removeSleep, removeREadiness, removeActivity) = await _ouraRepository.GetDataToRemove(userId);
                _blazorHeroContext.SleepSummary.RemoveRange(removeSleep);
                _blazorHeroContext.ReadinessSummary.RemoveRange(removeREadiness);
                _blazorHeroContext.ActivitySummary.RemoveRange(removeActivity;

                await _unitOfWork.Commit(cancellationToken);

#if DEBUG
                await _sheetApi.ExportData(userId);
#else
                BackgroundJob.Enqueue(() => _sheetApi.ExportData(userId));
#endif
            }
        }
    }
}
