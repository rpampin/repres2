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
        //public float[] met_1min { get; set; }
        public int? rest_mode_state { get; set; }
    }
}
