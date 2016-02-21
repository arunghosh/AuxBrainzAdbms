using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class ContactProfileVm : IUserSubProfile
    {
        [StringLength(32)]
        [Display(Name = "Mobile Number")]
        [Phone]
        public string MobileNumber { get; set; }

        [StringLength(32)]
        [Display(Name = "Home Phone")]
        [Phone]
        public string HomePhone { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Secondry Email Address")]
        [EmailAddress]
        public string OptionalEmail { get; set; }

        [MaxLength(128)]
        public string Website { get; set; }

        [MaxLength(128)]
        public string Linkdin { get; set; }

        [MaxLength(128)]
        public string Facebook { get; set; }

        public IEnumerable<string> VisibilityOptions { get; set; }
        public List<string> MobileVisibility { get; set; }
        public List<string> HomePhoneVisibility { get; set; }
        public List<string> EmailVisibility { get; set; }

        public ContactProfileVm(User user)
        {
            Email = user.Email;
            MobileNumber = user.MobileNumber;
            Website = user.Website;
            HomePhone = user.HomePhone;
            VisibilityOptions = Enum.GetNames(typeof(VisibilityType)).Cast<string>();
            MobileVisibility = CovertToList(user.MobileVisibility);
            EmailVisibility = CovertToList(user.EmailVisibility);
            HomePhoneVisibility = CovertToList(user.HomePhoneVisibility);
        }

        private List<string> CovertToList(byte visibleCode)
        {
            var visibles = new List<string>();
            foreach (VisibilityType item in Enum.GetValues(typeof(VisibilityType)))
            {
                byte code = (byte)(0x01 << (byte)item);
                if ((visibleCode & code) != 0)
                {
                    visibles.Add(item.ToString());
                }
            }
            return visibles;
        }

        private byte CovertToCode(List<string> visibles)
        {
            byte code = 0;
            if (visibles == null) return code;
            foreach (VisibilityType item in Enum.GetValues(typeof(VisibilityType)))
            {
                if(visibles.Contains(item.ToString()))
                {
                    code |= (byte)(0x01 << (byte)item);
                }
            }
            return code;
        }

        public void UpdateUser(User user)
        {
            user.Email = Email;
            user.MobileNumber = MobileNumber;
            user.Website = Website;
            user.HomePhone = HomePhone;
            user.HomePhoneVisibility = CovertToCode(HomePhoneVisibility);
            user.MobileVisibility = CovertToCode(MobileVisibility);
            user.EmailVisibility = CovertToCode(EmailVisibility);
        }

        public ContactProfileVm()
        {

        }
    }
}