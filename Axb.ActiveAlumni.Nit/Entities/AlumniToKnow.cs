using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class AlumniToKnow: AlumniArticleBase
    {
        [Key]
        public int AlumniToKnowId { get; set; }

        [StringLength(1048)]
        public override string About { get; set; }

        public virtual List<TweetAffinity> Affinities { get; set; }

        [NotMapped]
        public int AgreeCnt
        {
            get
            {
                if (Affinities == null) return 0;
                return Affinities.Where(a => a.Status == AffinityStatus.Agree).Count();
            }
        }

        [NotMapped]
        public int DisagreeCnt
        {
            get
            {
                if (Affinities == null) return 0;
                return Affinities.Where(a => a.Status == AffinityStatus.Disagree).Count();
            }
        }

        public AlumniToKnow()
            :base()
        {
        }

        public override int EntityKey
        {
            get { return AlumniToKnowId; }
        }
    }
}