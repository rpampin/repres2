using Hangfire.Console;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services.SheetApi;
using Repres.Application.Interfaces.Services.ThirdParty;
using Repres.Domain.Entities.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Jobs
{
    public class GoogleCalcExportExecution
    {
        private readonly ISheetApi _sheetApi;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IEnumerable<IApiService> _apiServices;

        public GoogleCalcExportExecution(IUnitOfWork<int> unitOfWork, IEnumerable<IApiService> apiServices, ISheetApi sheetApi)
        {
            _unitOfWork = unitOfWork;
            _apiServices = apiServices;
            _sheetApi = sheetApi;
        }

        public async Task Execute(PerformContext context, CancellationToken cancellationToken)
        {
            context.WriteLine("Commencing GoogleCalcExportExecution Task");

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
                        context.WriteLine($"Exporting data to google for USERID: {apiUser.UserId}");
                        await _sheetApi.ExportData(apiUser.UserId, context);
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
                context.WriteLine("GoogleCalcExportExecution finished with errors");
                context.ResetTextColor();
                throw new InvalidOperationException();
            }
            else
            {
                context.SetTextColor(ConsoleTextColor.Green);
                context.WriteLine("GoogleCalcExportExecution finished successfully");
                context.ResetTextColor();
            }
        }
    }
}
