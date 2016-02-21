using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class BasicProfileVm : IUserSubProfile
    {
        [StringLength(64)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(64)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(128)]
        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        public GenderType Sex { get; set; }

        [Display(Name = "Marital Status")]
        public RelationStatusType MaritialStatus { get; set; }

        [StringLength(8)]
        [Display(Name="Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "I am willing to donate blood")]
        public bool CanDonateBlood { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public int BirthDay { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }

        public BasicProfileVm()
        {

        }

        public BasicProfileVm(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            MaritialStatus = user.MaritialStatus;
            Sex = user.Sex;
            BloodGroup = user.BloodGroup;
            CanDonateBlood = user.CanDonateBlood;
            DateOfBirth = user.DateOfBirth;
            if (user.DateOfBirth != null)
            {
                var date = user.DateOfBirth.Value.Date;
                BirthDay = date.Day;
                BirthYear = date.Year;
                BirthMonth = date.Month;
            }
        }

        public void UpdateUser(User user)
        {
            user.Sex = Sex;
            user.MaritialStatus = MaritialStatus;
            user.LastName = LastName;
            user.FirstName = FirstName;
            user.CanDonateBlood = CanDonateBlood;
            user.BloodGroup = BloodGroup;
            if (BirthDay == 0 && BirthMonth == 0 && BirthYear == 0)
            {
                user.DateOfBirth = null;
            }
            else
            {
                user.DateOfBirth = new DateTime(BirthYear, BirthMonth, BirthDay);
            }
        }
    }
}