using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class DiscussionComment:ThreadItemBase
    {
        [Key]
        public int DiscussionCommentId { get; set; }

        public int DiscussionId { get; set; }

        [ForeignKey("DiscussionId")]
        public virtual Discussion Discussion { get; set; }

        [NotMapped]
        public int AgreeCnt 
        {
            get
            {
                return Affinities.Where(a => a.Status == AffinityStatus.Agree).Count();
            }
        }

        [NotMapped]
        public int DisagreeCnt
        {
            get
            {
                return Affinities.Where(a => a.Status == AffinityStatus.Disagree).Count();
            }
        }

        [NotMapped]
        public int OffensiveCnt
        {
            get
            {
                return Affinities.Where(a => a.Status == AffinityStatus.Offensive).Count();
            }
        }

        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual List<CommentAffinity> Affinities { get; set; }

        public override int EntityKey
        {
            get { return DiscussionCommentId; }
        }

        public override int ParentId
        {
            get { return DiscussionId; }
        }

    }
}