using AutoMapper;
using Repres.Application.Features.Apis.Queries.GetAccess;
using Repres.Application.Features.Apis.Queries.GetAll;
using Repres.Application.Features.Apis.Queries.GetById;
using Repres.Domain.Entities.ThirdParty;

namespace Repres.Application.Mappings
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<GetAllApisResponse, Api>().ReverseMap();
            CreateMap<GetApiByIdResponse, Api>().ReverseMap();
            CreateMap<GetApiAccessResponse, Api>().ReverseMap();
        }
    }
}
