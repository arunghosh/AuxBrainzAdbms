using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class EventMinute : EntityBase
    {
        public int EventMinuteId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [StringLength(512)]
        public string Text { get; set; }

        public override int EntityKey
        {
            get { return EventMinuteId; }
        }
    }
}