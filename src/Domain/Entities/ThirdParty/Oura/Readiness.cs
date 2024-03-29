﻿using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty.Oura
{
    public class Readiness : AuditableEntity<int>
    {
        public DateTime? exported_date { get; set; }
        public string user_id { get; set; }
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
}
