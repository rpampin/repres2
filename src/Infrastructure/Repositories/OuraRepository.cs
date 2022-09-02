using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Domain.Entities.ThirdParty.Oura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Repositories
{
    public class OuraRepository : IOuraRepository
    {
        private readonly IRepositoryAsync<Sleep, int> _sleepRepository;
        private readonly IRepositoryAsync<Readiness, int> _readinessRepository;
        private readonly IRepositoryAsync<Activity, int> _activityRepository;

        public OuraRepository(
            IRepositoryAsync<Sleep, int> sleepRepository,
            IRepositoryAsync<Readiness, int> readinessRepository,
            IRepositoryAsync<Activity, int> activityRepository)
        {
            _sleepRepository = sleepRepository;
            _readinessRepository = readinessRepository;
            _activityRepository = activityRepository;
        }

        public async Task<DateTime?> GetLastActivitySummaryDate(string userId)
            => await _activityRepository.Entities.Where(s => s.user_id == userId).Select(s => (DateTime?)s.summary_date).MaxAsync();

        public async Task<DateTime?> GetLastReadinessSummaryDate(string userId)
            => await _readinessRepository.Entities.Where(s => s.user_id == userId).Select(s => (DateTime?)s.summary_date).MaxAsync();

        public async Task<DateTime?> GetLastSleepSummaryDate(string userId)
            => await _sleepRepository.Entities.Where(s => s.user_id == userId).Select(s => (DateTime?)s.summary_date).MaxAsync();

        public async Task<bool> ActivitySummaryExists(string userId, DateTime summaryDate)
            => await _activityRepository.Entities.AnyAsync(x => x.user_id == userId && x.summary_date == summaryDate);

        public async Task<bool> ReadinessSummaryExists(string userId, DateTime summaryDate)
            => await _readinessRepository.Entities.AnyAsync(x => x.user_id == userId && x.summary_date == summaryDate);

        public async Task<bool> SleepSummaryExists(string userId, DateTime summaryDate)
            => await _sleepRepository.Entities.AnyAsync(x => x.user_id == userId && x.summary_date == summaryDate);

        public async Task<(List<Sleep> sleepSummary, List<Readiness> readinessSummary, List<Activity> activitySummary)> GetDataToExport(string userId)
        {
            var rv = (new List<Sleep>(), new List<Readiness>(), new List<Activity>());

            rv.Item1 = await _sleepRepository.Entities.Where(e => e.exported_date == null).OrderBy(e => e.summary_date).ToListAsync();
            rv.Item2 = await _readinessRepository.Entities.Where(e => e.exported_date == null).OrderBy(e => e.summary_date).ToListAsync();
            rv.Item3 = await _activityRepository.Entities.Where(e => e.exported_date == null).OrderBy(e => e.summary_date).ToListAsync();

            return rv;
        }

        public async Task<(List<Sleep> sleepSummary, List<Readiness> readinessSummary, List<Activity> activitySummary)> GetDataToRemove(string userId)
        {
            var rv = (new List<Sleep>(), new List<Readiness>(), new List<Activity>());

            rv.Item1 = await _sleepRepository.Entities.Where(e => e.exported_date != null).OrderBy(e => e.summary_date).ToListAsync() ?? new List<Sleep>();
            rv.Item2 = await _readinessRepository.Entities.Where(e => e.exported_date != null).OrderBy(e => e.summary_date).ToListAsync() ?? new List<Readiness>();
            rv.Item3 = await _activityRepository.Entities.Where(e => e.exported_date != null).OrderBy(e => e.summary_date).ToListAsync() ?? new List<Activity>();

            return rv;
        }
    }
}
