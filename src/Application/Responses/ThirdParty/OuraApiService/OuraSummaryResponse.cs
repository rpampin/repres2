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
        public float? timezone { get; set; }
        public float? score { get; set; }
        public float? score_stay_active { get; set; }
        public float? score_move_every_hour { get; set; }
        public float? score_meet_daily_targets { get; set; }
        public float? score_training_frequency { get; set; }
        public float? score_training_volume { get; set; }
        public float? score_recovery_time { get; set; }
        public float? daily_movement { get; set; }
        public float? non_wear { get; set; }
        public float? rest { get; set; }
        public float? inactive { get; set; }
        public float? inactivity_alerts { get; set; }
        public float? low { get; set; }
        public float? medium { get; set; }
        public float? high { get; set; }
        public float? steps { get; set; }
        public float? cal_total { get; set; }
        public float? cal_active { get; set; }
        public float? met_min_inactive { get; set; }
        public float? met_min_low { get; set; }
        public float? met_min_medium_plus { get; set; }
        public float? met_min_medium { get; set; }
        public float? met_min_high { get; set; }
        public float? average_met { get; set; }
        public string class_5min { get; set; }
        public float[] met_1min { get; set; }
        public float? rest_mode_state { get; set; }
    }

    public class OuraReadinessSummaryResponse
    {
        public DateTime summary_date { get; set; }
        public float? period_id { get; set; }
        public float? score { get; set; }
        public float? score_previous_night { get; set; }
        public float? score_sleep_balance { get; set; }
        public float? score_previous_day { get; set; }
        public float? score_activity_balance { get; set; }
        public float? score_resting_hr { get; set; }
        public float? score_hrv_balance { get; set; }
        public float? score_recovery_index { get; set; }
        public float? score_temperature { get; set; }
        public float? rest_mode_state { get; set; }
    }

    public class OuraSleepSummaryResponse
    {
        public DateTime summary_date { get; set; }
        public float? period_id { get; set; }
        public float? is_longest { get; set; }
        public float? timezone { get; set; }
        public DateTime bedtime_start { get; set; }
        public DateTime bedtime_end { get; set; }
        public float? score { get; set; }
        public float? score_total { get; set; }
        public float? score_disturbances { get; set; }
        public float? score_efficiency { get; set; }
        public float? score_latency { get; set; }
        public float? score_rem { get; set; }
        public float? score_deep { get; set; }
        public float? score_alignment { get; set; }
        public float? total { get; set; }
        public float? duration { get; set; }
        public float? awake { get; set; }
        public float? light { get; set; }
        public float? rem { get; set; }
        public float? deep { get; set; }
        public float? onset_latency { get; set; }
        public float? restless { get; set; }
        public float? efficiency { get; set; }
        public float? midpoint_time { get; set; }
        public float? hr_lowest { get; set; }
        public float? hr_average { get; set; }
        public float? rmssd { get; set; }
        public float? breath_average { get; set; }
        public float? temperature_delta { get; set; }
        public string hypnogram_5min { get; set; }
        public int[] hr_5min { get; set; }
        public int[] rmssd_5min { get; set; }
    }
}
