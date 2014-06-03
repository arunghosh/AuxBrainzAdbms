using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Feedback: EntityBase, IReadEntity
    {
        [Key]
        public int FeedbackId { get; set; }

        [StringLength(64)]
        public string IPAddress { get; set; }

        [FullNameLength]
        [Display(Name="Name")]
        public string UserName { get; set; }

        public int UserId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(1024)]
        [Required]
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public override int EntityKey
        {
            get { return FeedbackId; }
        }

        public Feedback()
        {
            Date = DateTime.UtcNow;
        }
    }
}