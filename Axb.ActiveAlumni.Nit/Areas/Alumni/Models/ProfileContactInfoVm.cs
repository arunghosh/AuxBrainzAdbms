//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using Axb.ActiveAlumni.Nit.Entities;

//namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Models
//{
//    public class ProfileContactInfoVm : IProfileVm
//    {
//        [StringLength(32)]
//        [Display(Name = "Mobile Number")]
//        public string MobileNumber { get; set; }

//        [StringLength(32)]
//        [Display(Name = "Home Phone")]
//        public string HomePhone { get; set; }

//        [Display(Name = "Email Address")]
//        [Required]
//        [MaxLength(64)]
//        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Enter a valid email address")]
//        public string Email { get; set; }

//        [Display(Name = "Secondry Email Address")]
//        [MaxLength(64)]
//        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Enter a valid email address")]
//        public string OptionalEmail { get; set; }

//        [MaxLength(128)]
//        public string Website { get; set; }

//        [MaxLength(128)]
//        public string Linkdin { get; set; }

//        [MaxLength(128)]
//        public string Facebook { get; set; }

//        public VisibilityType MobileVisibility { get; set; }
//        public VisibilityType HomePhoneVisibility { get; set; }
//        public VisibilityType EmailVisibility { get; set; }

//        public ProfileContactInfoVm(User user)
//        {
//            Email = user.Email;
//            MobileNumber = user.MobileNumber;
//            Website = user.Website;
//            HomePhone = user.HomePhone;
//            MobileVisibility = user.MobileVisbility;
//            EmailVisibility = user.EmailVisibility;
//            HomePhoneVisibility = user.HomePhoneVisibility;
//        }

//        public void UpdateUser(User user)
//        {
//            user.Email = Email;
//            user.MobileNumber = MobileNumber;
//            user.Website = Website;
//            user.HomePhone = HomePhone;
//            user.HomePhoneVisibility = HomePhoneVisibility;
//            user.MobileVisbility = MobileVisibility;
//            user.EmailVisibility = EmailVisibility;
//        }

//        public ProfileContactInfoVm()
//        {

//        }
//    }
//}