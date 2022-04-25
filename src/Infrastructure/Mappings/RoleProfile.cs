using AutoMapper;
using Repres.Infrastructure.Models.Identity;
using Repres.Application.Responses.Identity;

namespace Repres.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}