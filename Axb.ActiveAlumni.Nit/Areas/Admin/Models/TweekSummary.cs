using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class TweekSummary
    {
        [JsonProperty("uid")]
        public int UserId { get; set; }
        [JsonProperty("name")]
        public string UserName { get; set; }
        [JsonProperty("tweek")]
        public string Tweek { get; set; }
        public string Time { get; set; }
        public string Batch { get; set; }
        public int lCnt { get; set; }
        public int dCnt { get; set; }
        public int Id { get; set; }
    }
}