using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class EventInvitee: EntityUserMap
    {
        [Key]
        public int EventInviteeId { get; set; }

        public InviteStatusTypes Status { get; set; }

        public DateTime Date { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public override int EntityKey
        {
            get { return EventInviteeId; }
        }

        public EventInvitee()
        {
            Status = InviteStatusTypes.NotResponded;
            Date = DateTime.UtcNow;
        }
    }
}