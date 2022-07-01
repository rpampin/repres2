using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Repres.Application.Interfaces.Repositories;
using Repres.Application.Interfaces.Services;
using Repres.Application.Interfaces.Services.SheetApi;
using Repres.Domain.Entities.ThirdParty.Oura;
using Repres.Infrastructure.Helper;
using Repres.Infrastructure.Models.Identity;
using Repres.Infrastructure.Services.SheetApi.Options;
using Repres.Shared.Constants.Role;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repres.Infrastructure.Services.SheetApi
{
    public class SheetApi : ISheetApi
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IOuraRepository _ouraRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly DriveService _googleDrive;
        private readonly SpreadsheetsResource.ValuesResource _googleSheetValues;
        private readonly SpreadsheetsResource _googleSpreadSheet;
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly SheetOptions _options;

        public SheetApi(IOuraRepository ouraRepository,
                        GoogleSheetsHelper googleSheetsHelper,
                        IOptionsSnapshot<SheetOptions> options,
                        UserManager<BlazorHeroUser> userManager,
                        IUnitOfWork<int> unitOfWork,
                        IDateTimeService dateTimeService)
        {
            _ouraRepository = ouraRepository;
            _googleDrive = googleSheetsHelper.DriveService;
            _googleSheetValues = googleSheetsHelper.SheetService.Spreadsheets.Values;
            _googleSpreadSheet = googleSheetsHelper.SheetService.Spreadsheets;
            _userManager = userManager;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
        }

        string GetDateNameString(DateTime date, string userLanguage)
            => date.ToString(_options.DateFormat, new CultureInfo(userLanguage)).ToUpper();

        public async Task ExportData(string userId)
        {
            // CHECK IF THERE'S DATA TO BE EXPORTED
            var date = _dateTimeService.NowUtc;
            
            var exportData = await _ouraRepository.GetDataToExport(userId);

            if (exportData.sleepSummary.Count > 0 || exportData.readinessSummary.Count > 0 || exportData.activitySummary.Count > 0)
            {
                Spreadsheet spreadSheet;
                var user = await _userManager.FindByIdAsync(userId);

#if DEBUG
                // ONLY FOR DEV PURPOSE
                var delete = false;
                if (delete && !string.IsNullOrWhiteSpace(user.OuraSheetId))
                {
                    _googleDrive.Files.Delete(user.OuraSheetId).Execute();
                    user.OuraSheetId = null;
                    user.OuraSheetUrl = null;
                    await _userManager.UpdateAsync(user);
                    user = await _userManager.FindByIdAsync(userId);
                }
#endif
                if (string.IsNullOrWhiteSpace(user.OuraSheetId))
                {
                    // GET ADMIN DATA
                    var administrators = await _userManager.GetUsersInRoleAsync(RoleConstants.AdministratorRole);
                    var sharingUsers = administrators.Union(new List<BlazorHeroUser> { user });
                    
                    // CREATE NEW SHEET
                    var spreadSheetCreation = _googleSpreadSheet.Create(new Spreadsheet()
                    {
                        Properties = new SpreadsheetProperties()
                        {
                            Title = $"{user.FirstName} {user.LastName}; Body Dashboard"
                        },
                        Sheets = null
                    });

                    spreadSheet = await spreadSheetCreation.ExecuteAsync();

                    // ASSIGN SPREADSHEET TO USER
                    user.OuraSheetId = spreadSheet.SpreadsheetId;
                    user.OuraSheetUrl = spreadSheet.SpreadsheetUrl;

                    await _userManager.UpdateAsync(user);

                    // PERMISSION TO SPREADSHEET
                    foreach (var usr in sharingUsers)
                    {
                        Permission perms = new Permission();
                        perms.Role = "writer";
                        perms.Type = "user";
                        perms.EmailAddress = usr.GmailAccount ?? usr.Email;
                        _googleDrive.Permissions.Create(perms, spreadSheet.SpreadsheetId).Execute();
                    }

                    #region COMMENTED SECTION FOR DASHBOARD SHEET
                    // USER WRITE PERMISSION TO SPREADSHEET
                    //perms.EmailAddress = user.GmailAccount ?? user.Email;
                    //_googleDrive.Permissions.Create(perms, spreadSheet.SpreadsheetId).Execute();

                    // GET TEMPLATE SPREADSHEET
                    //var templateSpreadSheet = _googleSpreadSheet.Get(_options.TemplateSpreadsheetId).Execute();

                    // CLONE DASHBOARD SHEET
                    //CopySheetToAnotherSpreadsheetRequest copySheetRequest = new CopySheetToAnotherSpreadsheetRequest();
                    //copySheetRequest.DestinationSpreadsheetId = spreadSheet.SpreadsheetId;

                    // TODO: NO DASHBOARD
                    //int? dashboardSheetId = templateSpreadSheet.Sheets.Where(s => s.Properties.Title == _options.TemplateDashboardSheetName).Select(s => s.Properties.SheetId).Single();
                    //await _googleSpreadSheet.Sheets.CopyTo(copySheetRequest, _options.TemplateSpreadsheetId, dashboardSheetId.Value).ExecuteAsync();
                    //spreadSheet = _googleSpreadSheet.Get(spreadSheet.SpreadsheetId).Execute();

                    // RENAME CLONED DASHBOARD SHEET
                    //BatchUpdateSpreadsheetRequest batchUpdateTemplateSheetName = new BatchUpdateSpreadsheetRequest();
                    //batchUpdateTemplateSheetName.Requests = new List<Request>();

                    // TODO: NO DASHBOARD
                    //dashboardSheetId = spreadSheet.Sheets.Where(s => s.Properties.Title.Contains(_options.TemplateDashboardSheetName)).Select(s => s.Properties.SheetId).Single();

                    //batchUpdateTemplateSheetName.Requests.Add(new Request()
                    //{
                    //    UpdateSheetProperties = new UpdateSheetPropertiesRequest
                    //    {
                    //        Properties = new SheetProperties()
                    //        {
                    //            Title = _options.TemplateDashboardSheetName,
                    //            SheetId = dashboardSheetId
                    //        },
                    //        Fields = "Title"
                    //    }
                    //});

                    //batchUpdateTemplateSheetName.Requests.Add(new Request()
                    //{
                    //    AddProtectedRange = new AddProtectedRangeRequest
                    //    {
                    //        ProtectedRange = new ProtectedRange
                    //        {
                    //            Range = new GridRange
                    //            {
                    //                SheetId = dashboardSheetId
                    //            },
                    //            Editors = new Editors
                    //            {
                    //                Users = new List<string>() { adminEmail }
                    //            }
                    //        }
                    //    }
                    //});

                    // HIDE DEFAULT CREATED SHEET 1
                    //batchUpdateTemplateSheetName.Requests.Add(new Request()
                    //{
                    //    UpdateSheetProperties = new UpdateSheetPropertiesRequest
                    //    {
                    //        Properties = new SheetProperties()
                    //        {
                    //            SheetId = spreadSheet.Sheets.First().Properties.SheetId,
                    //            Hidden = true
                    //        },
                    //        Fields = "Hidden"
                    //    }
                    //});

                    //batchUpdateTemplateSheetName.Requests.Add(new Request()
                    //{
                    //    AddProtectedRange = new AddProtectedRangeRequest
                    //    {
                    //        ProtectedRange = new ProtectedRange
                    //        {
                    //            Range = new GridRange
                    //            {
                    //                SheetId = spreadSheet.Sheets.First().Properties.SheetId
                    //            },
                    //            Editors = new Editors
                    //            {
                    //                Users = new List<string>() { adminEmail }
                    //            }
                    //        }
                    //    }
                    //});

                    //await _googleSpreadSheet.BatchUpdate(batchUpdateTemplateSheetName, spreadSheet.SpreadsheetId).ExecuteAsync();

                    //spreadSheet = _googleSpreadSheet.Get(spreadSheet.SpreadsheetId).Execute();
                    #endregion
                }
                else
                {
                    spreadSheet = await _googleSpreadSheet.Get(user.OuraSheetId).ExecuteAsync();
                }

                // ADD MONTHLY SHEET
                var newMonthsToExport = exportData.sleepSummary
                    //.OrderByDescending(s => s.summary_date)
                    .Select(s => GetDateNameString(s.summary_date.ToUniversalTime().AddDays(1), user.Language))
                    .Where(t => !spreadSheet.Sheets.Any(s => s.Properties.Title == t))
                    .Distinct()
                    .ToList();

                if (newMonthsToExport.Count > 0)
                {
                    // GET TEMPLATE SPREADSHEET
                    var templateSpreadSheet = _googleSpreadSheet.Get(_options.TemplateSpreadsheetId).Execute();

                    // GET ADMIN DATA FOR SHEET SECURITY
                    var administrators = await _userManager.GetUsersInRoleAsync(RoleConstants.AdministratorRole);

                    foreach (var month in newMonthsToExport)
                    {
                        // CLONE DATA & DASHBOARD SHEETS
                        CopySheetToAnotherSpreadsheetRequest copySheetRequest = new CopySheetToAnotherSpreadsheetRequest();
                        copySheetRequest.DestinationSpreadsheetId = spreadSheet.SpreadsheetId;

                        int? templateDataSheetId = templateSpreadSheet.Sheets.Where(s => s.Properties.Title == _options.TemplateDataSheetName).Select(s => s.Properties.SheetId).Single();
                        int? templateDashboardSheetId = templateSpreadSheet.Sheets.Where(s => s.Properties.Title == _options.TemplateMonthSheetName).Select(s => s.Properties.SheetId).Single();
                        await _googleSpreadSheet.Sheets.CopyTo(copySheetRequest, _options.TemplateSpreadsheetId, templateDataSheetId.Value).ExecuteAsync();
                        await _googleSpreadSheet.Sheets.CopyTo(copySheetRequest, _options.TemplateSpreadsheetId, templateDashboardSheetId.Value).ExecuteAsync();

                        spreadSheet = _googleSpreadSheet.Get(spreadSheet.SpreadsheetId).Execute();

                        // RENAME CLONED TEMPLATE SHEETS
                        BatchUpdateSpreadsheetRequest batchUpdateTemplateSheetName = new BatchUpdateSpreadsheetRequest();
                        batchUpdateTemplateSheetName.Requests = new List<Request>();

                        int? dataSheetId = spreadSheet.Sheets.Where(s => s.Properties.Title.Contains(_options.TemplateDataSheetName)).Select(s => s.Properties.SheetId).Single();
                        int? monthSheetId = spreadSheet.Sheets.Where(s => s.Properties.Title.Contains(_options.TemplateMonthSheetName)).Select(s => s.Properties.SheetId).Single();

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            UpdateSheetProperties = new UpdateSheetPropertiesRequest
                            {
                                Properties = new SheetProperties()
                                {
                                    Title = $"DATA {month}",
                                    SheetId = dataSheetId,
                                    Hidden = true,
                                    Index = 1
                                },
                                Fields = "Title,Hidden,Index"
                            }
                        });

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            AddProtectedRange = new AddProtectedRangeRequest
                            {
                                ProtectedRange = new ProtectedRange
                                {
                                    Range = new GridRange
                                    {
                                        SheetId = dataSheetId
                                    },
                                    Editors = new Editors
                                    {
                                        Users = administrators.Select(a => a.GmailAccount ?? a.Email).ToList()
                                    }
                                }
                            }
                        });

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            UpdateSheetProperties = new UpdateSheetPropertiesRequest
                            {
                                Properties = new SheetProperties()
                                {
                                    Title = month,
                                    SheetId = monthSheetId,
                                    Index = 0
                                },
                                Fields = "Title,Index"
                            }
                        });

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            AddProtectedRange = new AddProtectedRangeRequest
                            {
                                ProtectedRange = new ProtectedRange
                                {
                                    Range = new GridRange
                                    {
                                        SheetId = monthSheetId,
                                        StartColumnIndex = 0,
                                        StartRowIndex = 0,
                                        EndColumnIndex = 31,
                                        EndRowIndex = 37
                                    },
                                    Editors = new Editors
                                    {
                                        Users = administrators.Select(a => a.GmailAccount ?? a.Email).ToList()
                                    }
                                }
                            }
                        });

                        await _googleSpreadSheet.BatchUpdate(batchUpdateTemplateSheetName, spreadSheet.SpreadsheetId).ExecuteAsync();

                        spreadSheet = await UpdateDashboardSheet(month, $"DATA {month}", spreadSheet.SpreadsheetId);
                    }

                    // IF SHEET1 IS NOT HIDDEN, HIDE IT AFTER CREATING OTHER SHEETS
                    var sheet1 = spreadSheet.Sheets.Where(s => s.Properties.Title == "Sheet1").SingleOrDefault();
                    if (sheet1 != null && sheet1.Properties.Hidden != true)
                    {
                        BatchUpdateSpreadsheetRequest batchUpdateTemplateSheetName = new BatchUpdateSpreadsheetRequest();
                        batchUpdateTemplateSheetName.Requests = new List<Request>();

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            UpdateSheetProperties = new UpdateSheetPropertiesRequest
                            {
                                Properties = new SheetProperties()
                                {
                                    SheetId = sheet1.Properties.SheetId,
                                    Hidden = true
                                },
                                Fields = "Hidden"
                            }
                        });

                        batchUpdateTemplateSheetName.Requests.Add(new Request()
                        {
                            AddProtectedRange = new AddProtectedRangeRequest
                            {
                                ProtectedRange = new ProtectedRange
                                {
                                    Range = new GridRange
                                    {
                                        SheetId = sheet1.Properties.SheetId
                                    },
                                    Editors = new Editors
                                    {
                                        Users = administrators.Select(a => a.GmailAccount ?? a.Email).ToList()
                                    }
                                }
                            }
                        });

                        await _googleSpreadSheet.BatchUpdate(batchUpdateTemplateSheetName, spreadSheet.SpreadsheetId).ExecuteAsync();

                        spreadSheet = _googleSpreadSheet.Get(spreadSheet.SpreadsheetId).Execute();
                    }
                }

                /// DATA EXPORT
                BatchUpdateValuesRequest batchUpdateValuesRequest = new BatchUpdateValuesRequest();
                batchUpdateValuesRequest.Data = new List<ValueRange>();

                // GET USER TIME ZONE
                var userTimeZone = TimeZoneInfo.GetSystemTimeZones().Where(tz => tz.Id == user.TimeZoneId).Single();

                ISet<string> updatedSheet = new HashSet<string>();
                var sleepRangeSplit = _options.SleepRange.Split(':');
                var sleepRange = (sleepRangeSplit[0], sleepRangeSplit[1]);
                foreach (var sleep in exportData.sleepSummary)
                {
                    var currentDate = sleep.summary_date.ToUniversalTime().AddDays(1);
                    var row = currentDate.Day + 1;
                    IList<object> list = new List<object>();

                    list.Add(currentDate.ToString("yyyy-MM-dd")); //Date
                    list.Add(sleep.score); //Sleep Score
                    list.Add(sleep.score_total); //Total Sleep Score
                    list.Add(sleep.score_rem); //REM Sleep Score
                    list.Add(sleep.score_deep); //Deep Sleep Score
                    list.Add(sleep.score_efficiency); //Sleep Efficiency Score
                    list.Add(sleep.score_disturbances); //Restfulness Score
                    list.Add(sleep.score_latency); //Sleep Latency Score 
                    list.Add(null); //Sleep Timin Score // TODO: NOT IN API?
                    list.Add(sleep.total); //Total Sleep Duration
                    list.Add(sleep.duration); //Total Bedtime
                    list.Add(sleep.awake); //Awake Time
                    list.Add(sleep.rem); //REM Sleep Duration
                    list.Add(sleep.light); //Light Sleep Duration
                    list.Add(sleep.deep); //Deep Sleep Duration
                    list.Add(sleep.restless); //Restless Sleep
                    list.Add(sleep.efficiency); //Sleep Efficiency
                    list.Add(sleep.onset_latency); //Sleep Latency
                    list.Add(null); //Sleep Timing // TODO: NOT IN API?
                    // CHANGE TIMEZONE OF BEDTIME START/END
                    var bedtime_start = sleep.bedtime_start.ToUniversalTime().Add(userTimeZone.BaseUtcOffset);
                    var bedtime_end = sleep.bedtime_end.ToUniversalTime().Add(userTimeZone.BaseUtcOffset);
                    list.Add(bedtime_start.ToString("yyyy-MM-dd HH:mm")); //Bedtime Start
                    list.Add(bedtime_end.ToString("yyyy-MM-dd HH:mm")); //Bedtime End
                    list.Add(sleep.hr_average); //Average Resting Heart Rate
                    list.Add(sleep.hr_lowest); //Lowest Resting Heart Rate
                    list.Add(sleep.rmssd); //Average HRV
                    list.Add(null); //Temperature Deviation (°C) // TODO: NOT IN API?
                    list.Add(sleep.temperature_delta); //Temperature Trend Deviation
                    list.Add(sleep.breath_average); //Respiratory Rate

                    IList<IList<object>> data = new List<IList<object>>();
                    data.Add(list);
                    string sheetName = GetDateNameString(currentDate, user.Language);
                    ValueRange valueRange = new ValueRange() { Range = $"DATA {sheetName}!{sleepRange.Item1}{row}:{sleepRange.Item2}{row}", Values = data };
                    batchUpdateValuesRequest.Data.Add(valueRange);

                    sleep.exported_date = date;
                    await _unitOfWork.Repository<Sleep>().UpdateAsync(sleep);

                    updatedSheet.Add(sheetName);
                }

                var activityRangeSplit = _options.ActivityRange.Split(':');
                var activityRange = (activityRangeSplit[0], activityRangeSplit[1]);
                foreach (var activity in exportData.activitySummary)
                {
                    var currentDate = activity.summary_date.ToUniversalTime().AddDays(1);
                    var row = currentDate.Day + 1;
                    IList<object> list = new List<object>();

                    list.Add(activity.score); //Activity Score
                    list.Add(activity.score_stay_active); //Stay Active Score
                    list.Add(activity.score_move_every_hour); //Move Every Hour Score
                    list.Add(activity.score_meet_daily_targets); //Meet Daily Targets Score
                    list.Add(activity.score_training_frequency); //Training Frequency Score
                    list.Add(activity.score_training_volume); //Training Volume Score
                    list.Add(activity.cal_active); //Activity Burn
                    list.Add(activity.cal_total); //Total Burn
                    list.Add(activity.steps); //Steps
                    list.Add(activity.daily_movement); //Equivalent Walking Distance
                    list.Add(activity.inactive); //Inactive Time
                    list.Add(activity.rest); //Rest Time
                    list.Add(activity.low); //Low Activity Time
                    list.Add(activity.medium); //Medium Activity Time
                    list.Add(activity.high); //High Activity Time
                    list.Add(activity.non_wear); //Non-wear Time
                    list.Add(activity.average_met); //Average MET
                    list.Add(activity.score_move_every_hour); //Long Periods of Inactivity

                    IList<IList<object>> data = new List<IList<object>>();
                    data.Add(list);
                    string sheetName = GetDateNameString(currentDate, user.Language);
                    ValueRange valueRange = new ValueRange() { Range = $"DATA {sheetName}!{activityRange.Item1}{row}:{activityRange.Item2}{row}", Values = data };
                    batchUpdateValuesRequest.Data.Add(valueRange);

                    activity.exported_date = date;
                    await _unitOfWork.Repository<Activity>().UpdateAsync(activity);

                    updatedSheet.Add(sheetName);
                }

                var readinessRangeSplit = _options.ReadinessRange.Split(':');
                var readinessRange = (readinessRangeSplit[0], readinessRangeSplit[1]);
                foreach (var readiness in exportData.readinessSummary)
                {
                    var currentDate = readiness.summary_date.ToUniversalTime().AddDays(1);
                    var row = currentDate.Day + 1;
                    IList<object> list = new List<object>();

                    list.Add(readiness.score); //Readiness Score
                    list.Add(null); //Previous Night Score
                    list.Add(null); //Sleep Balance Score
                    list.Add(null); //Previous Day Activity Score
                    list.Add(null); //Activity Balance Score
                    list.Add(readiness.score_temperature); //Temperature Score
                    list.Add(null); //Resting Heart Rate Score
                    list.Add(null); //HRV Balance Score
                    list.Add(null); //Recovery Index Score

                    IList<IList<object>> data = new List<IList<object>>();
                    data.Add(list);
                    string sheetName = GetDateNameString(currentDate, user.Language);
                    ValueRange valueRange = new ValueRange() { Range = $"DATA {sheetName}!{readinessRange.Item1}{row}:{readinessRange.Item2}{row}", Values = data };
                    batchUpdateValuesRequest.Data.Add(valueRange);

                    readiness.exported_date = date;
                    await _unitOfWork.Repository<Readiness>().UpdateAsync(readiness);

                    updatedSheet.Add(sheetName);
                }

                batchUpdateValuesRequest.ValueInputOption = "USER_ENTERED";
                if (batchUpdateValuesRequest.Data.Count > 0)
                    await _googleSheetValues.BatchUpdate(batchUpdateValuesRequest, spreadSheet.SpreadsheetId).ExecuteAsync();

                spreadSheet = await _googleSpreadSheet.Get(spreadSheet.SpreadsheetId).ExecuteAsync();

                // UPDATE WEIGHT CHART Y AXIS RANGE
                if (updatedSheet.Count > 0)
                {
                    BatchUpdateSpreadsheetRequest batchUpdateTemplateSheetName = new BatchUpdateSpreadsheetRequest();
                    batchUpdateTemplateSheetName.Requests = new List<Request>();

                    bool updateCharts = false;
                    foreach (var sheetName in updatedSheet)
                    {
                        var sheet = spreadSheet.Sheets.Where(s => s.Properties.Title == sheetName).Single();
                        var weightChart = sheet.Charts.Where(c => c.Spec.Title == _options.MonthWeightChartName).SingleOrDefault();
                        if (weightChart != null)
                        {
                            var weightRows = await _googleSheetValues.Get(spreadSheet.SpreadsheetId, $"{sheetName}!AG6:AG36").ExecuteAsync();
                            var weightValues = weightRows.Values != null ? weightRows.Values.Select(v => Convert.ToDouble(v[0].ToString())).ToList() : new List<double>();

                            var weightChartSpec = weightChart.Spec;
                            for (var i = 0; i < weightChartSpec.BasicChart.Axis.Count; i++)
                            {
                                if (weightChartSpec.BasicChart.Axis[i].Position == "LEFT_AXIS")
                                {
                                    weightChartSpec.BasicChart.Axis[i].ViewWindowOptions = new ChartAxisViewWindowOptions
                                    {
                                        ViewWindowMax = weightValues.Any() ? weightValues.Max() + 5 : 100,
                                        ViewWindowMin = weightValues.Any() ? weightValues.Min() - 5 : 0
                                    };
                                    updateCharts = true;
                                    break;
                                }
                            }
                            
                            batchUpdateTemplateSheetName.Requests.Add(new Request()
                            {
                                UpdateChartSpec = new UpdateChartSpecRequest
                                {
                                    ChartId = weightChart.ChartId,
                                    Spec = weightChartSpec
                                }
                            });
                        }
                    }

                    if (batchUpdateTemplateSheetName.Requests.Count > 0 && updateCharts)
                        await _googleSpreadSheet.BatchUpdate(batchUpdateTemplateSheetName, spreadSheet.SpreadsheetId).ExecuteAsync();
                }

                await _unitOfWork.Commit(CancellationToken.None);
            }
        }

        async Task<Spreadsheet> UpdateDashboardSheet(string monthSheetName, string dataSheetName, string spreadSheetId)
        {
            BatchUpdateValuesRequest batchUpdateValuesRequest = new BatchUpdateValuesRequest();
            batchUpdateValuesRequest.Data = new List<ValueRange>();

            int day = 1;
            int dataRow = 2;
            int dashboardRow = 6;
            
            while (day <= 31)
            {
                IList<IList<object>> data = new List<IList<object>>();
                IList<object> values = new List<object>();
                values.Add($"='{dataSheetName}'!A{dataRow}");
                values.Add($"='{dataSheetName}'!T{dataRow}");
                values.Add($"='{dataSheetName}'!U{dataRow}");
                values.Add($"=IF('{dataSheetName}'!J{dataRow}/86400;'{dataSheetName}'!J{dataRow}/86400;\"\")");
                values.Add($"=IF('{dataSheetName}'!Q{dataRow}/100;'{dataSheetName}'!Q{dataRow}/100;\"\")");
                values.Add($"='{dataSheetName}'!G{dataRow}");
                values.Add($"='{dataSheetName}'!H{dataRow}");
                values.Add($"=IF('{dataSheetName}'!R{dataRow}/60;'{dataSheetName}'!R{dataRow}/60;\"\")");
                values.Add($"=IF('{dataSheetName}'!L{dataRow}/86400;'{dataSheetName}'!L{dataRow}/86400;\"\")");
                values.Add($"=IF('{dataSheetName}'!N{dataRow}/86400;'{dataSheetName}'!N{dataRow}/86400;\"\")");
                values.Add($"=IF('{dataSheetName}'!M{dataRow}/86400;'{dataSheetName}'!M{dataRow}/86400;\"\")");
                values.Add($"=IF('{dataSheetName}'!O{dataRow}/86400;'{dataSheetName}'!O{dataRow}/86400;\"\")");
                values.Add($"='{dataSheetName}'!B{dataRow}");
                values.Add($"='{dataSheetName}'!V{dataRow}");
                values.Add($"=IF('{dataSheetName}'!W{dataRow};'{dataSheetName}'!W{dataRow};\"\")");
                values.Add($"='{dataSheetName}'!X{dataRow}");
                values.Add($"=IF('{dataSheetName}'!AY{dataRow}/100;'{dataSheetName}'!AY{dataRow}/100;\"\")");
                values.Add($"='{dataSheetName}'!AA{dataRow}");
                values.Add($"='{dataSheetName}'!AT{dataRow}");
                values.Add($"='{dataSheetName}'!AI{dataRow}");
                values.Add($"='{dataSheetName}'!AH{dataRow}");
                values.Add($"=IF('{dataSheetName}'!AN{dataRow}/1440,'{dataSheetName}'!AN{dataRow}/1440,\"\")");
                values.Add($"=IF('{dataSheetName}'!AO{dataRow}/1440,'{dataSheetName}'!AO{dataRow}/1440,\"\")");
                values.Add($"=IF('{dataSheetName}'!AP{dataRow}/1440,'{dataSheetName}'!AP{dataRow}/1440,\"\")");
                values.Add($"='{dataSheetName}'!AR{dataRow}");
                values.Add($"='{dataSheetName}'!AJ{dataRow}");
                values.Add($"=IF('{dataSheetName}'!AK{dataRow}/1000,'{dataSheetName}'!AK{dataRow}/1000,\"\")");
                values.Add($"=IF('{dataSheetName}'!AQ{dataRow},'{dataSheetName}'!AQ{dataRow}/1440,\"\")");
                values.Add($"='{dataSheetName}'!AB{dataRow}");

                data.Add(values);
                var valueRange = new ValueRange() { Range = $"{monthSheetName}!B{dashboardRow}:AE{dashboardRow}", Values = data };
                batchUpdateValuesRequest.Data.Add(valueRange);

                day++;
                dataRow++;
                dashboardRow++;
            }

            batchUpdateValuesRequest.ValueInputOption = "USER_ENTERED";
            await _googleSheetValues.BatchUpdate(batchUpdateValuesRequest, spreadSheetId).ExecuteAsync();

            return await _googleSpreadSheet.Get(spreadSheetId).ExecuteAsync();
        }
    }
}


