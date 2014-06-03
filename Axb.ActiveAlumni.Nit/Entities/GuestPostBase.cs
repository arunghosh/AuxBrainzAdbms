using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class GuestPostBase : EntityBase
    {
        public int UserId { get; set; }

        public PostStatusType Status { get; set; }

        public DateTime Date { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }


        [FullNameLength]
        [Display(Name="Your Name")]
        public string UserName { get; set; }

        public GuestPostBase()
        {
            Date = DateTime.UtcNow;
            Status = PostStatusType.Pending;
            UserId = 0;
        }

    }
}