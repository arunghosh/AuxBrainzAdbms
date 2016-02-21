using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class DiscussionUserMap:EntityUserMap
    {
        [Key]
        public int DiscussionUserMapId { get; set; }

        public int DiscussionId { get; set; }

        [ForeignKey("DiscussionId")]
        public virtual Discussion Discussion { get; set; }

        public override int EntityKey
        {
            get { return  DiscussionUserMapId; }
        }
    }
}