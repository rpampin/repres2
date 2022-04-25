using AutoMapper;
using Repres.Application.Responses.ThirdParty.OuraApiService;
using Repres.Domain.Entities.ThirdParty.Oura;

namespace Repres.Application.Mappings
{
    public class OuraProfile : Profile
    {
        public OuraProfile()
        {
            CreateMap<OuraSleepSummaryResponse, Sleep>().ReverseMap();
            CreateMap<OuraReadinessSummaryResponse, Readiness>().ReverseMap();
            CreateMap<OuraActivitySummaryResponse, Activity>().ReverseMap();
        }
    }
}
