using System;
using System.Collections.Generic;

namespace Repres.Application.Responses.ThirdParty.OuraApiService
{
    public class OuraSummaryResponse
    {
        public List<OuraSleepSummaryResponse> Sleep { get; set; }
        public List<OuraReadinessSummaryResponse> Readiness { get; set; }
        public List<OuraActivitySummaryResponse> Activity { get; set; }
    }

    public class OuraActivitySummaryResponse
    {
        public DateTime summary_date { get; set; }
        public DateTime day_start { get; set; }
        public DateTime day_end { get; set; }
        public int? timezone { get; set; }
        public int? score { get; set; }
        public int? score_stay_active { get; set; }
        public int? score_move_every_hour { get; set; }
        public int? score_meet_daily_targets { get; set; }
        public int? score_training_frequency { get; set; }
        public int? score_training_volume { get; set; }
        public int? score_recovery_time { get; set; }
        public int? daily_movement { get; set; }
        public int? non_wear { get; set; }
        public int? rest { get; set; }
        public int? inactive { get; set; }
        public int? inactivity_alerts { get; set; }
        public int? low { get; set; }
        public int? medium { get; set; }
        public int? high { get; set; }
        public int? steps { get; set; }
        public int? cal_total { get; set; }
        public int? cal_active { get; set; }
        public int? met_min_inactive { get; set; }
        public int? met_min_low { get; set; }
        public int? met_min_medium_plus { get; set; }
        public int? met_min_medium { get; set; }
        public int? met_min_high { get; set; }
        public float average_met { get; set; }
        public string class_5min { get; set; }
        public float[] met_1min { get; set; }
        public int? rest_mode_state { get; set; }
    }

    public class OuraReadinessSummaryResponse
    {
        public DateTime summary_date { get; set; }
        public int? period_id { get; set; }
        public int? score { get; set; }
        public int? score_previous_night { get; set; }
        public int? score_sleep_balance { get; set; }
        public int? score_previous_day { get; set; }
        public int? score_activity_balance { get; set; }
        public int? score_resting_hr { get; set; }
        public int? score_hrv_balance { get; set; }
        public int? score_recovery_index { get; set; }
        public int? score_temperature { get; set; }
        public int? rest_mode_state { get; set; }
    }

    public class OuraSleepSummaryResponse
    {
        public DateTime summary_date { get; set; }
        public int? period_id { get; set; }
        public int? is_longest { get; set; }
        public int? timezone { get; set; }
        public DateTime bedtime_start { get; set; }
        public DateTime bedtime_end { get; set; }
        public int? score { get; set; }
        public int? score_total { get; set; }
        public int? score_disturbances { get; set; }
        public int? score_efficiency { get; set; }
        public int? score_latency { get; set; }
        public int? score_rem { get; set; }
        public int? score_deep { get; set; }
        public int? score_alignment { get; set; }
        public int? total { get; set; }
        public int? duration { get; set; }
        public int? awake { get; set; }
        public int? light { get; set; }
        public int? rem { get; set; }
        public int? deep { get; set; }
        public int? onset_latency { get; set; }
        public int? restless { get; set; }
        public int? efficiency { get; set; }
        public int? midpoint_time { get; set; }
        public float hr_lowest { get; set; }
        public float hr_average { get; set; }
        public int? rmssd { get; set; }
        public float breath_average { get; set; }
        public float temperature_delta { get; set; }
        public string hypnogram_5min { get; set; }
        public int[] hr_5min { get; set; }
        public int[] rmssd_5min { get; set; }
    }
}
