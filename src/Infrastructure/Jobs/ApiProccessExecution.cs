using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using System;
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

        public async Task Execute(PerformContext context, CancellationToken cancellationToken)
        {
            context.WriteLine("Commencing ApiProccessExecution Task");

            bool hadErrors = false;
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
                    try
                    {
                        context.WriteLine($"Getting data from {apiUser.Api.Name} for USERID: {apiUser.UserId}");
                        await apiService.ExecuteScheduledJob(apiUser.UserId, context, null, _dateTimeService.NowUtc, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        context.SetTextColor(ConsoleTextColor.Red);
                        context.WriteLine($"USERID: {apiUser.UserId} process throwed an error.{Environment.NewLine}{ex.Message}");
                        context.ResetTextColor();
                        hadErrors = true;
                    }
                }
            }

            if (hadErrors)
            {
                context.SetTextColor(ConsoleTextColor.Red);
                context.WriteLine("ApiProccessExecution finished with errors");
                context.ResetTextColor();
                throw new InvalidOperationException();
            }
        }
    }
}
