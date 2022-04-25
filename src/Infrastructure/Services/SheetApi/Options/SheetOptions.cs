namespace Repres.Infrastructure.Services.SheetApi.Options
{
    public class SheetOptions
    {
        public string TemplateSpreadsheetId { get; set; }
        public string TemplateDashboardSheetName { get; set; }
        public string TemplateDataSheetName { get; set; }
        public string TemplateMonthSheetName { get; set; }
        public string MonthWeightChartName { get; set; }
        public string SleepRange { get; set; }
        public string ReadinessRange { get; set; }
        public string ActivityRange { get; set; }
        public string DateFormat { get; set; }
    }
}
