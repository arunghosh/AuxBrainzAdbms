using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Activity : ThreadBase
    {
        [Key]
        public int ActivityId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(2048)]
        public string Description { get; set; }

        public int OwnerId { get; set; }

        public int DeputyOwnerId { get; set; }

        public virtual List<ActivityTask> Tasks { get; set; }
        public virtual List<ActivityComment> Comments { get; set; }

        public override int EntityKey
        {
            get { return ActivityId; }
        }

        public override IEnumerable<ThreadItemBase> ThreadItems
        {
            get { return Comments.Select(t => t as ThreadItemBase); }
        }

        public override IEnumerable<EntityUserMap> EntityUserMaps
        {
            get { return Tasks.Select(t => t as EntityUserMap); }
        }
    }
}