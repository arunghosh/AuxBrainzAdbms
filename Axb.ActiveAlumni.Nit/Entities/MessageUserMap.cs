using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class MessageUserMap: EntityUserMap
    {
        public int MessageUserMapId { get; set; }

        public int MessageThreadId { get; set; }

        [ForeignKey("MessageThreadId")]
        public virtual MessageThread MessageThread { get; set; }

        public override int EntityKey
        {
            get { return MessageUserMapId; }
        }

        public MessageUserMap()
        {
        }
    }
}