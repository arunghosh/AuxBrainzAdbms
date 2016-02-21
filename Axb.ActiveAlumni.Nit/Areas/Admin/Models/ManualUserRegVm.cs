using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class ManualUserRegVm
    {
            [Required]
            [StringLength(64)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(64)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Email Address")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Batch { get; set; }

            [Required]
            public int CourseId { get; set; }

            [Required]
            public int BranchId { get; set; }

            public ManualUserRegVm()
            {
                CourseId = 1;
                BranchId = 1;
            }

            public User GetUserEntity()
            {
                var user = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    UserRoles = new List<UserRole>()
                };
                return user;
            }
        }
}