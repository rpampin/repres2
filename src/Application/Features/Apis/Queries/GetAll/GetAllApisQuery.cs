using AutoMapper;
using LazyCache;
using MediatR;
using Repres.Application.Interfaces.Repositories;
using Repres.Domain.Entities.ThirdParty;
using Repres.Shared.Constants.Application;
using Repres.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Queries.GetAll
{
    public class GetAllApisQuery : IRequest<Result<List<GetAllApisResponse>>> { }

    internal class GetAllApisQueryHandler : IRequestHandler<GetAllApisQuery, Result<List<GetAllApisResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllApisQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllApisResponse>>> Handle(GetAllApisQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Api>>> getAllApis = () => _unitOfWork.Repository<Api>().GetAllAsync();
            var apiList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllApisCacheKey, getAllApis);
            var mappedApis = _mapper.Map<List<GetAllApisResponse>>(apiList);
            return await Result<List<GetAllApisResponse>>.SuccessAsync(mappedApis);
        }
    }
}
