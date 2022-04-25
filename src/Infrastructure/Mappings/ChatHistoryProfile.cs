using AutoMapper;
using Repres.Application.Interfaces.Chat;
using Repres.Application.Models.Chat;
using Repres.Infrastructure.Models.Identity;

namespace Repres.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}