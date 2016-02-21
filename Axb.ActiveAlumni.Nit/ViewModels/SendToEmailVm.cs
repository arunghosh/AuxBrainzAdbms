using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class SendToEmailVm
    {
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}