using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class RegisterUserVm
    {
        [Required]
        [StringLength(64)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(64)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Password]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Password]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword { get; set; }

        public UserRoleType Role { get; set; }

        [StringLength(32)]
        [Display(Name = "Mobile Number")]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name="Year of graduation")]
        public string Batch { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int BranchId { get; set; }

        //public bool IsAlumni { get; set; }

        public RegisterUserVm()
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