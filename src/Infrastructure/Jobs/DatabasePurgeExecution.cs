using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using Repres.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Jobs
{

    public class DatabasePurgeExecution
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IOuraRepository _ouraRepository;
        private readonly BlazorHeroContext _blazorHeroContext;
        private readonly IEnumerable<IApiService> _apiServices;

        public DatabasePurgeExecution(IUnitOfWork<int> unitOfWork,
                                      IOuraRepository ouraRepository,
                                      BlazorHeroContext blazorHeroContext,
                                      IEnumerable<IApiService> apiServices)
        {
            _ouraRepository = ouraRepository;
            _unitOfWork = unitOfWork;
            _apiServices = apiServices;
            _blazorHeroContext = blazorHeroContext;
        }

        public async Task Execute(PerformContext context, CancellationToken cancellationToken)
        {
            context.WriteLine("Commencing DatabasePurgeExecution Task");

            var apiByUsers = await _unitOfWork
                .Repository<ApiByUser>()
                .Entities
                .Include(x => x.Api)
                .ToListAsync();

            bool hadErrors = false;
            foreach (var apiUser in apiByUsers)
            {
                var apiService = _apiServices.Where(x => x.Name == apiUser.Api.Name).SingleOrDefault();
                if (apiService != null)
                {
                    try
                    {
                        context.WriteLine($"Retrieving old {apiUser.Api.Name} data from USERID {apiUser.UserId} purged from the datbase");
                        var (sleepPurge, readinessPurge, activityPurge) = await _ouraRepository.GetDataToRemove(apiUser.UserId);

                        var lastSleep = sleepPurge.OrderByDescending(x => x.summary_date).FirstOrDefault();
                        if (lastSleep != null)
                        {
                            sleepPurge.Remove(lastSleep);
                            context.WriteLine($"Process will purge {sleepPurge.Count} sleep registries");
                            _blazorHeroContext.SleepSummary.RemoveRange(sleepPurge);
                        }
                        else
                        {
                            context.WriteLine($"There're no sleep registries to purge");
                        }

                        var lastReadiness = readinessPurge.OrderByDescending(x => x.summary_date).FirstOrDefault();
                        if (lastReadiness != null)
                        {
                            readinessPurge.Remove(lastReadiness);
                            context.WriteLine($"Process will purge {readinessPurge.Count} readiness registries");
                            _blazorHeroContext.ReadinessSummary.RemoveRange(readinessPurge);
                        }
                        else
                        {
                            context.WriteLine($"There're no readiness registries to purge");
                        }

                        var lastActivity = activityPurge.OrderByDescending(x => x.summary_date).FirstOrDefault();
                        if (lastActivity != null)
                        {
                            activityPurge.Remove(lastActivity);
                            context.WriteLine($"Process will purge {activityPurge.Count} activity registries");
                            _blazorHeroContext.ActivitySummary.RemoveRange(activityPurge);
                        }
                        else
                        {
                            context.WriteLine($"There're no activity registries to purge");
                        }

                        await _blazorHeroContext.SaveChangesAsync();
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
                context.WriteLine("DatabasePurgeExecution finished with errors");
                context.ResetTextColor();
                throw new InvalidOperationException();
            }
            else
            {
                context.SetTextColor(ConsoleTextColor.Green);
                context.WriteLine("DatabasePurgeExecution finished successfully");
                context.ResetTextColor();
            }
        }
    }
}
