using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class SignInRequest
    {
        public string UserCode { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool? IsPresistant { get; set; }


        public string IPAddress { get; set; }

        public SignInRequest()
        {
            IsPresistant = true;
        }
    }
}