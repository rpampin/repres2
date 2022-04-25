using Repres.Application.Interfaces.Common;

namespace Repres.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}