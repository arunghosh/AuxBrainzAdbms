using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class EventComment : ThreadItemBase
    {
        [Key]
        public int EventCommentId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public override int ParentId
        {
            get { return EventId; }
        }

        public override int EntityKey
        {
            get { return EventCommentId; }
        }

        public EventComment()
        {
            Date = DateTime.UtcNow;
        }
    }
}