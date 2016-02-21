using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class MessageThread: ThreadBase
    {
        [Key]
        public int MessageThreadId { get; set; }

        public virtual List<MessageUserMap> MessageUserMaps { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        public virtual List<Message> Messages { get; set; }

        public override int EntityKey
        {
            get { return MessageThreadId; }
        }

        public MessageThread()
        {
            Messages = new List<Message>();
            MessageUserMaps = new List<MessageUserMap>();
        }

        public override IEnumerable<ThreadItemBase> ThreadItems
        {
            get { return Messages.Select(m => m as ThreadItemBase); }
        }

        public override IEnumerable<EntityUserMap> EntityUserMaps
        {
            get { return MessageUserMaps.Select(m => m as EntityUserMap); }
        }
    }
}