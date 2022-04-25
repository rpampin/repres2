using AutoMapper;
using LazyCache;
using MediatR;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using Repres.Shared.Constants.Application;
using Repres.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Queries.GetAccess
{
    public class GetApiAccessQuery : IRequest<Result<List<GetApiAccessResponse>>>
    {
        public string UserId { get; set; }
    }

    internal class GetApiAccessQueryHandler : IRequestHandler<GetApiAccessQuery, Result<List<GetApiAccessResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IEnumerable<IApiService> _apiServices;
        private readonly IApiByUserRepository _apiByUserRepository;

        public GetApiAccessQueryHandler(
            IMapper mapper,
            IAppCache cache,
            IUnitOfWork<int> unitOfWork,
            IEnumerable<IApiService> apiServices,
            IApiByUserRepository apiByUserRepository)
        {
            _cache = cache;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _apiServices = apiServices;
            _apiByUserRepository = apiByUserRepository;
        }

        public async Task<Result<List<GetApiAccessResponse>>> Handle(GetApiAccessQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Api>>> allApis = () => _unitOfWork.Repository<Api>().GetAllAsync();
            var apiList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllApisCacheKey, allApis);
            var apiAccess = _mapper.Map<List<GetApiAccessResponse>>(apiList);

            foreach (var api in apiAccess)
            {
                var apiService = _apiServices.Where(s => s.Name == api.Name).SingleOrDefault();
                if (apiService != null)
                {
                    api.AuthUrl = await apiService.GetAuthUri();
                    api.DisplayName = api.DisplayName ??= api.Name;
                    api.IsAuthenticated = await _apiByUserRepository.IsUserAuthenticated(api.Name, request.UserId);
                }
            }

            return await Result<List<GetApiAccessResponse>>.SuccessAsync(apiAccess);
        }
    }
}
