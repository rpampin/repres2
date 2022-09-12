using Hangfire.Server;
using Repres.Infrastructure.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Jobs
{
    public class PruneSummariesExecution
    {
        private readonly BlazorHeroContext _blazorHeroContext;

        public PruneSummariesExecution(BlazorHeroContext blazorHeroContext)
        {
            _blazorHeroContext = blazorHeroContext;
        }

        public async Task Execute(PerformContext context, CancellationToken cancellationToken)
        {
            _blazorHeroContext.RemoveRange(_blazorHeroContext.SleepSummary.ToList());
            _blazorHeroContext.RemoveRange(_blazorHeroContext.ActivitySummary.ToList());
            _blazorHeroContext.RemoveRange(_blazorHeroContext.ReadinessSummary.ToList());
            await _blazorHeroContext.SaveChangesAsync();
        }
    }
}
