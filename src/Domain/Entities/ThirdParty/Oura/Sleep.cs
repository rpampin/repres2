using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty.Oura
{
    public class Sleep : AuditableEntity<int>
    {
        public DateTime? exported_date { get; set; }
        public string user_id { get; set; }
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
        //public int[] hr_5min { get; set; }
        //public int[] rmssd_5min { get; set; }
    }
}
