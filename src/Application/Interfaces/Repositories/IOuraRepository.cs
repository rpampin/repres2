using Repres.Domain.Entities.ThirdParty.Oura;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repres.Application.Interfaces.Repositories
{
    public interface IOuraRepository
    {
        Task<DateTime?> GetLastSleepSummaryDate(string userId);
        Task<DateTime?> GetLastReadinessSummaryDate(string userId);
        Task<DateTime?> GetLastActivitySummaryDate(string userId);
        Task<bool> SleepSummaryExists(string userId, DateTime summaryDate);
        Task<bool> ReadinessSummaryExists(string userId, DateTime summaryDate);
        Task<bool> ActivitySummaryExists(string userId, DateTime summaryDate);
        Task<(List<Sleep> sleepSummary, List<Readiness> readinessSummary, List<Activity> activitySummary)> GetDataToExport(string userId);
    }
}
