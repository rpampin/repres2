using AutoMapper;
using Repres.Infrastructure.Models.Audit;
using Repres.Application.Responses.Audit;

namespace Repres.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}