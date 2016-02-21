using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class IntrestProfileVm : IUserSubProfile
    {
        [Display(Name="Interested in mentoring Student Projects")]
        public bool MentoringInteset { get; set; }
        [Display(Name = "Interested in Entrepreneurship")]
        public bool StartupInterest { get; set; }
        [Display(Name = "Interested in giving Lectures")]
        public bool LectureInterest { get; set; }
        [Display(Name = "Interested in providing Placement Guidance")]
        public bool PlacementInterest { get; set; }

        public IntrestProfileVm()
        {

        }

        public IntrestProfileVm(User user)
        {
            MentoringInteset = user.MentoringInteset;
            PlacementInterest = user.PlacementInterest;
            StartupInterest = user.StartupInterest;
            LectureInterest = user.LectureInterest;
        }
        public void UpdateUser(User user)
        {
            user.MentoringInteset = MentoringInteset;
            user.PlacementInterest = PlacementInterest;
            user.StartupInterest = StartupInterest;
            user.LectureInterest = LectureInterest;
        }
    }
}