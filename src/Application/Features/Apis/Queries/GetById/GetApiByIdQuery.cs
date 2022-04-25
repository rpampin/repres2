using AutoMapper;
using MediatR;
using Repres.Application.Interfaces.Repositories;
using Repres.Domain.Entities.ThirdParty;
using Repres.Shared.Wrapper;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Application.Features.Apis.Queries.GetById
{
    public class GetApiByIdQuery : IRequest<Result<GetApiByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetApiByIdQueryHandler : IRequestHandler<GetApiByIdQuery, Result<GetApiByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetApiByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetApiByIdResponse>> Handle(GetApiByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Repository<Api>().GetByIdAsync(query.Id);
            var mappedBrand = _mapper.Map<GetApiByIdResponse>(brand);
            return await Result<GetApiByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}
