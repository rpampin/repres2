using System;
using System.Collections.Generic;

namespace Repres.Shared.Constants.Localization
{
    public static class UtcConstants
    {
        public static readonly KeyValuePair<string,int>[] Values = new[] {
            new KeyValuePair<string, int>("UTC−12:00", -(int)TimeSpan.Parse("12:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−11:00", -(int)TimeSpan.Parse("11:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−10:00", -(int)TimeSpan.Parse("10:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−09:30", -(int)TimeSpan.Parse("09:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC−09:00", -(int)TimeSpan.Parse("09:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−08:00", -(int)TimeSpan.Parse("08:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−07:00", -(int)TimeSpan.Parse("07:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−06:00", -(int)TimeSpan.Parse("06:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−05:00", -(int)TimeSpan.Parse("05:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−04:00", -(int)TimeSpan.Parse("04:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−03:30", -(int)TimeSpan.Parse("03:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC−03:00", -(int)TimeSpan.Parse("03:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−02:00", -(int)TimeSpan.Parse("02:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC−01:00", -(int)TimeSpan.Parse("01:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC±00:00", (int)TimeSpan.Parse("00:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+01:00", (int)TimeSpan.Parse("01:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+02:00", (int)TimeSpan.Parse("02:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+03:00", (int)TimeSpan.Parse("03:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+03:30", (int)TimeSpan.Parse("03:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+04:00", (int)TimeSpan.Parse("04:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+04:30", (int)TimeSpan.Parse("04:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+05:00", (int)TimeSpan.Parse("05:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+05:30", (int)TimeSpan.Parse("05:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+05:45", (int)TimeSpan.Parse("05:45").TotalMinutes),
            new KeyValuePair<string, int>("UTC+06:00", (int)TimeSpan.Parse("06:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+06:30", (int)TimeSpan.Parse("06:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+07:00", (int)TimeSpan.Parse("07:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+08:00", (int)TimeSpan.Parse("08:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+08:45", (int)TimeSpan.Parse("08:45").TotalMinutes),
            new KeyValuePair<string, int>("UTC+09:00", (int)TimeSpan.Parse("09:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+09:30", (int)TimeSpan.Parse("09:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+10:00", (int)TimeSpan.Parse("10:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+10:30", (int)TimeSpan.Parse("10:30").TotalMinutes),
            new KeyValuePair<string, int>("UTC+11:00", (int)TimeSpan.Parse("11:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+12:00", (int)TimeSpan.Parse("12:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+12:45", (int)TimeSpan.Parse("12:45").TotalMinutes),
            new KeyValuePair<string, int>("UTC+13:00", (int)TimeSpan.Parse("13:00").TotalMinutes),
            new KeyValuePair<string, int>("UTC+14:00", (int)TimeSpan.Parse("14:00").TotalMinutes),
        };
    }
}
