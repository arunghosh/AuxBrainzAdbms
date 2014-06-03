using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class RelativeVm
    {
        public int RelativeId { get; set; }

        public int RelativeUserId { get; set; }

        [FullNameLength]
        [Required]
        public string Name { get; set; }

        [StringLength(128)]
        public string Work { get; set; }

        [StringLength(128)]
        public string Education { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [Required]
        public RelationType RelationShip { get; set; }

        public RelativeVm()
        {

        }

        public RelativeVm(Relative model)
        {
            SetUser(model);
        }

        public void SetUser(Relative relative)
        {
            var user = relative.RelativeUser;
            Name = user.FullName;
            var jobs= user.Jobs;
            if(jobs.Any())
            {
                Work = jobs[0].CompanyName;
            }

            var edus = user.Educations;
            if (edus.Any())
            {
                Education = edus[0].SchoolName;
            }
        }

        //public void UpdateUser(User user)
        //{
        //    user.FirstName = Name;
        //    user.CurrentCity = Location;
        //    if (string.IsNullOrEmpty(Work))
        //    {
        //        if (user.Jobs.Any())
        //        {
        //            user.Jobs[0]
        //        }
        //    }
        //}
    }
}