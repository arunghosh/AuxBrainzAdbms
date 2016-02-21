using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class TweetAffinity:EntityBase
    {
        public int TweetAffinityId { get; set; }
        
        public int UserId { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

        public AffinityStatus Status { get; set; }

        public DateTime TimeStamp { get; set; }

        [ForeignKey("AlumniToKnowId")]
        public virtual AlumniToKnow Tweet { get; set; }

        public int AlumniToKnowId { get; set; }

        public TweetAffinity()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public override int EntityKey
        {
            get { return TweetAffinityId; }
        }
    }
}