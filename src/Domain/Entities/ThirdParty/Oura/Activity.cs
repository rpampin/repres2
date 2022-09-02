using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty.Oura
{
    public class Activity : AuditableEntity<int>
    {
        public DateTime? exported_date { get; set; }
        public string user_id { get; set; }
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
        //public float[] met_1min { get; set; }
        public float? rest_mode_state { get; set; }
    }
}
