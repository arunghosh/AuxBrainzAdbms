using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class ActivityTask : EntityUserMap
    {
        [Key]
        public int ActivityTaskId { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        [StringLength(2048)]
        public string Description { get; set; }

        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        public DateTime DueDate { get; set; }

        public TaskStatus Status { get; set; }

        public int PercentCompleted { get; set; }

        public override int EntityKey
        {
            get { return ActivityTaskId; }
        }
    }
}