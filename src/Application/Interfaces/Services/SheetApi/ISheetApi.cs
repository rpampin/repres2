using Hangfire.Server;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Services.SheetApi
{
    public interface ISheetApi
    {
        Task ExportData(string userId, PerformContext context = null);
    }
}
