using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class CommentAffinity
    {
        public int CommentAffinityId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public AffinityStatus Status { get; set; }
        public DateTime TimeStamp { get; set; }

        [ForeignKey("DiscussionCommentId")]
        public virtual DiscussionComment DiscussionComment { get; set; }

        public int DiscussionCommentId { get; set; }

        public CommentAffinity()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}