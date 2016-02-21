using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class ActivityComment: ThreadItemBase
    {
        [Key]
        public int ActivityCommentId { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        public override int ParentId
        {
            get { return ActivityId; }
        }

        public override int EntityKey
        {
            get { return ActivityCommentId; }
        }
    }
}