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
                    try
                    {
                        await _sheetApi.ExportData(apiUser.UserId);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
