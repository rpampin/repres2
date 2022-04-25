using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Jobs
{
    public class ApiProccessExecution
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IDateTimeService _dateTimeService;
        private readonly IEnumerable<IApiService> _apiServices;

        public ApiProccessExecution(IUnitOfWork<int> unitOfWork, IEnumerable<IApiService> apiServices, IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork;
            _apiServices = apiServices;
            _dateTimeService = dateTimeService;
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var apiByUsers = await _unitOfWork
                .Repository<ApiByUser>()
                .Entities
                .Include(x => x.Api)
                .ToListAsync();
            foreach (var apiUser in apiByUsers)
            {
                var apiService = _apiServices.Where(x => x.Name == apiUser.Api.Name).SingleOrDefault();
                if (apiService != null)
                {
                    await apiService.ExecuteScheduledJob(apiUser.UserId, null, _dateTimeService.NowUtc, cancellationToken);
                }
            }
        }
    }
}
