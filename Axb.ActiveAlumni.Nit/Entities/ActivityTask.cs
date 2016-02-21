using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
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

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        public TaskStatus Status { get; set; }

        [NotMapped]
        public string DaysRemaining 
        {
            get
            {
                if(Status == TaskStatus.Open)
                    return (DueDate - DateTime.Now).Days.ToString();
                return "--";
            }
        }

        [NotMapped]
        public string DueStatus
        {
            get
            {
                if (Status == TaskStatus.Close) return "green";
                if (Status == TaskStatus.Abandon || Status == TaskStatus.Hold) return "gray";
                return (DueDate - DateTime.Now).Days > 0 ? "" : "red";
            }
        }


        public int PercentCompleted { get; set; }

        public int ReminderFrequency { get; set; }

        public override int EntityKey
        {
            get { return ActivityTaskId; }
        }

        public ActivityTask()
        {
            DueDate = DateTime.Now;
        }

        public string ReminderMail()
        {
            var date = string.Format("Due Date : {0}", DueDate.ToString("ddd, dd MMM yyyy"));
            var composer = new HtmlComposer();
            composer.AppendDiv("This is gentle reminder for the task assigned to you.")
                    .AppendHead("Task : " + Title)
                    .AppendDiv(date)
                    .AppendDiv("Click the below link for more details")
                    .AppendLink(Routes.ActivityUrl(ActivityId), Routes.ActivityUrl(ActivityId));
            return composer.Text.ToString();
        }
    }


    //public class EventActionItem : EntityUserMap
    //{
    //    [Key]
    //    public int ActivityTaskId { get; set; }

    //    [StringLength(256)]
    //    public string Title { get; set; }

    //    [StringLength(2048)]
    //    public string Description { get; set; }

    //    //public int EventId { get; set; }

    //    //[ForeignKey("EventId")]
    //    //public Event Event { get; set; }

    //    public DateTime DueDate { get; set; }

    //    public DateTime AssignedDate { get; set; }

    //    public TaskStatus Status { get; set; }

    //    public int PercentCompleted { get; set; }

    //    public override int EntityKey
    //    {
    //        get { return ActivityTaskId; }
    //    }
    //}
}