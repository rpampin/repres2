using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Migrations
{
    public partial class OuraModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivitySummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    day_start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    day_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timezone = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    score_stay_active = table.Column<int>(type: "int", nullable: false),
                    score_move_every_hour = table.Column<int>(type: "int", nullable: false),
                    score_meet_daily_targets = table.Column<int>(type: "int", nullable: false),
                    score_training_frequency = table.Column<int>(type: "int", nullable: false),
                    score_training_volume = table.Column<int>(type: "int", nullable: false),
                    score_recovery_time = table.Column<int>(type: "int", nullable: false),
                    daily_movement = table.Column<int>(type: "int", nullable: false),
                    non_wear = table.Column<int>(type: "int", nullable: false),
                    rest = table.Column<int>(type: "int", nullable: false),
                    inactive = table.Column<int>(type: "int", nullable: false),
                    inactivity_alerts = table.Column<int>(type: "int", nullable: false),
                    low = table.Column<int>(type: "int", nullable: false),
                    medium = table.Column<int>(type: "int", nullable: false),
                    high = table.Column<int>(type: "int", nullable: false),
                    steps = table.Column<int>(type: "int", nullable: false),
                    cal_total = table.Column<int>(type: "int", nullable: false),
                    cal_active = table.Column<int>(type: "int", nullable: false),
                    met_min_inactive = table.Column<int>(type: "int", nullable: false),
                    met_min_low = table.Column<int>(type: "int", nullable: false),
                    met_min_medium_plus = table.Column<int>(type: "int", nullable: false),
                    met_min_medium = table.Column<int>(type: "int", nullable: false),
                    met_min_high = table.Column<int>(type: "int", nullable: false),
                    average_met = table.Column<float>(type: "real", nullable: false),
                    class_5min = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    met_1min = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rest_mode_state = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReadinessSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    score_previous_night = table.Column<int>(type: "int", nullable: false),
                    score_sleep_balance = table.Column<int>(type: "int", nullable: false),
                    score_previous_day = table.Column<int>(type: "int", nullable: false),
                    score_activity_balance = table.Column<int>(type: "int", nullable: false),
                    score_resting_hr = table.Column<int>(type: "int", nullable: false),
                    score_hrv_balance = table.Column<int>(type: "int", nullable: false),
                    score_recovery_index = table.Column<int>(type: "int", nullable: false),
                    score_temperature = table.Column<int>(type: "int", nullable: false),
                    rest_mode_state = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadinessSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SleepSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    is_longest = table.Column<int>(type: "int", nullable: false),
                    timezone = table.Column<int>(type: "int", nullable: false),
                    bedtime_start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bedtime_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    score_total = table.Column<int>(type: "int", nullable: false),
                    score_disturbances = table.Column<int>(type: "int", nullable: false),
                    score_efficiency = table.Column<int>(type: "int", nullable: false),
                    score_latency = table.Column<int>(type: "int", nullable: false),
                    score_rem = table.Column<int>(type: "int", nullable: false),
                    score_deep = table.Column<int>(type: "int", nullable: false),
                    score_alignment = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    awake = table.Column<int>(type: "int", nullable: false),
                    light = table.Column<int>(type: "int", nullable: false),
                    rem = table.Column<int>(type: "int", nullable: false),
                    deep = table.Column<int>(type: "int", nullable: false),
                    onset_latency = table.Column<int>(type: "int", nullable: false),
                    restless = table.Column<int>(type: "int", nullable: false),
                    efficiency = table.Column<int>(type: "int", nullable: false),
                    midpoint_time = table.Column<int>(type: "int", nullable: false),
                    hr_lowest = table.Column<float>(type: "real", nullable: false),
                    hr_average = table.Column<float>(type: "real", nullable: false),
                    rmssd = table.Column<int>(type: "int", nullable: false),
                    breath_average = table.Column<float>(type: "real", nullable: false),
                    temperature_delta = table.Column<float>(type: "real", nullable: false),
                    hypnogram_5min = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hr_5min = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rmssd_5min = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepSummary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitySummary");

            migrationBuilder.DropTable(
                name: "ReadinessSummary");

            migrationBuilder.DropTable(
                name: "SleepSummary");
        }
    }
}
