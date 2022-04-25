using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty.Oura
{
    public class Readiness : AuditableEntity<int>
    {
        public DateTime? exported_date { get; set; }
        public string user_id { get; set; }
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
}
