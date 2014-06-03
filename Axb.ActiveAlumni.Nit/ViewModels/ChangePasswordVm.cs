using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class ChangePasswordVm
    {
        [DataType(DataType.Password)]
        [Required]
        [Display(Name="Current Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Password]
        [Display(Name="New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}