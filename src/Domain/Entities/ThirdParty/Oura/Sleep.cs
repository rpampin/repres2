using Repres.Domain.Contracts;
using System;

namespace Repres.Domain.Entities.ThirdParty.Oura
{
    public class Sleep : AuditableEntity<int>
    {
        public DateTime? exported_date { get; set; }
        public string user_id { get; set; }
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
        //public int[] hr_5min { get; set; }
        //public int[] rmssd_5min { get; set; }
    }
}
